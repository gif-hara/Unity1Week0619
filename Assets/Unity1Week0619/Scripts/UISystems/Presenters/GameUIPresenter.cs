﻿namespace Unity1Week0619.UISystems.Presenters
{
    public class GameUIPresenter
    {
        private GameUIView View { get; }

        public GameUIPresenter(GameUIView viewPrefab)
        {
            this.View = UIManager.Open(viewPrefab);
        }

        public void SetSacabambaspisCount(int count)
        {
            // ローカライズは必要になったらする
            this.View.SacabambaspisCount.CountText.text = $"{count}バスピス！";
        }
    }
}