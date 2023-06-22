using Cysharp.Threading.Tasks;

namespace Unity1Week0619
{
    public class SceneManager
    {
        public static async UniTask LoadSceneAsync(string sceneName)
        {
            await MessageBroker.GetAsyncPublisher<SceneEvents.BeginFade>()
                .PublishAsync(SceneEvents.BeginFade.Get());

            MessageBroker.GetPublisher<SceneEvents.BeginLoad>()
                .Publish(SceneEvents.BeginLoad.Get());
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            MessageBroker.GetPublisher<SceneEvents.EndLoad>()
                .Publish(SceneEvents.EndLoad.Get());
        }

        public static void LoadScene(string sceneName)
        {
            LoadSceneAsync(sceneName).Forget();
        }
    }
}
