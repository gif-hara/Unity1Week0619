using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
        /// ゲームの時間（秒）
        /// </summary>
        [SerializeField]
        private float gameTimeSeconds;

        public float GameTimeSeconds => gameTimeSeconds;

        /// <summary>
        /// <inheritdoc cref="levelData"/>
        /// </summary>
        public LevelData_ LevelData => this.levelData;

        /// <summary>
        /// <inheritdoc cref="baspisGaugeData"/>
        /// </summary>
        public BaspisGaugeData_ BaspisGaugeData => this.baspisGaugeData;

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
            [Serializable]
            public class AudioDataWithSerif
            {
                [SerializeField]
                private SoundEffectElement.AudioData audioData;

                [SerializeField]
                private string serif;

                public SoundEffectElement.AudioData AudioData => this.audioData;

                public string Serif => this.serif;
            }
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
            private List<AudioDataWithSerif> appearanceAudioDataList;

            /// <summary>
            /// キャッチされた時に再生されるサウンドデータリスト
            /// </summary>
            [SerializeField]
            private List<SoundEffectElement.AudioData> onEnterAudioDataList;

            /// <summary>
            /// 離れた時に再生されるサウンドデータリスト
            /// </summary>
            [SerializeField]
            private List<AudioDataWithSerif> onExitAudioDataList;

            [SerializeField]
            private float spawnDelaySeconds;

            [SerializeField]
            private Vector2 spawnVelocityMin;

            [SerializeField]
            private Vector2 spawnVelocityMax;

            [SerializeField]
            private float spawnAddTorqueMin;

            [SerializeField]
            private float spawnAddTorqueMax;

            /// <summary>
            /// <inheritdoc cref="score"/>
            /// </summary>
            public int Score => this.score;

            public List<SoundEffectElement.AudioData> SpawnAudioDataList => this.spawnAudioDataList;

            public List<AudioDataWithSerif> AppearanceAudioDataList => this.appearanceAudioDataList;

            public List<SoundEffectElement.AudioData> OnEnterAudioDataList => this.onEnterAudioDataList;

            public List<AudioDataWithSerif> OnExitAudioDataList => this.onExitAudioDataList;

            public float SpawnDelaySeconds => this.spawnDelaySeconds;

            public Vector2 SpawnVelocityMin => this.spawnVelocityMin;

            public Vector2 SpawnVelocityMax => this.spawnVelocityMax;

            public float SpawnAddTorqueMin => this.spawnAddTorqueMin;

            public float SpawnAddTorqueMax => this.spawnAddTorqueMax;
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
            /// サカバンバスピスをキャッチした際に加算される量
            /// </summary>
            [SerializeField]
            private float onEnterAmount;

            /// <summary>
            /// フルバスピスになった際に再生されるサウンド
            /// </summary>
            [SerializeField]
            private SoundEffectElement.AudioData fullBaspisModeClip;

            /// <summary>
            /// <inheritdoc cref="onEnterAmount"/>
            /// </summary>
            public float OnEnterAmount => this.onEnterAmount;

            /// <summary>
            /// <inheritdoc cref="fullBaspisModeClip"/>
            /// </summary>
            public SoundEffectElement.AudioData FullBaspisModeClip => this.fullBaspisModeClip;
        }
    }
}
