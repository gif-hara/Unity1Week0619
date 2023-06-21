using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using MessagePipe;
using Unity1Week0619.GameSystems;

namespace Unity1Week0619.UISystems.Presenters
{
    public static class GameUIPresenter
    {
        public static void Setup(
            GameUIView viewPrefab,
            IUniTaskAsyncEnumerable<int> score,
            IUniTaskAsyncEnumerable<float> baspisGauge,
            IUniTaskAsyncEnumerable<float> gameTimeSeconds,
            CancellationToken cancellationToken
            )
        {
            var view = UIManager.Open(viewPrefab);
            view.Message.SetActive(false);

            score
                .Subscribe(x =>
                {
                    // ローカライズは必要になったらする
                    view.SacabambaspisCount.CountText.text = $"{x}バスピス！";
                    view.SacabambaspisCount.AnimationController.Play(view.SacabambaspisCount.UpdateAnimationClip);
                })
                .AddTo(cancellationToken);
            baspisGauge
                .Subscribe(x =>
                {
                    view.BaspisGauge.Gauge.value = x;
                })
                .AddTo(cancellationToken);
            gameTimeSeconds
                .Subscribe(x =>
                {
                    view.GameTime.Text.text = $"{x:F1}";
                })
                .AddTo(cancellationToken);

            // ゲーム開始時にメッセージを出す
            MessageBroker.GetAsyncSubscriber<GameEvents.NotifyBeginGame>()
                .Subscribe(async (_, ct) =>
                {
                    await view.Message.ShowAsync("スタート！");
                })
                .AddTo(cancellationToken);

            // ゲーム終了時にメッセージを出す
            MessageBroker.GetAsyncSubscriber<GameEvents.NotifyEndGame>()
                .Subscribe(async (_, ct) =>
                {
                    await view.Message.ShowAsync("パフェ完成！");
                })
                .AddTo(cancellationToken);
        }
    }
}