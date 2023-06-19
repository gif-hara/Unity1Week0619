using UnityEngine;

namespace Unity1Week0619
{
    /// <summary>
    /// サカバンバスピスを制御するクラス
    /// </summary>
    public class SacabambaspisController : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision.gameObject.name);
        }
    }
}
