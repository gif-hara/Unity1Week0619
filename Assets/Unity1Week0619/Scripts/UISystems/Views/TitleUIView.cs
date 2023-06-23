using UnityEngine;
using UnityEngine.UI;

namespace Unity1Week0619.UISystems
{
    public class TitleUIView : UIView
    {
        /// <summary>
        /// ゲームを開始するボタン
        /// </summary>
        [SerializeField]
        private Button gameStartButton;

        [SerializeField]
        private Button licenseButton;

        [SerializeField]
        private Animator animator;

        /// <summary>
        /// <inheritdoc cref="gameStartButton"/>
        /// </summary>
        public Button GameStartButton => gameStartButton;

        public Button LicenseButton => licenseButton;

        public Animator Animator => animator;
    }
}
