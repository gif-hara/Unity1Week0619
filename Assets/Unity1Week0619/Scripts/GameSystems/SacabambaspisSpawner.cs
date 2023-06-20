using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;

namespace Unity1Week0619.GameSystems
{
    public class SacabambaspisSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform parent;

        [SerializeField]
        private List<SacabambaspisController> normalSacabambaspisControllerPrefabs;

        [SerializeField]
        private List<SacabambaspisController> colorfulSacabambaspisControllerPrefabs;

        /// <summary>
        /// 最初に生成する座標を持つオブジェクト
        /// </summary>
        [SerializeField]
        private Transform firstSpawnPoint;

        [SerializeField]
        private Rect spawnArea;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            var center = this.transform.localPosition + new Vector3(spawnArea.center.x, spawnArea.center.y, 0f);
            Gizmos.DrawWireCube(center, spawnArea.size);
        }

        public void BeginSpawn(GameDesignData gameDesignData, CancellationToken cancellationToken)
        {
            var isFullBaspisMode = false;

            MessageBroker.GetSubscriber<GameEvents.BeginFullBaspisMode>()
                .Subscribe(_ => isFullBaspisMode = true)
                .AddTo(cancellationToken);
            MessageBroker.GetSubscriber<GameEvents.EndFullBaspisMode>()
                .Subscribe(_ => isFullBaspisMode = false)
                .AddTo(cancellationToken);

            UniTask.Void(async _ =>
                {
                    var spawnCount = 0;
                    var level = 0;
                    while (true)
                    {
                        var delaySeconds = gameDesignData.LevelData.GetSpawnIntervalSeconds(level);
                        await UniTask.Delay(TimeSpan.FromSeconds(delaySeconds), cancellationToken: cancellationToken);

                        // すでにキャンセルされていたら何もしない
                        if (cancellationToken.IsCancellationRequested)
                        {
                            return;
                        }

                        var position = this.GetSpawnPosition(spawnCount);
                        var sacabambaspis = Instantiate(GetSacabambaspisControllerPrefab(isFullBaspisMode), position, Quaternion.identity, this.parent);
                        sacabambaspis.SetupAsync(gameDesignData).Forget();
                        spawnCount++;
                    }
                },
                cancellationToken);
        }

        /// <summary>
        /// 生成する座標を返す
        /// </summary>
        private Vector3 GetSpawnPosition(int spawnCount)
        {
            // 初回は分かりやすくするために指定した座標を返す
            if (spawnCount == 0)
            {
                return this.firstSpawnPoint.position;
            }

            // それ以外はランダムな座標を返す
            var position = this.transform.localPosition;
            position.x += UnityEngine.Random.Range(spawnArea.xMin, spawnArea.xMax);
            position.y += UnityEngine.Random.Range(spawnArea.yMin, spawnArea.yMax);
            return position;
        }

        /// <summary>
        /// 生成するサカバンバスピスを返す
        /// </summary>
        private SacabambaspisController GetSacabambaspisControllerPrefab(bool isFullBaspisMode)
        {
            var list = isFullBaspisMode ? this.colorfulSacabambaspisControllerPrefabs : this.normalSacabambaspisControllerPrefabs;
            var index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
        }
    }
}
