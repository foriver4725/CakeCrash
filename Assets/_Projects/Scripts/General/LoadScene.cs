using System;
using UnityEditor;
using UnityEngine;
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
                _ => throw new Exception("–³Œø‚ÈŒ`Ž®‚Å‚·")
            };

        internal static void LoadSync(SceneName sceneName) => SceneManager.LoadScene(sceneName.ToSceneNameString());

        internal static bool IsInThisScene(SceneName sceneName)
            => SceneManager.GetActiveScene().name == sceneName.ToSceneNameString();

        internal static void QuitGame() =>
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}