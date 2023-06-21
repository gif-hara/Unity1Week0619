using System.Threading;
using MessagePipe;
using UnityEngine;

namespace Unity1Week0619.GameSystems
{
    public static class GameEvents
    {
        /// <summary>
        /// サカバンバスピスが器に入った際のイベント
        /// </summary>
        public sealed class OnEnterSacabambaspis : Message<OnEnterSacabambaspis, Define.SacabambaspisType>
        {
            public Define.SacabambaspisType SacabambaspisType => this.Param1;
        }

        /// <summary>
        /// サカバンバスピスが器から出た際のイベント
        /// </summary>
        public sealed class OnExitSacabambaspis : Message<OnExitSacabambaspis, Define.SacabambaspisType, bool, Vector3, string>
        {
            public Define.SacabambaspisType SacabambaspisType => this.Param1;

            /// <summary>
            /// プレイヤーの中に入ったか
            /// </summary>
            public bool IsEnteredPlayer => this.Param2;

            public Vector3 Position => this.Param3;

            public string Serif => this.Param4;
        }

        /// <summary>
        /// フルバスピスモードが開始した際のイベント
        /// </summary>
        public sealed class BeginFullBaspisMode : Message<BeginFullBaspisMode>
        {
        }

        /// <summary>
        /// フルバスピスモードが終了した際のイベント
        /// </summary>
        public sealed class EndFullBaspisMode : Message<EndFullBaspisMode>
        {
        }

        /// <summary>
        /// ゲーム開始を通知する際のイベント
        /// </summary>
        public sealed class NotifyBeginGame : Message<NotifyBeginGame>
        {
        }
        
        /// <summary>
        /// ゲームを開始した際のイベント
        /// </summary>
        public sealed class BeginGame : Message<BeginGame, CancellationToken>
        {
            /// <summary>
            /// ゲームのスコープ
            /// </summary>
            public CancellationToken GameScopeToken => this.Param1;
        }

        /// <summary>
        /// ゲームが終了するまで待つイベント
        /// </summary>
        public sealed class TakeUntilEndGame : Message<TakeUntilEndGame>
        {
        }

        /// <summary>
        /// ゲーム終了を通知する際のイベント
        /// </summary>
        public sealed class NotifyEndGame : Message<NotifyEndGame>
        {
        }

        /// <summary>
        /// イベントを登録する
        /// </summary>
        public static void RegisterEvents(BuiltinContainerBuilder builder)
        {
            builder.AddMessageBroker<OnEnterSacabambaspis>();
            builder.AddMessageBroker<OnExitSacabambaspis>();
            builder.AddMessageBroker<BeginFullBaspisMode>();
            builder.AddMessageBroker<EndFullBaspisMode>();
            builder.AddMessageBroker<NotifyBeginGame>();
            builder.AddMessageBroker<BeginGame>();
            builder.AddMessageBroker<TakeUntilEndGame>();
            builder.AddMessageBroker<NotifyEndGame>();
        }
    }
}