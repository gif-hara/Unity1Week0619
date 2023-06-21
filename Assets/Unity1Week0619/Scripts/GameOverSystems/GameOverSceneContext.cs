using System;
using UnityEngine;

namespace Unity1Week0619.GameOverSystems
{
    [Serializable]
    public class GameOverSceneContext : ISceneContext
    {
        [SerializeField]
        private int score;
        
        public int Score => score;
        
        public GameOverSceneContext(int score)
        {
            this.score = score;
        }
    }
}