using Data.General;
using IA;
using Interface;
using SO;
using TMPro;
using UnityEngine;
using Profiler = UnityEngine.Profiling.Profiler;

namespace General
{
    public sealed class ShortcutHandler : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI debugInfoText;

        private DebugInfoDisplayer debugInfoDisplayer;

        private InputGetter input => InputGetter.Instance;

        private void OnEnable()
        {
            debugInfoDisplayer = new(debugInfoText);
            debugInfoDisplayer.Start();
        }

        private void OnDisable()
        {
            debugInfoDisplayer.Dispose();
            debugInfoDisplayer = null;

            debugInfoText = null;
        }

        private void Update()
        {
            if (input.Shortcut_LoadTitleSceneClick.Bool)
            {
                LoadScene.LoadSync(SceneName.Title);
            }
            else if (input.Shortcut_LoadConfigSceneInTitleSceneClick.Bool && LoadScene.IsInThisScene(SceneName.Title))
            {
                LoadScene.LoadSync(SceneName.Config);
            }
            else if (input.Shortcut_TriggerScreenSizeClick.Bool)
            {
                var gameStates = SO_GameState.Entity.GameStates;
                if (gameStates is null) return;
                if (gameStates.Count <= 1) return;
                foreach (var e in gameStates) if (e is null) return;

                gameStates[GameState.LoopedIndex++].Apply();
            }
            else if (input.Shortcut_TriggerDebugInfoDisplayClick.Bool)
            {
                debugInfoText.enabled = !debugInfoText.enabled;
            }
            else if (input.Shortcut_QuitGame.Bool)
            {
                LoadScene.QuitGame();
            }

            if (debugInfoText.enabled) debugInfoDisplayer.Update();
        }
    }

    internal sealed class DebugInfoDisplayer : IHandler
    {
        private TextMeshProUGUI debugText;

        int cnt = 0;
        float preT = 0f;

        float fps = 0f;
        float allocatedMemory = 0f;
        float unusedReservedMemory = 0f;
        float reservedMemory = 0f;
        float memoryP = 0f;

        internal DebugInfoDisplayer(TextMeshProUGUI debugText) => this.debugText = debugText;
        public void Dispose() => debugText = null;
        public void Start()
        {
            if (debugText == null) return;

            if (debugText.enabled) debugText.enabled = false;
        }
        public void Update()
        {
            if (debugText == null) return;

            cnt++;
            float t = Time.realtimeSinceStartup - preT;
            if (t >= 0.5f)
            {
                fps = cnt / t;
                cnt = 0;
                preT = Time.realtimeSinceStartup;
            }

            allocatedMemory = Profiler.GetTotalAllocatedMemoryLong().ByteToMegabyte();
            unusedReservedMemory = Profiler.GetTotalUnusedReservedMemoryLong().ByteToMegabyte();
            reservedMemory = Profiler.GetTotalReservedMemoryLong().ByteToMegabyte();
            memoryP = allocatedMemory / reservedMemory;

            debugText.text = $" FPS: {fps:F2}\n Memory(MB): {allocatedMemory:F2}/{reservedMemory:F2} ({memoryP:P2}, {unusedReservedMemory:F2} unused)";
        }
    }

    internal static class DebugInfoDisplayerEx
    {
        internal static float ByteToMegabyte(this long n)
        {
            // KBˆÈ‰º‚ÍØ‚èŽÌ‚Ä
            return (n >> 10) / 1024f;
        }
    }
}