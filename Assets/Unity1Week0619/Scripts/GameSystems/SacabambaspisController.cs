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
        /// <summary>
        /// プレイヤーの中に入ったか
        /// </summary>
        private bool isEnterPlayer;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.attachedRigidbody != null)
            {
                // プレイヤーと接触した場合はプレイヤーの中に入ったとみなす
                if(!this.isEnterPlayer && collider.attachedRigidbody.GetComponent<PlayerController>() != null)
                {
                    this.isEnterPlayer = true;
                    MessageBroker.GetPublisher<GameEvents.OnEnterSacabambaspis>()
                        .Publish(GameEvents.OnEnterSacabambaspis.Get());

                }
                // 他のサカバンバスピスに接触した場合はプレイヤーの中に入っているかを確認する
                else if (!this.isEnterPlayer && collider.attachedRigidbody.GetComponent<SacabambaspisController>() != null)
                {
                    var other = collider.attachedRigidbody.GetComponent<SacabambaspisController>();
                    // 他のサカバンバスピスがプレイヤーの中に入っている場合は自分もプレイヤーの中に入る
                    if (other.isEnterPlayer)
                    {
                        this.isEnterPlayer = true;
                        MessageBroker.GetPublisher<GameEvents.OnEnterSacabambaspis>()
                            .Publish(GameEvents.OnEnterSacabambaspis.Get());
                    }
                }
            }
            else
            {
                if (collider.CompareTag("Deadline"))
                {
                    Destroy(this.gameObject);
                    if (this.isEnterPlayer)
                    {
                        MessageBroker.GetPublisher<GameEvents.OnExitSacabambaspis>()
                            .Publish(GameEvents.OnExitSacabambaspis.Get());
                    }
                }
            }
        }
    }
}
