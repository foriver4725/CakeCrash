using UnityEngine.SceneManagement;

namespace General
{
    internal static class LoadScene
    {
        internal static void LoadSync(SceneName sceneName) => SceneManager.LoadScene(sceneName.ToString());
    }
}