﻿using Cysharp.Threading.Tasks;
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
            gameUIPresenter.SetSacabambaspisCount(score);
            MessageBroker.GetSubscriber<GameEvents.OnEnterSacabambaspis>()
                .Subscribe(x =>
                {
                    score += this.gameDesignData.GetSacabambaspisData(x.SacabambaspisType).Score;
                    baspisGauge += this.gameDesignData.BaspisGaugeData.OnEnterAmount;
                    gameUIPresenter.SetSacabambaspisCount(score);
                    gameUIPresenter.SetBaspisGauge(baspisGauge);
                })
                .AddTo(ct);
            MessageBroker.GetSubscriber<GameEvents.OnExitSacabambaspis>()
                .Subscribe(x =>
                {
                    score -= this.gameDesignData.GetSacabambaspisData(x.SacabambaspisType).Score;
                    baspisGauge -= this.gameDesignData.BaspisGaugeData.OnExitAmount;
                    gameUIPresenter.SetSacabambaspisCount(score);
                    gameUIPresenter.SetBaspisGauge(baspisGauge);
                })
                .AddTo(ct);
        }
    }
}
