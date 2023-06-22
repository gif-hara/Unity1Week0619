using System;
using System.Threading;
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
            try
            {
                // ブートシステム初期化待ち
                await BootSystem.IsReady;

                // ゲームシステム初期化
                var gameSceneToken = this.GetCancellationTokenOnDestroy();
                var score = new AsyncReactiveProperty<int>(0);
                var baspisGauge = new AsyncReactiveProperty<float>(0.0f);
                var gameTimeSeconds = new AsyncReactiveProperty<float>(this.gameDesignData.GameTimeSeconds);
                GameUIPresenter.Setup(
                    this.gameUIView,
                    score,
                    baspisGauge,
                    gameTimeSeconds,
                    gameSceneToken
                    );

                // サカバンバスピスがプレイヤーに入った際の処理
                MessageBroker.GetSubscriber<GameEvents.OnEnterSacabambaspis>()
                    .Subscribe(x =>
                    {
                        score.Value += this.gameDesignData.GetSacabambaspisData(x.SacabambaspisType).Score;
                        baspisGauge.Value = Mathf.Clamp01(baspisGauge.Value + this.gameDesignData.BaspisGaugeData.OnEnterAmount);
                        if (baspisGauge.Value >= 1.0f)
                        {
                            // フルバスピスモード開始
                            MessageBroker.GetPublisher<GameEvents.BeginFullBaspisMode>()
                                .Publish(GameEvents.BeginFullBaspisMode.Get());
                        }
                    })
                    .AddTo(gameSceneToken);

                // サカバンバスピスが離れた際の処理
                MessageBroker.GetSubscriber<GameEvents.OnExitSacabambaspis>()
                    .Subscribe(x =>
                    {
                        if (x.IsEnteredPlayer)
                        {
                            score.Value -= this.gameDesignData.GetSacabambaspisData(x.SacabambaspisType).Score;
                        }
                        baspisGauge.Value = 0.0f;
                    })
                    .AddTo(gameSceneToken);
                
                // フルバスピスモードが開始した際の処理
                MessageBroker.GetSubscriber<GameEvents.BeginFullBaspisMode>()
                    .Subscribe(x =>
                    {
                        this.sacabambaspisSpawner.SpawnColorful(this.gameDesignData);
                        baspisGauge.Value = 0.0f;
                    })
                    .AddTo(gameSceneToken);
                
                // ゲームを開始した際の処理
                MessageBroker.GetSubscriber<GameEvents.BeginGame>()
                    .Subscribe(x =>
                    {
                        this.GetAsyncUpdateTrigger()
                            .Subscribe(_ =>
                            {
                                gameTimeSeconds.Value -= UnityEngine.Time.deltaTime;
                            })
                            .AddTo(x.GameScopeToken);
                    })
                    .AddTo(gameSceneToken);

                // ゲーム終了待ちの処理
                MessageBroker.GetAsyncSubscriber<GameEvents.TakeUntilEndGame>()
                    .Subscribe(async (_, ct) =>
                    {
                        await UniTask.WaitWhile(() => gameTimeSeconds.Value > 0.0f, cancellationToken: ct);
                    })
                    .AddTo(gameSceneToken);

                // ゲーム開始を通知する
                await MessageBroker.GetAsyncPublisher<GameEvents.NotifyBeginGame>()
                    .PublishAsync(GameEvents.NotifyBeginGame.Get(), gameSceneToken);

                // ゲームを開始する
                var inGameTokenSource = new CancellationTokenSource();
                MessageBroker.GetPublisher<GameEvents.BeginGame>()
                    .Publish(GameEvents.BeginGame.Get(inGameTokenSource.Token));
                
                this.sacabambaspisSpawner.BeginSpawn(this.gameDesignData, inGameTokenSource.Token);

                // ゲームが終了するまで待機
                await MessageBroker.GetAsyncPublisher<GameEvents.TakeUntilEndGame>()
                    .PublishAsync(GameEvents.TakeUntilEndGame.Get(), gameSceneToken);

                inGameTokenSource.Cancel();
                inGameTokenSource.Dispose();

                // ゲーム終了を通知する
                await MessageBroker.GetAsyncPublisher<GameEvents.NotifyEndGame>()
                    .PublishAsync(GameEvents.NotifyEndGame.Get(), gameSceneToken);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    }
}