using System.Threading;
using Cysharp.Threading.Tasks;
using General;
using Interface;
using Main.Data;
using UnityEngine;

namespace Main.Manager
{
    internal sealed class GameManager : ASingleton<GameManager>, IManager
    {
        internal AnnounceImageReference AnnounceImageReference { get; set; } = null;

        internal State State { get; set; }
        internal PressedColor RecentPressedColor { get; set; }

        // ケーキを壊した数
        private int cakeCount { get; set; } = 0;
        public int CakeCount
        {
            get => cakeCount;
            set => cakeCount = Mathf.Clamp(value, 0, 0xffff);
        }

        // クリアを判定するスクリプトで呼ぶ
        public void OnClear() => GoToClear(destroyCancellationToken).Forget();

        private async UniTask GoToClear(CancellationToken ct)
        {
            await AnnounceImageReference.GameEnded(destroyCancellationToken);
            LoadScene.LoadSync(SceneName.Clear);
        }

        public void OnStart()
        {
            State = new();
            RecentPressedColor = new();

            Countdown(destroyCancellationToken).Forget();
        }

        private async UniTask Countdown(CancellationToken ct)
        {
            GameState.IsPaused = true;
            await AnnounceImageReference.CountDown(ct);
            GameState.IsPaused = false;
        }

        public void OnUpdate() { }

        private void OnDisable()
        {
            AnnounceImageReference.Dispose();
            AnnounceImageReference = null;

            State = null;
            RecentPressedColor = null;
        }
    }

    internal sealed class PressedColor
    {
        internal string ColorType { get; set; } = string.Empty;
    }

    internal sealed class State
    {
        internal State() { }

        internal bool IsSquatting { get; set; } = false;
        internal bool IsBeingHitted { get; set; } = false;
        internal bool IsGameEnded { get; set; } = false;
    }
}