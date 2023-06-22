using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Unity1Week0619.GameOverSystems
{
    /// <summary>
    /// ゲームオーバーシーンのデザインデータ
    /// </summary>
    [CreateAssetMenu(menuName = "Unity1Week0619/GameOverDesignData")]
    public class GameOverDesignData : ScriptableObject
    {
        [Serializable]
        public class CommentData
        {
            [SerializeField]
            private int scoreMin;

            [SerializeField]
            private int scoreMax;

            [SerializeField]
            private string comment;

            public int ScoreMin => scoreMin;

            public int ScoreMax => scoreMax;

            public string Comment => comment;
        }

        [SerializeField]
        private List<CommentData> commentDataList;

        public string GetComment(int score)
        {
            foreach (var commentData in this.commentDataList)
            {
                if (commentData.ScoreMin <= score && score <= commentData.ScoreMax)
                {
                    return commentData.Comment;
                }
            }

            Assert.IsTrue(false, $"{score}に対応するコメントがありません");
            return string.Empty;
        }
    }
}
