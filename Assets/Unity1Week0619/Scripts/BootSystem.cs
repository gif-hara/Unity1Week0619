using Cysharp.Threading.Tasks;
using Unity1Week0619.GameSystems;
using Unity1Week0619.UISystems;
using UnityEngine;

namespace Unity1Week0619
{
    /// <summary>
    /// ブートシステム
    /// </summary>
    public static class BootSystem
    {
        public static UniTask IsReady { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitializeOnBeforeSplashScreen()
        {
            IsReady = SetupInternal();
        }

        private static async UniTask SetupInternal()
        {
            await UniTask.WhenAll(
                SetupMessageBrokerAsync(),
                UIManager.SetupAsync(),
                AudioManager.SetupAsync(),
                UniTask.DelayFrame(1)
                );

            IsReady = UniTask.CompletedTask;
        }

        private static UniTask SetupMessageBrokerAsync()
        {
            MessageBroker.Setup(builder =>
            {
                SceneEvents.RegisterEvents(builder);
                GameEvents.RegisterEvents(builder);
            });
            return UniTask.CompletedTask;
        }
    }
}