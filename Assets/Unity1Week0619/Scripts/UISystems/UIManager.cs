using Cysharp.Threading.Tasks;
using MessagePipe;
using Unity1Week0619.UISystems.Views;
using UnityEngine;
using UnityEngine.Assertions;

namespace Unity1Week0619.UISystems
{
    /// <summary>
    /// </summary>
    public sealed class UIManager : MonoBehaviour
    {
        [SerializeField]
        private RectTransform uiParent;

        [SerializeField]
        private Camera uiCamera;

        [SerializeField]
        private FadeUIView fadeUIViewPrefab;

        public static UIManager Instance { get; private set; }

        public static Camera UICamera => Instance.uiCamera;

        public static async UniTask SetupAsync()
        {
            Assert.IsNull(Instance);

            var prefab = await Resources.LoadAsync<UIManager>("UIManager");
            Instance = Instantiate((UIManager)prefab);
            DontDestroyOnLoad(Instance);

            var fadeUIView = Open(Instance.fadeUIViewPrefab);
            Hidden(fadeUIView);
            MessageBroker.GetAsyncSubscriber<SceneEvents.BeginFade>()
                .Subscribe(async (_, ct) =>
                {
                    Show(fadeUIView);
                    await fadeUIView.BeginFadeAsync(ct);
                });
        }

        public static T Open<T>(T uiViewPrefab) where T : UIView
        {
            return Instantiate(uiViewPrefab, Instance.uiParent);
        }

        public static void Close(UIView uiView)
        {
            Destroy(uiView.gameObject);
        }

        public static void Show(UIView uiView)
        {
            uiView.gameObject.SetActive(true);
        }

        public static void Hidden(UIView uiView)
        {
            uiView.gameObject.SetActive(false);
        }

        public static void SetAsLastSibling(UIView uiView)
        {
            uiView.transform.SetAsLastSibling();
        }
    }
}
