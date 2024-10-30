using Interface;
using SO;
using System;
using UnityEngine;

namespace General
{
    internal sealed class GameState : IProperty
    {
        internal Vector2Int Resolution { get; private set; }
        internal bool IsFullScreen { get; private set; }
        internal (bool isVsyncOn, int targetFrameRate) Display { get; private set; }

        internal GameState(Vector2Int resolution, bool isFullScreen, (bool isVsyncOn, int targetFrameRate) display)
        {
            resolution.x = Mathf.Clamp(resolution.x, 960, 1920);
            resolution.y = Mathf.Clamp(resolution.y, 540, 1080);
            if (resolution.x * 9 != resolution.y * 16) resolution = new(1920, 1080);

            display.targetFrameRate = Mathf.Clamp(display.targetFrameRate, 60, 120);

            Resolution = resolution;
            IsFullScreen = isFullScreen;
            Display = display;
        }

        private static int length => SGameState.Entity.GameStates?.Count ?? 0;
        private static int index = 0;
        internal static int LoopedIndex
        {
            get { return index; }
            set
            {
                int val = value;
                while (val >= length) val -= length;
                while (val < 0) val += length;
                index = val;
            }
        }
    }

    [Serializable]
    internal sealed class SerializedGameState : IProperty
    {
        [SerializeField, Header("�𑜓x(ex. 1920, 1080)")]
        private Vector2Int resolution;
        internal Vector2Int Resolution
        {
            get
            {
                int x = Mathf.Clamp(resolution.x, 960, 1920);
                int y = Mathf.Clamp(resolution.y, 540, 1080);
                if (x * 9 != y * 16) (x, y) = (1920, 1080);
                return new(x, y);
            }
        }

        [SerializeField, Header("�t���X�N���[�����H")]
        private bool isFullScreen;
        internal bool IsFullScreen => isFullScreen;

        [SerializeField, Header("���������̓I�����H")]
        private bool isVsyncOn;
        [SerializeField, Range(60, 120), Header("�����������I�t�Ȃ�A\n�^�[�Q�b�g�t���[�����[�g�͂������H")]
        private int targetFrameRate;
        internal (bool isVsyncOn, int targetFrameRate) Display => (isVsyncOn, targetFrameRate);
    }

    internal static class GameStateEx
    {
        private static GameState Convert(this SerializedGameState instance)
        {
            if (instance == null) return new(new(1920, 1080), true, (true, 60));
            return new(instance.Resolution, instance.IsFullScreen, instance.Display);
        }

        internal static void Apply(this GameState gameState)
        {
            if (gameState == null) return;

            Screen.SetResolution(gameState.Resolution.x, gameState.Resolution.y, gameState.IsFullScreen);
            if (gameState.Display.isVsyncOn)
            {
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = gameState.Display.targetFrameRate;
            }
        }

        internal static void Apply(this SerializedGameState serializedGameState)
            => serializedGameState.Convert().Apply();

        [RuntimeInitializeOnLoadMethod]
        internal static void InitializeGameState()
        {
            var gameStates = SGameState.Entity.GameStates;
            if (gameStates is null) return;
            foreach (var e in gameStates) if (e is null) return;

            GameState.LoopedIndex = 0;
            gameStates[GameState.LoopedIndex].Apply();
        }
    }
}