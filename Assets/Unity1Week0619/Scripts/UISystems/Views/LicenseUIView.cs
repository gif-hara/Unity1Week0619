using UnityEngine;
using UnityEngine.UI;

namespace Unity1Week0619.UISystems
{
    public class LicenseUIView : UIView
    {
        /// <summary>
        /// タイトルボタン
        /// </summary>
        [SerializeField]
        private Button titleButton;

        /// <summary>
        /// <inheritdoc cref="titleButton"/>
        /// </summary>
        public Button TitleButton => titleButton;
    }
}
