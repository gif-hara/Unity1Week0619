using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using MessagePipe;
using UnityEngine.SceneManagement;

namespace Unity1Week0619.UISystems.Presenters
{
    public class LicenseUIPresenter
    {
        public static void Setup(LicenseUIView viewPrefab, CancellationToken token)
        {
            var view = UIManager.Open(viewPrefab);

            view.TitleButton.OnClickAsAsyncEnumerable()
                .Subscribe(_ =>
                {
                    SceneManager.LoadScene("Title");
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
