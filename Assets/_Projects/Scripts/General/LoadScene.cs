using System;
using UnityEngine.SceneManagement;

namespace General
{
    internal enum SceneName
    {
        Title,
        Config,
        Main
    }

    internal static class LoadScene
    {
        private static string ToSceneNameString(this SceneName sceneName)
            => sceneName switch
            {
                SceneName.Title => "Title",
                SceneName.Config => "Config",
                SceneName.Main => "Main",
                _ => throw new Exception("無効な形式です")
            };

        internal static void LoadSync(SceneName sceneName) => SceneManager.LoadScene(sceneName.ToSceneNameString());

        internal static bool IsInThisScene(SceneName sceneName)
            => SceneManager.GetActiveScene().name == sceneName.ToSceneNameString();
    }
}