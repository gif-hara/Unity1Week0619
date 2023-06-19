using Cysharp.Threading.Tasks;
using MessagePipe;
using Unity1Week0619.Scripts.GameSystems;
using Unity1Week0619.UISystems;
using Unity1Week0619.UISystems.Presenters;
using UnityEngine;

namespace Unity1Week0619.GameSystems
{
    /// <summary>
    /// ゲームシーンを制御するクラス
    /// </summary>
    public class GameSceneController : MonoBehaviour
    {
        [SerializeField]
        private SacabambaspisSpawner sacabambaspisSpawner;

        [SerializeField]
        private GameUIView gameUIView;

        async void Start()
        {
            await BootSystem.IsReady;

            var ct = this.GetCancellationTokenOnDestroy();
            this.sacabambaspisSpawner.BeginSpawn(ct);

            var gameUIPresenter = new GameUIPresenter(this.gameUIView);
            gameUIPresenter.SetSacabambaspisCount(0);

            var sacabambaspisCount = 0;
            MessageBroker.GetSubscriber<GameEvents.OnEnterSacabambaspis>()
                .Subscribe(_ =>
                {
                    gameUIPresenter.SetSacabambaspisCount(++sacabambaspisCount);
                })
                .AddTo(ct);
            MessageBroker.GetSubscriber<GameEvents.OnExitSacabambaspis>()
                .Subscribe(_ =>
                {
                    gameUIPresenter.SetSacabambaspisCount(--sacabambaspisCount);
                })
                .AddTo(ct);
        }
    }
}
