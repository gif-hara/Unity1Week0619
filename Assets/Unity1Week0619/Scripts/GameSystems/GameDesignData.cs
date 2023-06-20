using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week0619.GameSystems
{
    [CreateAssetMenu(menuName = "Unity1Week0619/GameDesignData")]
    public class GameDesignData : ScriptableObject
    {
        /// <summary>
        /// 通常のサカバンバスピスに関するデータ
        /// </summary>
        [SerializeField]
        private SacabambaspisData_ normalSacabambaspisData;

        /// <summary>
        /// 色付きのサカバンバスピスに関するデータ
        /// </summary>
        [SerializeField]
        private SacabambaspisData_ colorfulSacabambaspisData;

        /// <summary>
        /// レベルに関するデータ
        /// </summary>
        [SerializeField]
        private LevelData_ levelData;

        /// <summary>
        /// バスピスゲージに関するデータ
        /// </summary>
        [SerializeField]
        private BaspisGaugeData_ baspisGaugeData;

        /// <summary>
        /// フルバスピスモードに関するデータ
        /// </summary>
        [SerializeField]
        private FullBaspisModeData_ fullBaspisModeData;

        /// <summary>
        /// <inheritdoc cref="levelData"/>
        /// </summary>
        public LevelData_ LevelData => this.levelData;

        /// <summary>
        /// <inheritdoc cref="baspisGaugeData"/>
        /// </summary>
        public BaspisGaugeData_ BaspisGaugeData => this.baspisGaugeData;

        /// <summary>
        /// <inheritdoc cref="fullBaspisModeData"/>
        /// </summary>
        public FullBaspisModeData_ FullBaspisModeData => this.fullBaspisModeData;

        /// <summary>
        /// <paramref name="sacabambaspisType"/>から<see cref="SacabambaspisData_"/>を返す
        /// </summary>
        public SacabambaspisData_ GetSacabambaspisData(Define.SacabambaspisType sacabambaspisType)
        {
            switch (sacabambaspisType)
            {
                case Define.SacabambaspisType.Normal:
                    return this.normalSacabambaspisData;
                case Define.SacabambaspisType.Colorful:
                    return this.colorfulSacabambaspisData;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sacabambaspisType), sacabambaspisType, null);
            }
        }

        /// <summary>
        /// サカバンバスピスに関するデータ
        /// </summary>
        [Serializable]
        public class SacabambaspisData_
        {
            /// <summary>
            /// 加減算されるスコア
            /// </summary>
            [SerializeField]
            private int score;

            /// <summary>
            /// 生成された時に再生されるサウンドデータリスト
            /// </summary>
            [SerializeField]
            private List<SoundEffectElement.AudioData> spawnAudioDataList;

            /// <summary>
            /// 登場した時に再生されるサウンドデータリスト
            /// </summary>
            [SerializeField]
            private List<SoundEffectElement.AudioData> appearanceAudioDataList;

            /// <summary>
            /// キャッチされた時に再生されるサウンドデータリスト
            /// </summary>
            [SerializeField]
            private List<SoundEffectElement.AudioData> onEnterAudioDataList;

            /// <summary>
            /// 離れた時に再生されるサウンドデータリスト
            /// </summary>
            [SerializeField]
            private List<SoundEffectElement.AudioData> onExitAudioDataList;

            /// <summary>
            /// <inheritdoc cref="score"/>
            /// </summary>
            public int Score => this.score;

            public List<SoundEffectElement.AudioData> SpawnAudioDataList => this.spawnAudioDataList;

            public List<SoundEffectElement.AudioData> AppearanceAudioDataList => this.appearanceAudioDataList;

            public List<SoundEffectElement.AudioData> OnEnterAudioDataList => this.onEnterAudioDataList;

            public List<SoundEffectElement.AudioData> OnExitAudioDataList => this.onExitAudioDataList;
        }

        /// <summary>
        /// レベルに関するデータ
        /// </summary>
        [Serializable]
        public class LevelData_
        {
            /// <summary>
            /// 生成する間隔（秒）
            /// </summary>
            [SerializeField]
            private List<float> spawnIntervalSeconds;

            /// <summary>
            /// レベルから生成する間隔秒数を返す
            /// </summary>
            public float GetSpawnIntervalSeconds(int level)
            {
                // インデックスは最大値を超えないように
                var index = level;
                if (index >= this.spawnIntervalSeconds.Count)
                {
                    index = this.spawnIntervalSeconds.Count - 1;
                }

                return this.spawnIntervalSeconds[index];
            }
        }

        /// <summary>
        /// バスピスゲージに関するデータ
        /// </summary>
        [Serializable]
        public class BaspisGaugeData_
        {
            /// <summary>
            /// ゲージの初期値
            /// </summary>
            [SerializeField, Range(0.0f, 1.0f)]
            private float initialAmount;

            /// <summary>
            /// サカバンバスピスをキャッチした際に加算される量
            /// </summary>
            [SerializeField]
            private float onEnterAmount;

            /// <summary>
            /// サカバンバスピスが離れた際に加算される量
            /// </summary>
            [SerializeField]
            private float onExitAmount;

            /// <summary>
            /// <inheritdoc cref="initialAmount"/>
            /// </summary>
            public float InitialAmount => this.initialAmount;

            /// <summary>
            /// <inheritdoc cref="onEnterAmount"/>
            /// </summary>
            public float OnEnterAmount => this.onEnterAmount;

            /// <summary>
            /// <inheritdoc cref="onExitAmount"/>
            /// </summary>
            public float OnExitAmount => this.onExitAmount;
        }

        /// <summary>
        /// フルバスピスモードに関するデータ
        /// </summary>
        [Serializable]
        public class FullBaspisModeData_
        {
            /// <summary>
            /// フルバスピスモードゲージが秒間で減少する量
            /// </summary>
            [SerializeField]
            private float decreaseAmountPerSeconds;

            /// <summary>
            /// キャッチした際に加算する量
            /// </summary>
            [SerializeField]
            private float onEnterAmount;

            /// <summary>
            /// 離れた際に加算する量
            /// </summary>
            [SerializeField]
            private float onExitAmount;

            /// <summary>
            /// <inheritdoc cref="decreaseAmountPerSeconds"/>
            /// </summary>
            public float DecreaseAmountPerSeconds => this.decreaseAmountPerSeconds;

            /// <summary>
            /// <inheritdoc cref="onEnterAmount"/>
            /// </summary>
            public float OnEnterAmount => this.onEnterAmount;

            /// <summary>
            /// <inheritdoc cref="onExitAmount"/>
            /// </summary>
            public float OnExitAmount => this.onExitAmount;
        }
    }
}
