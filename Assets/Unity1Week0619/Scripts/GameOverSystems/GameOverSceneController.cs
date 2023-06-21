using Cysharp.Threading.Tasks;
using Unity1Week0619.UISystems;
using Unity1Week0619.UISystems.Presenters;
using UnityEngine;

namespace Unity1Week0619.GameOverSystems
{
    public class GameOverSceneController : MonoBehaviour
    {
        [SerializeField]
        private GameOverSceneContext debugContext;
        
        [SerializeField]
        private GameOverUIView gameOverUIView;
        
        private void Start()
        {
            var sceneToken = this.GetCancellationTokenOnDestroy();
            var context = SceneContext.Get<GameOverSceneContext>();
            if (context == null)
            {
                Debug.LogWarning("シーンコンテキストがありません。デバッグ用のコンテキストを使用します");
                context = this.debugContext;
            }
            GameOverUIPresenter.Setup(this.gameOverUIView, context.Score, sceneToken);
        }
    }
}