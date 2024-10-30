using UnityEditor;
using UnityEngine.SceneManagement;

namespace General
{
    internal enum SceneName
    {
        Title,
        Config,
        Main,
        Clear
    }

    internal static class LoadScene
    {
        private static string ToSceneNameString(this SceneName sceneName)
            => sceneName switch
            {
                SceneName.Title => "Title",
                SceneName.Config => "Config",
                SceneName.Main => "Main",
                SceneName.Clear => "Clear",
                _ => throw new()
            };

        internal static void LoadSync(SceneName sceneName)
        {
            GameState.IsPaused = false;
            SceneManager.LoadScene(sceneName.ToSceneNameString());
        }

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