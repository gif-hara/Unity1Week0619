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
        /// スクリーンショットを映すイメージ
        /// </summary>
        [SerializeField]
        private RawImage screenShot;

        /// <summary>
        /// コメント
        /// </summary>
        [SerializeField]
        private TMP_Text comment;

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

        /// <summary>
        /// <inheritdoc cref="screenShot"/>
        /// </summary>
        public RawImage ScreenShot => screenShot;

        /// <summary>
        /// <inheritdoc cref="comment"/>
        /// </summary>
        public TMP_Text Comment => comment;
    }
}
