using Cysharp.Threading.Tasks;
using Unity1Week0619.UISystems;
using Unity1Week0619.UISystems.Presenters;
using UnityEngine;

namespace Unity1Week0619.GameOverSystems
{
    public class GameOverSceneController : MonoBehaviour
    {
        [SerializeField]
        private GameOverUIView gameOverUIView;
        
        private void Start()
        {
            var sceneToken = this.GetCancellationTokenOnDestroy();
            GameOverUIPresenter.Setup(this.gameOverUIView, 999, sceneToken);
        }
    }
}