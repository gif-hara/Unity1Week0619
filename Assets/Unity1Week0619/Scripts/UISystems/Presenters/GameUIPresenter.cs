using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Unity1Week0619.UISystems.Presenters
{
    public class GameUIPresenter
    {
        private GameUIView View { get; }

        public GameUIPresenter(
            GameUIView viewPrefab,
            IUniTaskAsyncEnumerable<int> score,
            IUniTaskAsyncEnumerable<float> baspisGauge,
            IUniTaskAsyncEnumerable<float> fullBaspisModeGauge,
            CancellationToken cancellationToken
            )
        {
            this.View = UIManager.Open(viewPrefab);
            score
                .Subscribe(x =>
                {
                    // ローカライズは必要になったらする
                    this.View.SacabambaspisCount.CountText.text = $"{x}バスピス！";
                })
                .AddTo(cancellationToken);
            baspisGauge
                .Subscribe(x =>
                {
                    this.View.BaspisGauge.Gauge.value = x;
                })
                .AddTo(cancellationToken);
            fullBaspisModeGauge
                .Subscribe(x =>
                {
                    this.View.FullBaspisMode.Gauge.value = x;
                })
                .AddTo(cancellationToken);
        }
    }
}
