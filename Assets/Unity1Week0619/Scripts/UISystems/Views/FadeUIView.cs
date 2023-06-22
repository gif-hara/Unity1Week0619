using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Unity1Week0619.UISystems.Views
{
    public class FadeUIView : UIView
    {
        [SerializeField]
        private Animator animator;

        public async UniTask BeginFadeAsync(CancellationToken cancellationToken)
        {
            this.animator.enabled = false;

            await UniTask.DelayFrame(1, cancellationToken: cancellationToken);

            this.animator.enabled = true;
            this.animator.Play("Fade", 0, 0.0f);

            // アニメーションが半分終わるまで待つ
            await UniTask.Delay(
                TimeSpan.FromSeconds(this.animator.GetCurrentAnimatorStateInfo(0).length / 2),
                cancellationToken: cancellationToken
                );
        }
    }
}
