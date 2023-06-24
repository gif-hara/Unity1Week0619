using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using MessagePipe;
using UnityEngine.SceneManagement;

namespace Unity1Week0619.UISystems.Presenters
{
    public class TitleUIPresenter
    {
        public static void Setup(TitleUIView viewPrefab, CancellationToken token)
        {
            var view = UIManager.Open(viewPrefab);

            view.Animator.Update(0.0f);

            view.GameStartButton.OnClickAsAsyncEnumerable()
                .Subscribe(_ =>
                {
                    SceneManager.LoadScene("Game");
                    AudioManager.FadeBGM(0.5f, 0.0f);
                })
                .AddTo(token);

            view.LicenseButton.OnClickAsAsyncEnumerable()
                .Subscribe(_ =>
                {
                    SceneManager.LoadScene("License");
                })
                .AddTo(token);

            MessageBroker.GetSubscriber<SceneEvents.BeginLoad>()
                .Subscribe(_ =>
                {
                    UIManager.Close(view);
                })
                .AddTo(token);
        }
    }
}
