using UnityEngine.Assertions;

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
    }

    /// <summary>
    /// シーンコンテキストのインターフェイス
    /// </summary>
    public interface ISceneContext
    {
    }
}
