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
        internal static void LoadSync(SceneName sceneName) => SceneManager.LoadScene(sceneName.ToSceneNameString());

        private static string ToSceneNameString(this SceneName sceneName)
            => sceneName switch
            {
                SceneName.Title => "Title",
                SceneName.Config => "Config",
                SceneName.Main => "Main",
                _ => throw new Exception("–³Œø‚ÈŒ`Ž®‚Å‚·")
            };
    }
}