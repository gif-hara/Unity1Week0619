using UnityEngine;

namespace Unity1Week0619
{
    public class OnClickInstantiate : MonoBehaviour
    {
        // クリックした座標にプレハブを生成する
        [SerializeField] GameObject prefab;

        [SerializeField]
        private Camera targetCamera;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var pos = Input.mousePosition;
                pos.z = 10f;
                var obj = Instantiate(prefab);
                obj.transform.position = this.targetCamera.ScreenToWorldPoint(pos);
            }
        }
    }
}
