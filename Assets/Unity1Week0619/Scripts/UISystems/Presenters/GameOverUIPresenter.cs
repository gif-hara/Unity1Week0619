using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using MessagePipe;
using TweetWithScreenShot;
using UnityEngine;

namespace Unity1Week0619.UISystems.Presenters
{
    public class GameOverUIPresenter
    {
        public static void Setup(
            GameOverUIView viewPrefab,
            int score,
            Texture2D screenShot,
            string comment,
            CancellationToken token
            )
        {
            var view = UIManager.Open(viewPrefab);

            view.RetryButton.OnClickAsAsyncEnumerable()
                .Subscribe(_ =>
                {
                    AudioManager.FadeBGM(0.5f, 0.0f);
                    SceneManager.LoadScene("Game");
                })
                .AddTo(token);

            view.TitleButton.OnClickAsAsyncEnumerable()
                .Subscribe(_ =>
                {
                    AudioManager.FadeBGM(0.5f, 0.0f);
                    SceneManager.LoadScene("Title");
                })
                .AddTo(token);

            view.TweetButton.OnClickAsAsyncEnumerable()
                .Subscribe(_ =>
                {
                    view.StartCoroutine(TweetManager.TweetWithScreenShot($"バスピスパフェで{score}バスピス獲得したよ！バスピス！"));
                })
                .AddTo(token);

            MessageBroker.GetSubscriber<SceneEvents.BeginLoad>()
                .Subscribe(_ =>
                {
                    UIManager.Close(view);
                })
                .AddTo(token);

            view.ScoreText.text = $"{score}バスピス！";
            view.ScreenShot.texture = screenShot;
            view.Comment.text = comment;
        }
    }
}
