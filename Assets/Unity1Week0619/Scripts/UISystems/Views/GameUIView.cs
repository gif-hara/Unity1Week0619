using System;
using TMPro;
using UnityEngine;

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

        [SerializeField]
        private SacabambaspisCountArea sacabambaspisCountArea;

        public SacabambaspisCountArea SacabambaspisCount => sacabambaspisCountArea;
    }
}
