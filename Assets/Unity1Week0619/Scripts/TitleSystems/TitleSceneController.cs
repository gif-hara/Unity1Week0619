using Cysharp.Threading.Tasks;
using Unity1Week0619.UISystems;
using Unity1Week0619.UISystems.Presenters;
using UnityEngine;

namespace Unity1Week0619.TitleSystems
{
    public class TitleSceneController : MonoBehaviour
    {
        [SerializeField]
        private TitleUIView titleUIView;

        [SerializeField]
        private AudioClip bgm;

        private async void Start()
        {
            await BootSystem.IsReady;

            var sceneToken = this.GetCancellationTokenOnDestroy();
            TitleUIPresenter.Setup(this.titleUIView, sceneToken);
            AudioManager.PlayBGM(this.bgm);
        }
    }
}
