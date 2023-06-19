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
                if(!this.isEnterPlayer && collider.attachedRigidbody.GetComponent<PlayerController>() != null)
                {
                    this.isEnterPlayer = true;
                    MessageBroker.GetPublisher<GameEvents.OnEnterSacabambaspis>()
                        .Publish(GameEvents.OnEnterSacabambaspis.Get());

                }
                else if (!this.isEnterPlayer && collider.attachedRigidbody.GetComponent<SacabambaspisController>() != null)
                {
                    var other = collider.attachedRigidbody.GetComponent<SacabambaspisController>();
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
