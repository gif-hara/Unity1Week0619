using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;

namespace Unity1Week0619.GameSystems
{
    public class DeadSpeechBalloonController : MonoBehaviour
    {
        [SerializeField]
        private SpeechBalloonController speechBalloonControllerPrefab;

        private void Start()
        {
            MessageBroker.GetSubscriber<GameEvents.OnExitSacabambaspis>()
                .Subscribe(x =>
                {
                    var speechBalloonController = Instantiate(this.speechBalloonControllerPrefab, this.transform);
                    speechBalloonController.PlayAnimation(x.Serif);
                    var position = new Vector3(x.Position.x, 0.0f, 0.0f);
                    speechBalloonController.transform.localPosition = position;
                })
                .AddTo(this.GetCancellationTokenOnDestroy());
        }
    }
}
