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
        private GameOverDesignData designData;

        [SerializeField]
        private GameOverUIView gameOverUIView;

        private async void Start()
        {
            await BootSystem.IsReady;

            var sceneToken = this.GetCancellationTokenOnDestroy();
            var context = SceneContext.Get<GameOverSceneContext>();
            if (context == null)
            {
                Debug.LogWarning("シーンコンテキストがありません。デバッグ用のコンテキストを使用します");
                context = this.debugContext;
            }
            var comment = this.designData.GetComment(context.Score);
            GameOverUIPresenter.Setup(
                this.gameOverUIView,
                context.Score,
                context.ScreenShot,
                comment,
                sceneToken
                );
        }
    }
}
