using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Unity1Week0619.GameSystems
{
    /// <summary>
    /// ゲームシーンを制御するクラス
    /// </summary>
    public class GameSceneController : MonoBehaviour
    {
        [SerializeField]
        private SacabambaspisSpawner sacabambaspisSpawner;

        void Start()
        {
            var ct = this.GetCancellationTokenOnDestroy();
            this.sacabambaspisSpawner.BeginSpawn(ct);
        }
    }
}
