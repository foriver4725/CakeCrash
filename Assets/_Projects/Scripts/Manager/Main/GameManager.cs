using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using SO;
using UnityEngine;

namespace Manager.Main
{
    internal sealed class GameManager : ASingleton<GameManager>, IManager
    {
        internal State State { get; set; }
        internal PressedColor RecentPressedColor { get; set; }

        // ケーキを壊した数
        public int CakeCount { get; set; } = 0;

        // クリアを判定するスクリプトで呼ぶ
        public void OnClear()
        {

        }

        public void OnStart()
        {
            State = new();
            RecentPressedColor = new();
        }

        public void OnUpdate() { }

        private void OnDisable()
        {
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