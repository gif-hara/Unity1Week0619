using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using MessagePipe;
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
        private GameObject spawnEffectPrefab;

        [SerializeField]
        private GameObject onEnterEffectPrefab;

        [SerializeField]
        private SpeechBalloonController speechBalloonController;

        /// <summary>
        /// プレイヤーの中に入ったか
        /// </summary>
        private bool isEnterPlayer;

        public async UniTask SetupAsync(GameDesignData gameDesignData)
        {
            try
            {
                var token = this.GetCancellationTokenOnDestroy();
                
                // ゲームが終了していたら死亡する
                MessageBroker.GetSubscriber<GameEvents.NotifyEndGame>()
                    .Subscribe(_ =>
                    {
                        Destroy(this.gameObject);
                    })
                    .AddTo(token);
                
                // 最初にエフェクトを生成する
                Instantiate(this.spawnEffectPrefab, this.transform.position, Quaternion.identity);
                this.gameObject.SetActive(false);
                this.targetRigidbody2D.simulated = false;
                var sacabambaspisData = gameDesignData.GetSacabambaspisData(this.sacabambaspisType);
                PlaySE(sacabambaspisData.SpawnAudioDataList);

                await UniTask.Delay(TimeSpan.FromSeconds(sacabambaspisData.SpawnDelaySeconds), cancellationToken: token);

                // ここで登場する
                this.gameObject.SetActive(true);
                this.targetRigidbody2D.simulated = true;
                this.targetRigidbody2D.velocity = new Vector2(
                    UnityEngine.Random.Range(sacabambaspisData.SpawnVelocityMin.x,
                        sacabambaspisData.SpawnVelocityMax.x),
                    UnityEngine.Random.Range(sacabambaspisData.SpawnVelocityMin.y, sacabambaspisData.SpawnVelocityMax.y)
                );
                this.targetRigidbody2D.AddTorque(
                    UnityEngine.Random.Range(sacabambaspisData.SpawnAddTorqueMin, sacabambaspisData.SpawnAddTorqueMax)
                );
                var appearanceAudioData =
                    sacabambaspisData.AppearanceAudioDataList[
                        UnityEngine.Random.Range(0, sacabambaspisData.AppearanceAudioDataList.Count)];
                AudioManager.PlaySE(appearanceAudioData.AudioData);
                this.speechBalloonController.PlayAnimation(appearanceAudioData.Serif);

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
                                    var otherSacabambaspis =
                                        other.attachedRigidbody.GetComponent<SacabambaspisController>();
                                    if (otherSacabambaspis != null && otherSacabambaspis.isEnterPlayer)
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
                                var audioData = sacabambaspisData.OnExitAudioDataList[
                                    UnityEngine.Random.Range(0, sacabambaspisData.OnExitAudioDataList.Count)];
                                AudioManager.PlaySE(audioData.AudioData);
                                MessageBroker.GetPublisher<GameEvents.OnExitSacabambaspis>()
                                    .Publish(GameEvents.OnExitSacabambaspis.Get(
                                            this.sacabambaspisType,
                                            this.isEnterPlayer,
                                            this.transform.localPosition,
                                            audioData.Serif
                                        )
                                    );
                            }
                        }
                    })
                    .AddTo(this.GetCancellationTokenOnDestroy());
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
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

        private static void PlaySE(IReadOnlyList<SoundEffectElement.AudioData> dataList)
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
