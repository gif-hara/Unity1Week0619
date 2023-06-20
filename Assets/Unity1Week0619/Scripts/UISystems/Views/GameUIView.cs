using System;
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

        [SerializeField]
        private SacabambaspisCountArea sacabambaspisCountArea;
        
        [SerializeField]
        private BaspisGaugeArea baspisGaugeArea;
        
        [SerializeField]
        private FullBaspisModeArea fullBaspisModeArea;

        public SacabambaspisCountArea SacabambaspisCount => sacabambaspisCountArea;
        
        public BaspisGaugeArea BaspisGauge => baspisGaugeArea;
        
        public FullBaspisModeArea FullBaspisMode => fullBaspisModeArea;
    }
}
