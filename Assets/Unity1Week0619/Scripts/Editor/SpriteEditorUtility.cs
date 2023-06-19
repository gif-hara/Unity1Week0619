using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.XR;

namespace Unity1Week0619.Editor
{
    public class SpriteEditorUtility
    {
        [MenuItem("Unity1Week0619/GetSelectedBones")]
        public static void GetSelectedBones()
        {
            foreach (var gameObject in Selection.gameObjects)
            {
                var spriteSkin = gameObject.GetComponent<SpriteSkin>();
                if (spriteSkin == null)
                {
                    spriteSkin = gameObject.AddComponent<SpriteSkin>();
                }

                for (var i = 0; i < spriteSkin.boneTransforms.Length; i++)
                {
                    var boneTransform = spriteSkin.boneTransforms[i];
                    foreach (var springJoint2D in boneTransform.GetComponents<SpringJoint2D>())
                    {
                        Object.DestroyImmediate(springJoint2D);
                    }
                    var joint = boneTransform.AddComponent<SpringJoint2D>();
                    joint.connectedBody = gameObject.GetComponent<Rigidbody2D>();
                    joint = boneTransform.AddComponent<SpringJoint2D>();
                    joint.connectedBody = spriteSkin.boneTransforms[(i - 1) <= -1 ? spriteSkin.boneTransforms.Length - 1 : i - 1].GetComponent<Rigidbody2D>();
                    joint = boneTransform.AddComponent<SpringJoint2D>();
                    joint.connectedBody = spriteSkin.boneTransforms[(i + 1) % spriteSkin.boneTransforms.Length].GetComponent<Rigidbody2D>();
                }
            }
        }
    }

}
