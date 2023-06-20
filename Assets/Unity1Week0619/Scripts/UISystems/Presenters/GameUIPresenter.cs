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
            IUniTaskAsyncEnumerable<float> fullBaspisModeGauge,
            CancellationToken cancellationToken
            )
        {
            var view = UIManager.Open(viewPrefab);
            
            score
                .Subscribe(x =>
                {
                    // ローカライズは必要になったらする
                    view.SacabambaspisCount.CountText.text = $"{x}バスピス！";
                })
                .AddTo(cancellationToken);
            baspisGauge
                .Subscribe(x =>
                {
                    view.BaspisGauge.Gauge.value = x;
                })
                .AddTo(cancellationToken);
            fullBaspisModeGauge
                .Subscribe(x =>
                {
                    view.FullBaspisMode.Gauge.value = x;
                })
                .AddTo(cancellationToken);

            MessageBroker.GetAsyncSubscriber<GameEvents.BeginGame>()
                .Subscribe(async (_, ct) =>
                {
                    await view.GameStartMessage.ShowAsync();
                })
                .AddTo(cancellationToken);
        }
    }
}
