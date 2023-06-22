using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Unity1Week0619
{
    /// <summary>
    /// 指定した秒数で死亡するクラス
    /// </summary>
    public class AutoDestroyFromSeconds : MonoBehaviour
    {
        [SerializeField]
        private float delaySeconds;

        private async void Start()
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(this.delaySeconds), cancellationToken: this.GetCancellationTokenOnDestroy());
                if (this.gameObject == null)
                {
                    return;
                }
                Destroy(this.gameObject);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    }
}
