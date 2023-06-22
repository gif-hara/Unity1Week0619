using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using MessagePipe;
using Unity1Week0619.GameOverSystems;
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
                var sceneToken = this.GetCancellationTokenOnDestroy();
                var inGameTokenSource = new CancellationTokenSource();
                var gameData = new GameData();
                gameData.gameTimeSeconds.Value = this.gameDesignData.GameTimeSeconds;
                GameUIPresenter.Setup(
                    this.gameUIView,
                    gameData,
                    sceneToken
                    );

                // サカバンバスピスがプレイヤーに入った際の処理
                MessageBroker.GetSubscriber<GameEvents.OnEnterSacabambaspis>()
                    .Subscribe(x =>
                    {
                        gameData.score.Value += this.gameDesignData.GetSacabambaspisData(x.SacabambaspisType).Score;
                        gameData.baspisGauge.Value = Mathf.Clamp01(gameData.baspisGauge.Value + this.gameDesignData.BaspisGaugeData.OnEnterAmount);
                        if (gameData.baspisGauge.Value >= 1.0f && !gameData.isEnterFullBaspisMode)
                        {
                            // フルバスピスモード開始
                            gameData.isEnterFullBaspisMode = true;
                            MessageBroker.GetPublisher<GameEvents.BeginFullBaspisMode>()
                                .Publish(GameEvents.BeginFullBaspisMode.Get());
                        }
                    })
                    .AddTo(inGameTokenSource.Token);

                // サカバンバスピスが離れた際の処理
                MessageBroker.GetSubscriber<GameEvents.OnExitSacabambaspis>()
                    .Subscribe(x =>
                    {
                        if (x.IsEnteredPlayer)
                        {
                            gameData.score.Value -= this.gameDesignData.GetSacabambaspisData(x.SacabambaspisType).Score;
                        }
                    })
                    .AddTo(inGameTokenSource.Token);

                // フルバスピスモードが開始した際の処理
                MessageBroker.GetSubscriber<GameEvents.BeginFullBaspisMode>()
                    .Subscribe(async _ =>
                    {
                        try
                        {
                            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: inGameTokenSource.Token);
                            this.sacabambaspisSpawner.SpawnColorful(this.gameDesignData);
                            gameData.level.Value++;
                            gameData.baspisGauge.Value = 0.0f;
                            gameData.isEnterFullBaspisMode = false;
                        }
                        catch (OperationCanceledException)
                        {
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    })
                    .AddTo(inGameTokenSource.Token);

                // ゲームを開始した際の処理
                MessageBroker.GetSubscriber<GameEvents.BeginGame>()
                    .Subscribe(x =>
                    {
                        this.GetAsyncUpdateTrigger()
                            .Subscribe(_ =>
                            {
                                gameData.gameTimeSeconds.Value -= UnityEngine.Time.deltaTime;
                            })
                            .AddTo(x.GameScopeToken);
                    })
                    .AddTo(sceneToken);

                // ゲーム終了待ちの処理
                MessageBroker.GetAsyncSubscriber<GameEvents.TakeUntilEndGame>()
                    .Subscribe(async (_, ct) =>
                    {
                        await UniTask.WaitWhile(() => gameData.gameTimeSeconds.Value > 0.0f, cancellationToken: ct);
                    })
                    .AddTo(sceneToken);

                // ゲーム開始を通知する
                await MessageBroker.GetAsyncPublisher<GameEvents.NotifyBeginGame>()
                    .PublishAsync(GameEvents.NotifyBeginGame.Get(), sceneToken);

                // ゲームを開始する
                MessageBroker.GetPublisher<GameEvents.BeginGame>()
                    .Publish(GameEvents.BeginGame.Get(inGameTokenSource.Token));

                this.sacabambaspisSpawner.BeginSpawn(this.gameDesignData, gameData, inGameTokenSource.Token);

                // ゲームが終了するまで待機
                await MessageBroker.GetAsyncPublisher<GameEvents.TakeUntilEndGame>()
                    .PublishAsync(GameEvents.TakeUntilEndGame.Get(), sceneToken);

                inGameTokenSource.Cancel();
                inGameTokenSource.Dispose();

                // この段階でスクリーンショットを撮ってTexture2dにする
                var texture2d = ScreenCapture.CaptureScreenshotAsTexture();

                // ゲームオーバーシーン用にコンテキストを作る
                var context = new GameOverSceneContext(gameData.score.Value, texture2d);
                SceneContext.Set(context);

                // ゲーム終了を通知する
                await MessageBroker.GetAsyncPublisher<GameEvents.NotifyEndGame>()
                    .PublishAsync(GameEvents.NotifyEndGame.Get(), sceneToken);

                SceneManager.LoadScene("GameOver");
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
