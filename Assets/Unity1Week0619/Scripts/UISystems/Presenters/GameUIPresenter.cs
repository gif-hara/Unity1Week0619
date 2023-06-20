namespace Unity1Week0619.UISystems.Presenters
{
    public class GameUIPresenter
    {
        private GameUIView View { get; }

        public GameUIPresenter(GameUIView viewPrefab)
        {
            this.View = UIManager.Open(viewPrefab);
        }

        /// <summary>
        /// サカバンバスピスカウントを設定する
        /// </summary>
        public void SetSacabambaspisCount(int count)
        {
            // ローカライズは必要になったらする
            this.View.SacabambaspisCount.CountText.text = $"{count}バスピス！";
        }
        
        /// <summary>
        /// バスピスゲージの量を設定する
        /// </summary>
        public void SetBaspisGauge(float value)
        {
            this.View.BaspisGauge.Gauge.value = value;
        }
        
        /// <summary>
        /// フルバスピスモードのゲージの量を設定する
        /// </summary>
        public void SetFullBaspisModeGauge(float value)
        {
            this.View.FullBaspisMode.Gauge.value = value;
        }
    }
}
