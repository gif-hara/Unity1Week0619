using UnityEngine;

namespace Unity1Week0619.GameSystems
{
    /// <summary>
    /// プレイヤーを制御するクラス
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D targetRigidbody2D;

        [SerializeField]
        private float smoothDamp;

        private Vector2 velocity;

        // マウスの座標と同期を取る
        private void Update()
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            var pos2D = new Vector2(pos.x, pos.y);

            // Rigidbody2dに対して、SmoothDampを使って移動させる
            targetRigidbody2D.velocity = (pos2D - targetRigidbody2D.position) * this.smoothDamp;
        }
    }
}
