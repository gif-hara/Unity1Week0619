using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
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
        private GameDesignData gameDesignData;
        
        [SerializeField]
        private SacabambaspisSpawner sacabambaspisSpawner;

        [SerializeField]
        private GameUIView gameUIView;

        async void Start()
        {
            await BootSystem.IsReady;

            var ct = this.GetCancellationTokenOnDestroy();
            this.sacabambaspisSpawner.BeginSpawn(this.gameDesignData, ct);

            var gameUIPresenter = new GameUIPresenter(this.gameUIView);

            var score = 0;
            var baspisGauge = this.gameDesignData.BaspisGaugeData.InitialAmount;
            var fullBaspisModeGauge = 0.0f;
            IDisposable fullBaspisModeStream = null;
            gameUIPresenter.SetSacabambaspisCount(score);
            MessageBroker.GetSubscriber<GameEvents.OnEnterSacabambaspis>()
                .Subscribe(x =>
                {
                    score += this.gameDesignData.GetSacabambaspisData(x.SacabambaspisType).Score;
                    baspisGauge = Mathf.Clamp01(baspisGauge + this.gameDesignData.BaspisGaugeData.OnEnterAmount);
                    if(fullBaspisModeStream != null)
                    {
                        fullBaspisModeGauge = Mathf.Clamp01(fullBaspisModeGauge + this.gameDesignData.FullBaspisModeData.OnEnterAmount);
                    }
                    gameUIPresenter.SetSacabambaspisCount(score);
                    gameUIPresenter.SetBaspisGauge(baspisGauge);

                    if (baspisGauge >= 1.0f && fullBaspisModeStream == null)
                    {
                        // フルバスピスモード開始
                        MessageBroker.GetPublisher<GameEvents.BeginFullBaspisMode>()
                            .Publish(GameEvents.BeginFullBaspisMode.Get());
                        fullBaspisModeGauge = 1.0f;
                        fullBaspisModeStream = this.GetAsyncUpdateTrigger()
                            .Subscribe(_ =>
                            {
                                fullBaspisModeGauge -= this.gameDesignData.FullBaspisModeData.DecreaseAmountPerSeconds * UnityEngine.Time.deltaTime;
                                if (fullBaspisModeGauge <= 0.0f)
                                {
                                    // フルバスピスモード終了
                                    fullBaspisModeStream?.Dispose();
                                    fullBaspisModeStream = null;
                                    baspisGauge = this.gameDesignData.BaspisGaugeData.InitialAmount;
                                    gameUIPresenter.SetBaspisGauge(baspisGauge);
                                    MessageBroker.GetPublisher<GameEvents.EndFullBaspisMode>()
                                        .Publish(GameEvents.EndFullBaspisMode.Get());
                                }
                            });
                    }
                })
                .AddTo(ct);
            MessageBroker.GetSubscriber<GameEvents.OnExitSacabambaspis>()
                .Subscribe(x =>
                {
                    score -= this.gameDesignData.GetSacabambaspisData(x.SacabambaspisType).Score;
                    baspisGauge = Mathf.Clamp01(baspisGauge - this.gameDesignData.BaspisGaugeData.OnExitAmount);
                    if(fullBaspisModeStream != null)
                    {
                        fullBaspisModeGauge = Mathf.Clamp01(fullBaspisModeGauge - this.gameDesignData.FullBaspisModeData.OnExitAmount);
                    }
                    gameUIPresenter.SetSacabambaspisCount(score);
                    gameUIPresenter.SetBaspisGauge(baspisGauge);
                })
                .AddTo(ct);
        }
    }
}
