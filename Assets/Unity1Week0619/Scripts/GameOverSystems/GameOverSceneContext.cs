using System;
using UnityEngine;

namespace Unity1Week0619.GameOverSystems
{
    [Serializable]
    public class GameOverSceneContext : ISceneContext
    {
        [SerializeField]
        private int score;

        [SerializeField]
        private Texture2D screenShot;

        public int Score => score;

        public Texture2D ScreenShot => screenShot;

        public GameOverSceneContext(int score, Texture2D screenShot)
        {
            this.score = score;
            this.screenShot = screenShot;
        }
    }
}
