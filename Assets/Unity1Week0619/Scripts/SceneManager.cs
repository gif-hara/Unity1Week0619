namespace Unity1Week0619
{
    public class SceneManager
    {
        public static void LoadScene(string sceneName)
        {
            MessageBroker.GetPublisher<SceneEvents.BeginLoad>()
                .Publish(SceneEvents.BeginLoad.Get());
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            MessageBroker.GetPublisher<SceneEvents.EndLoad>()
                .Publish(SceneEvents.EndLoad.Get());
        }
    }    
}