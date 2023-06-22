using System.Collections;
using System.Collections.Generic;
using MessagePipe;
using Unity1Week0619.GameSystems;
using UnityEngine;

namespace Unity1Week0619
{
    /// <summary>
    /// シーンに関するイベント
    /// </summary>
    public static class SceneEvents
    {
        /// <summary>
        /// シーン読み込みを開始した際のイベント
        /// </summary>
        public sealed class BeginLoad : Message<BeginLoad>
        {
        }

        /// <summary>
        /// シーン読み込みを終了した際のイベント
        /// </summary>
        public sealed class EndLoad : Message<EndLoad>
        {
        }

        public sealed class BeginFade : Message<BeginFade>
        {
        }

        /// <summary>
        /// イベントを登録する
        /// </summary>
        public static void RegisterEvents(BuiltinContainerBuilder builder)
        {
            builder.AddMessageBroker<BeginLoad>();
            builder.AddMessageBroker<EndLoad>();
            builder.AddMessageBroker<BeginFade>();
        }
    }
}
