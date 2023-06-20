using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using Unity1Week0619.GameSystems;
using Unity1Week0619.Scripts.GameSystems;
using UnityEngine;

namespace Unity1Week0619
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

        private async void Start()
        {
            // 最初にエフェクトを生成する
            Instantiate(this.spawnEffectPrefab, this.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
            this.targetRigidbody2D.simulated = false;
            // TODO: SE再生

            await UniTask.Delay(TimeSpan.FromSeconds(this.spawnDelaySeconds));
            
            this.gameObject.SetActive(true);
            this.targetRigidbody2D.simulated = true;
            // TODO: SE再生する？

            this.GetAsyncTriggerEnter2DTrigger()
                .Subscribe(other =>
                {
                    if (other.attachedRigidbody != null)
                    {
                        // プレイヤーと接触した場合はプレイヤーの中に入ったとみなす
                        if (!this.isEnterPlayer && other.attachedRigidbody.GetComponent<PlayerController>() != null)
                        {
                            this.OnEnterPlayer();
                        }
                        // 他のサカバンバスピスに接触した場合はプレイヤーの中に入っているかを確認する
                        else if (!this.isEnterPlayer &&
                                 other.attachedRigidbody.GetComponent<SacabambaspisController>() != null)
                        {
                            var otherSacabambaspis = other.attachedRigidbody.GetComponent<SacabambaspisController>();
                            // 他のサカバンバスピスがプレイヤーの中に入っている場合は自分もプレイヤーの中に入る
                            if (otherSacabambaspis.isEnterPlayer)
                            {
                                this.OnEnterPlayer();
                            }
                        }
                    }
                    else
                    {
                        if (other.CompareTag("Deadline"))
                        {
                            Destroy(this.gameObject);
                            if (this.isEnterPlayer)
                            {
                                MessageBroker.GetPublisher<GameEvents.OnExitSacabambaspis>()
                                    .Publish(GameEvents.OnExitSacabambaspis.Get(this.sacabambaspisType));
                            }
                        }
                    }
                })
                .AddTo(this.GetCancellationTokenOnDestroy());
        }

        /// <summary>
        /// プレイヤーの中に入った際の処理
        /// </summary>
        private void OnEnterPlayer()
        {
            this.isEnterPlayer = true;
            Instantiate(this.onEnterEffectPrefab, this.transform.position, Quaternion.identity);
            MessageBroker.GetPublisher<GameEvents.OnEnterSacabambaspis>()
                .Publish(GameEvents.OnEnterSacabambaspis.Get(this.sacabambaspisType));
        }
    }
}
