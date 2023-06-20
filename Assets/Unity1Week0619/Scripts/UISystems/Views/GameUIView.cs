using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1Week0619.UISystems
{
    public class GameUIView : UIView
    {
        /// <summary>
        /// サカバンバスピスカウントのエリア
        /// </summary>
        [Serializable]
        public class SacabambaspisCountArea
        {
            [SerializeField]
            private TMP_Text countText;

            public TMP_Text CountText => countText;
        }

        /// <summary>
        /// バスピスゲージのエリア
        /// </summary>
        [Serializable]
        public class BaspisGaugeArea
        {
            [SerializeField]
            private Slider gauge;

            public Slider Gauge => gauge;
        }

        /// <summary>
        /// フルバスピスモードのエリア
        /// </summary>
        [Serializable]
        public class FullBaspisModeArea
        {
            [SerializeField]
            private Slider gauge;

            public Slider Gauge => gauge;
        }

        /// <summary>
        /// メッセージを表示するエリア
        /// </summary>
        [Serializable]
        public class MessageArea
        {
            [SerializeField]
            private GameObject root;

            [SerializeField]
            private TMP_Text text;

            [SerializeField]
            private AnimationController animationController;

            [SerializeField]
            private AnimationClip showAnimationClip;

            public async UniTask ShowAsync(string message)
            {
                this.text.text = message;
                this.root.SetActive(true);
                await this.animationController.PlayAsync(this.showAnimationClip);
                this.root.SetActive(false);
            }

            public void SetActive(bool isActive)
            {
                this.root.SetActive(isActive);
            }
        }

        [SerializeField]
        private SacabambaspisCountArea sacabambaspisCountArea;

        [SerializeField]
        private BaspisGaugeArea baspisGaugeArea;

        [SerializeField]
        private FullBaspisModeArea fullBaspisModeArea;

        [SerializeField]
        private MessageArea messageArea;

        public SacabambaspisCountArea SacabambaspisCount => sacabambaspisCountArea;

        public BaspisGaugeArea BaspisGauge => baspisGaugeArea;

        public FullBaspisModeArea FullBaspisMode => fullBaspisModeArea;

        public MessageArea Message => messageArea;
    }
}
