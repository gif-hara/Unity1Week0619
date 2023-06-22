using Cysharp.Threading.Tasks;

namespace Unity1Week0619.GameSystems
{
    public class GameData
    {
        /// <summary>
        /// レベル
        /// </summary>
        public readonly AsyncReactiveProperty<int> level = new(0);

        /// <summary>
        /// スコア
        /// </summary>
        public readonly AsyncReactiveProperty<int> score = new(0);

        /// <summary>
        /// バスピスゲージ
        /// </summary>
        public readonly AsyncReactiveProperty<float> baspisGauge = new(0.0f);

        /// <summary>
        /// ゲーム時間
        /// </summary>
        public readonly AsyncReactiveProperty<float> gameTimeSeconds = new(0.0f);

        /// <summary>
        /// フルバスビスモード中かどうか
        /// </summary>
        public bool isEnterFullBaspisMode = false;
    }
}
