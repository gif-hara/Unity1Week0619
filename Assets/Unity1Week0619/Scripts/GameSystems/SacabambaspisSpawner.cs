using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity1Week0619.Scripts.GameSystems;
using UnityEngine;

namespace Unity1Week0619.GameSystems
{
    public class SacabambaspisSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform parent;

        [SerializeField]
        private GameObject sacabambaspisPrefab;

        /// <summary>
        /// 最初に生成する座標を持つオブジェクト
        /// </summary>
        [SerializeField]
        private Transform firstSpawnPoint;

        [SerializeField]
        private float spawnInterval;

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
            UniTask.Void(async _ =>
                {
                    var spawnCount = 0;
                    var level = 0;
                    while (true)
                    {
                        var delaySeconds = gameDesignData.LevelData.GetSpawnIntervalSeconds(level);
                        await UniTask.Delay(TimeSpan.FromSeconds(delaySeconds), cancellationToken: cancellationToken);
                        
                        var position = this.GetSpawnPosition(spawnCount);
                        Instantiate(sacabambaspisPrefab, position, Quaternion.identity, this.parent);
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
    }
}
