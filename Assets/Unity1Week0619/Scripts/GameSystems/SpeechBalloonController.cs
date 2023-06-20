using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Unity1Week0619
{
    public class SpeechBalloonController : MonoBehaviour
    {
        [SerializeField]
        private AnimationController animationController;

        [SerializeField]
        private AnimationClip clip;

        [SerializeField]
        private TMP_Text text;

        public void PlayAnimation(string text)
        {
            this.text.text = text;
            this.animationController.Play(this.clip);
        }
    }
}
