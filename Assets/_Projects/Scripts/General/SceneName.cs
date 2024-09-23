using System;

namespace General
{
    internal enum SceneName
    {
        Title,
        Config,
        Main
    }

    internal static class SceneNameEnumToSceneNameString
    {
        internal static string ToString(this SceneName sceneName)
            => sceneName switch
            {
                SceneName.Title => "Title",
                SceneName.Config => "Config",
                SceneName.Main => "Main",
                _ => throw new Exception("–³Œø‚ÈŒ`Ž®‚Å‚·")
            };
    }
}