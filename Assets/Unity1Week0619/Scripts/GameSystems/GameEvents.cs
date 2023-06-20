using MessagePipe;

namespace Unity1Week0619.Scripts.GameSystems
{
    public class GameEvents
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
        public sealed class OnExitSacabambaspis : Message<OnExitSacabambaspis, Define.SacabambaspisType>
        {
            public Define.SacabambaspisType SacabambaspisType => this.Param1;
        }

        /// <summary>
        /// イベントを登録する
        /// </summary>
        public static void RegisterEvents(BuiltinContainerBuilder builder)
        {
            builder.AddMessageBroker<OnEnterSacabambaspis>();
            builder.AddMessageBroker<OnExitSacabambaspis>();
        }
    }
}
