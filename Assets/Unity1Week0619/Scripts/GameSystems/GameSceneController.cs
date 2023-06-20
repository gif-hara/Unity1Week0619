using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using MessagePipe;
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
        private GameDesignData gameDesignData;
        
        [SerializeField]
        private SacabambaspisSpawner sacabambaspisSpawner;

        [SerializeField]
        private GameUIView gameUIView;

        async void Start()
        {
            // ブートシステム初期化待ち
            await BootSystem.IsReady;

            // ゲームシステム初期化
            var cancellationToken = this.GetCancellationTokenOnDestroy();
            var score = new AsyncReactiveProperty<int>(0);
            var baspisGauge = new AsyncReactiveProperty<float>(this.gameDesignData.BaspisGaugeData.InitialAmount);
            var fullBaspisModeGauge = new AsyncReactiveProperty<float>(0.0f);
            IDisposable fullBaspisModeStream = null;
            GameUIPresenter.Setup(
                this.gameUIView,
                score,
                baspisGauge,
                fullBaspisModeGauge,
                cancellationToken
                );
            
            // サカバンバスピスがプレイヤーに入った際の処理
            MessageBroker.GetSubscriber<GameEvents.OnEnterSacabambaspis>()
                .Subscribe(x =>
                {
                    score.Value += this.gameDesignData.GetSacabambaspisData(x.SacabambaspisType).Score;
                    if(fullBaspisModeStream != null)
                    {
                        fullBaspisModeGauge.Value = Mathf.Clamp01(fullBaspisModeGauge.Value + this.gameDesignData.FullBaspisModeData.OnEnterAmount);
                    }
                    else
                    {
                        baspisGauge.Value = Mathf.Clamp01(baspisGauge.Value + this.gameDesignData.BaspisGaugeData.OnEnterAmount);
                    }

                    if (baspisGauge >= 1.0f && fullBaspisModeStream == null)
                    {
                        // フルバスピスモード開始
                        MessageBroker.GetPublisher<GameEvents.BeginFullBaspisMode>()
                            .Publish(GameEvents.BeginFullBaspisMode.Get());
                        fullBaspisModeGauge.Value = 1.0f;
                        fullBaspisModeStream = this.GetAsyncUpdateTrigger()
                            .Subscribe(_ =>
                            {
                                fullBaspisModeGauge.Value -= this.gameDesignData.FullBaspisModeData.DecreaseAmountPerSeconds * UnityEngine.Time.deltaTime;
                                if (fullBaspisModeGauge <= 0.0f)
                                {
                                    // フルバスピスモード終了
                                    fullBaspisModeStream?.Dispose();
                                    fullBaspisModeStream = null;
                                    baspisGauge.Value = this.gameDesignData.BaspisGaugeData.InitialAmount;
                                    MessageBroker.GetPublisher<GameEvents.EndFullBaspisMode>()
                                        .Publish(GameEvents.EndFullBaspisMode.Get());
                                }
                            });
                    }
                })
                .AddTo(cancellationToken);
            
            // サカバンバスピスが離れた際の処理
            MessageBroker.GetSubscriber<GameEvents.OnExitSacabambaspis>()
                .Subscribe(x =>
                {
                    score.Value -= this.gameDesignData.GetSacabambaspisData(x.SacabambaspisType).Score;
                    if(fullBaspisModeStream != null)
                    {
                        fullBaspisModeGauge.Value = Mathf.Clamp01(fullBaspisModeGauge - this.gameDesignData.FullBaspisModeData.OnExitAmount);
                    }
                    else
                    {
                        baspisGauge.Value = Mathf.Clamp01(baspisGauge.Value - this.gameDesignData.BaspisGaugeData.OnExitAmount);
                    }
                })
                .AddTo(cancellationToken);
            
            // ゲーム終了時の処理
            MessageBroker.GetAsyncSubscriber<GameEvents.TakeUntilEndGame>()
                .Subscribe(async (_, ct) =>
                {
                    await baspisGauge
                        .Where(x => x <= 0.0f)
                        .FirstAsync(ct);
                })
                .AddTo(cancellationToken);

            // ゲームを開始する
            await MessageBroker.GetAsyncPublisher<GameEvents.BeginGame>()
                .PublishAsync(GameEvents.BeginGame.Get(), cancellationToken);
            
            this.sacabambaspisSpawner.BeginSpawn(this.gameDesignData, cancellationToken);
            
            // ゲームが終了するまで待機
            await MessageBroker.GetAsyncPublisher<GameEvents.TakeUntilEndGame>()
                .PublishAsync(GameEvents.TakeUntilEndGame.Get(), cancellationToken);
            
            // ゲーム終了
            await MessageBroker.GetAsyncPublisher<GameEvents.EndGame>()
                .PublishAsync(GameEvents.EndGame.Get(), cancellationToken);
        }
    }
}
