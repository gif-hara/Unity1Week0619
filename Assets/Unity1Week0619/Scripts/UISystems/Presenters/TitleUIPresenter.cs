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

            view.GameStartButton.OnClickAsAsyncEnumerable()
                .Subscribe(_ =>
                {
                    SceneManager.LoadScene("Game");
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
