using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace Unity1Week0619.GameSystems
{
    /// <summary>
    /// サカバンバスピスを制御するクラス
    /// </summary>
    public class SacabambaspisController : MonoBehaviour
    {
        [SerializeField]
        private Define.SacabambaspisType sacabambaspisType;

        [SerializeField]
        private Rigidbody2D targetRigidbody2D;

        [SerializeField]
        private float spawnDelaySeconds;

        [SerializeField]
        private GameObject spawnEffectPrefab;

        [SerializeField]
        private GameObject onEnterEffectPrefab;

        /// <summary>
        /// プレイヤーの中に入ったか
        /// </summary>
        private bool isEnterPlayer;

        public async UniTask SetupAsync(GameDesignData gameDesignData)
        {
            // 最初にエフェクトを生成する
            Instantiate(this.spawnEffectPrefab, this.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
            this.targetRigidbody2D.simulated = false;
            PlaySE(gameDesignData.GetSacabambaspisData(this.sacabambaspisType).SpawnAudioDataList);

            await UniTask.Delay(TimeSpan.FromSeconds(this.spawnDelaySeconds));

            this.gameObject.SetActive(true);
            this.targetRigidbody2D.simulated = true;
            PlaySE(gameDesignData.GetSacabambaspisData(this.sacabambaspisType).AppearanceAudioDataList);

            this.GetAsyncTriggerEnter2DTrigger()
                .Subscribe(other =>
                {
                    if (other.attachedRigidbody != null)
                    {
                        if (!this.isEnterPlayer)
                        {
                            // プレイヤーと接触した場合はプレイヤーの中に入ったとみなす
                            if (other.attachedRigidbody.GetComponent<PlayerController>() != null)
                            {
                                this.OnEnterPlayer(gameDesignData);
                            }
                            else
                            {
                                // 他のサカバンバスピスがプレイヤーの中に入っている場合は自分もプレイヤーの中に入る
                                var otherSacabambaspis = other.attachedRigidbody.GetComponent<SacabambaspisController>();
                                if(otherSacabambaspis != null && otherSacabambaspis.isEnterPlayer)
                                {
                                    this.OnEnterPlayer(gameDesignData);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (other.CompareTag("Deadline"))
                        {
                            Destroy(this.gameObject);
                            PlaySE(gameDesignData.GetSacabambaspisData(this.sacabambaspisType).OnExitAudioDataList);
                            MessageBroker.GetPublisher<GameEvents.OnExitSacabambaspis>()
                                .Publish(GameEvents.OnExitSacabambaspis.Get(this.sacabambaspisType, this.isEnterPlayer));
                        }
                    }
                })
                .AddTo(this.GetCancellationTokenOnDestroy());
        }

        /// <summary>
        /// プレイヤーの中に入った際の処理
        /// </summary>
        private void OnEnterPlayer(GameDesignData gameDesignData)
        {
            this.isEnterPlayer = true;
            Instantiate(this.onEnterEffectPrefab, this.transform.position, Quaternion.identity);
            PlaySE(gameDesignData.GetSacabambaspisData(this.sacabambaspisType).OnEnterAudioDataList);
            MessageBroker.GetPublisher<GameEvents.OnEnterSacabambaspis>()
                .Publish(GameEvents.OnEnterSacabambaspis.Get(this.sacabambaspisType));
        }

        private static void PlaySE(List<SoundEffectElement.AudioData> dataList)
        {
            if(dataList.Count <= 0)
            {
                return;
            }
            var audioData = dataList[UnityEngine.Random.Range(0, dataList.Count)];
            AudioManager.PlaySE(audioData);
        }
    }
}
