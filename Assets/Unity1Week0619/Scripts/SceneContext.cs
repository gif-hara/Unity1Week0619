using UnityEngine;

namespace Unity1Week0619
{
    public static class SceneContext
    {
        private static ISceneContext current;

        public static T Get<T>() where T : class, ISceneContext
        {
            return current as T;
        }

        public static void Set(ISceneContext context)
        {
            current = context;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Clear()
        {
            current = null;
        }
    }

    /// <summary>
    /// シーンコンテキストのインターフェイス
    /// </summary>
    public interface ISceneContext
    {
    }
}
