using System;
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
        private SacabambaspisData normalSacabambaspisData;
        
        /// <summary>
        /// 色付きのサカバンバスピスに関するデータ
        /// </summary>
        [SerializeField]
        private SacabambaspisData colorfulSacabambaspisData;
        
        /// <summary>
        /// <paramref name="sacabambaspisType"/>から<see cref="SacabambaspisData"/>を返す
        /// </summary>
        public SacabambaspisData GetSacabambaspisData(Define.SacabambaspisType sacabambaspisType)
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
        public class SacabambaspisData
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
    }
}