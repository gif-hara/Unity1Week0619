using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity1Week0619.UISystems;
using Unity1Week0619.UISystems.Presenters;
using UnityEngine;

namespace Unity1Week0619.LicenseSystems
{
    public class LicenseSceneController : MonoBehaviour
    {
        [SerializeField]
        private LicenseUIView viewPrefab;

        private async void Start()
        {
            await BootSystem.IsReady;

            LicenseUIPresenter.Setup(viewPrefab, this.GetCancellationTokenOnDestroy());
        }
    }
}
