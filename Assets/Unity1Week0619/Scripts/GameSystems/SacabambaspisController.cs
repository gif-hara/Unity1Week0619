using System;
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
                        if (!this.isEnterPlayer)
                        {
                            // プレイヤーと接触した場合はプレイヤーの中に入ったとみなす
                            if (other.attachedRigidbody.GetComponent<PlayerController>() != null)
                            {
                                this.OnEnterPlayer();
                            }
                            else
                            {
                                // 他のサカバンバスピスがプレイヤーの中に入っている場合は自分もプレイヤーの中に入る
                                var otherSacabambaspis = other.attachedRigidbody.GetComponent<SacabambaspisController>();
                                if(otherSacabambaspis != null && otherSacabambaspis.isEnterPlayer)
                                {
                                    this.OnEnterPlayer();
                                }
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
