using Cysharp.Threading.Tasks;
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
                UniTask.DelayFrame(1)
                );

            IsReady = UniTask.CompletedTask;
        }

        private static UniTask SetupMessageBrokerAsync()
        {
            return UniTask.CompletedTask;
        }
    }
}
