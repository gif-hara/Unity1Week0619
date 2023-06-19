using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Unity1Week0619.GameSystems
{
    public class SacabambaspisSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject sacabambaspisPrefab;

        [SerializeField]
        private float spawnInterval;

        [SerializeField]
        private Rect spawnArea;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            var center = this.transform.localPosition + new Vector3(spawnArea.center.x, spawnArea.center.y, 0f);
            Gizmos.DrawWireCube( center, spawnArea.size);
        }

        public void BeginSpawn(CancellationToken cancellationToken)
        {
            UniTask.Void(async _ =>
                {
                    while (true)
                    {
                        await UniTask.Delay(TimeSpan.FromSeconds(spawnInterval), cancellationToken: cancellationToken);
                        var position = this.transform.localPosition;
                        position.x += UnityEngine.Random.Range(spawnArea.xMin, spawnArea.xMax);
                        position.y += UnityEngine.Random.Range(spawnArea.yMin, spawnArea.yMax);
                        Instantiate(sacabambaspisPrefab, position, Quaternion.identity);
                    }
                },
                cancellationToken);
        }
    }
}
