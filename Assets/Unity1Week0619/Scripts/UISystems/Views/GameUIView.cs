using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1Week0619.UISystems
{
    public class GameUIView : UIView
    {
        [Serializable]
        public class SacabambaspisCountArea
        {
            [SerializeField]
            private TMP_Text countText;

            public TMP_Text CountText => countText;
        }

        [Serializable]
        public class BaspisGaugeArea
        {
            [SerializeField]
            private Slider gauge;
            
            public Slider Gauge => gauge;
        }

        [SerializeField]
        private SacabambaspisCountArea sacabambaspisCountArea;
        
        [SerializeField]
        private BaspisGaugeArea baspisGaugeArea;

        public SacabambaspisCountArea SacabambaspisCount => sacabambaspisCountArea;
        
        public BaspisGaugeArea BaspisGauge => baspisGaugeArea;
    }
}
