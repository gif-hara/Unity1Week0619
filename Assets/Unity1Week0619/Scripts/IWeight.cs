using System;
using UnityEngine;

namespace Unity1Week0619
{
    /// <summary>
    /// 重みを持つインターフェイス
    /// </summary>
    public interface IWeight
    {
        /// <summary>
        /// 重み
        /// </summary>
        int Weight { get; }
    }

    [Serializable]
    public struct WeightData : IWeight
    {
        [SerializeField]
        private int weight;

        public int Weight => this.weight;
    }
}
