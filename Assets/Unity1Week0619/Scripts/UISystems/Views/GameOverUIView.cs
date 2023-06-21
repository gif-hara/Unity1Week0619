using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1Week0619.UISystems
{
    public class GameOverUIView : UIView
    {
        /// <summary>
        /// スコアテキスト
        /// </summary>
        [SerializeField]
        private TMP_Text scoreText;

        /// <summary>
        /// リトライボタン
        /// </summary>
        [SerializeField]
        private Button retryButton;
        
        /// <summary>
        /// タイトルボタン
        /// </summary>
        [SerializeField]
        private Button titleButton;
        
        /// <summary>
        /// <inheritdoc cref="scoreText"/>
        /// </summary>
        public TMP_Text ScoreText => scoreText;
        
        /// <summary>
        /// <inheritdoc cref="retryButton"/>
        /// </summary>
        public Button RetryButton => retryButton;
        
        /// <summary>
        /// <inheritdoc cref="titleButton"/>
        /// </summary>
        public Button TitleButton => titleButton;
    }
}