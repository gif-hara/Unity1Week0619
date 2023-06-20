using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week0619.Scripts.GameSystems
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
        /// <inheritdoc cref="levelData"/>
        /// </summary>
        public LevelData_ LevelData => this.levelData;
        
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
            /// キャッチした際に加算されるバスピスゲージの量
            /// </summary>
            [SerializeField]
            private float onEnterBaspisGauge;
            
            /// <summary>
            /// 離れた際に加算されるバスピスゲージの量
            /// </summary>
            [SerializeField]
            private float onExitBaspisGauge;
            
            /// <summary>
            /// <inheritdoc cref="score"/>
            /// </summary>
            public int Score => this.score;
            
            /// <summary>
            /// <inheritdoc cref="onEnterBaspisGauge"/>
            /// </summary>
            public float OnEnterBaspisGauge => this.onEnterBaspisGauge;
            
            /// <summary>
            /// <inheritdoc cref="onExitBaspisGauge"/>
            /// </summary>
            public float OnExitBaspisGauge => this.onExitBaspisGauge;
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
    }
}