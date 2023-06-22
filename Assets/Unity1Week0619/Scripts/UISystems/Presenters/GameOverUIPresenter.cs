using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using MessagePipe;

namespace Unity1Week0619.UISystems.Presenters
{
    public class GameOverUIPresenter
    {
        public static void Setup(GameOverUIView viewPrefab, int score, CancellationToken token)
        {
            var view = UIManager.Open(viewPrefab);

            view.RetryButton.OnClickAsAsyncEnumerable()
                .Subscribe(_ =>
                {
                    SceneManager.LoadScene("Game");
                })
                .AddTo(token);

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

            view.ScoreText.text = $"{score}バスピス！";
        }
    }
}
