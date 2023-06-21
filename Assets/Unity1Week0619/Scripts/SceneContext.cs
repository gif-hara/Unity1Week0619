using UnityEngine.Assertions;

namespace Unity1Week0619
{
    public static class SceneContext
    {
        private static ISceneContext current;
        
        public static T Get<T>() where T : class, ISceneContext
        {
            var result = current as T;
            Assert.IsNotNull(result, $"シーンコンテキストが{typeof(T).Name}ではありません");
            return result;
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