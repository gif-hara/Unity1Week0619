using UnityEngine;

namespace Unity1Week0619.GameSystems
{
    /// <summary>
    /// プレイヤーを制御するクラス
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Camera targetCamera;
        
        [SerializeField]
        private Rigidbody2D targetRigidbody2D;

        [SerializeField]
        private float smoothDamp;

        private void Update()
        {
            var pos = this.targetCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            var pos2D = new Vector2(pos.x, pos.y);

            targetRigidbody2D.velocity = (pos2D - targetRigidbody2D.position) * this.smoothDamp;
        }
    }
}
