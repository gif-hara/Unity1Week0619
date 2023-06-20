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
            private float delaySeconds;
            
            public async UniTask ShowAsync()
            {
                this.root.SetActive(true);
                await UniTask.Delay(TimeSpan.FromSeconds(this.delaySeconds));
                this.root.SetActive(false);
            }
        }

        [SerializeField]
        private SacabambaspisCountArea sacabambaspisCountArea;
        
        [SerializeField]
        private BaspisGaugeArea baspisGaugeArea;
        
        [SerializeField]
        private FullBaspisModeArea fullBaspisModeArea;

        [SerializeField]
        private MessageArea gameStartMessageArea;

        public SacabambaspisCountArea SacabambaspisCount => sacabambaspisCountArea;
        
        public BaspisGaugeArea BaspisGauge => baspisGaugeArea;
        
        public FullBaspisModeArea FullBaspisMode => fullBaspisModeArea;
        
        public MessageArea GameStartMessage => gameStartMessageArea;
    }
}
