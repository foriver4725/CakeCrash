using System;
using UnityEngine;

namespace Manager.Main
{
    internal sealed class GameManager : MonoBehaviour
    {
        internal static GameManager Instance { get; set; } = null;

        // ゲームの状態とフラグ
        private State state;
        private Flag flag;
        internal Flag Flag => flag;

        /// <summary>
        /// メンバOnStart()より前に呼ぶ
        /// </summary>
        internal void OnInit()
        {
            // シングルトン化
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        /// <summary>
        /// Start()で一番最初に呼ぶ
        /// </summary>
        internal void OnStart()
        {
            state = new();
            flag = new(state);
        }

        /// <summary>
        /// Update()で一番最初に呼ぶ
        /// </summary>
        internal void OnUpdate()
        {

        }

        /// <summary>
        /// null代入などの破棄処理
        /// </summary>
        private void OnDisable()
        {
            flag.Dispose();

            Instance = null;
            flag = null;
            state = null;
        }
    }

    /// <summary>
    /// ゲームの状態
    /// </summary>
    internal sealed class State
    {
        internal State() { }

        /// <summary>
        /// しゃがんでいるか
        /// </summary>
        internal bool IsSquatting { get; set; } = false;

        /// <summary>
        /// 立ち上がった直後か
        /// </summary>
        internal bool IsOnStandUp { get; set; } = false;

        /// <summary>
        /// 攻撃を食らっている最中か
        /// </summary>
        internal bool IsBeingHitted { get; set; } = false;

        /// <summary>
        /// ゲームクリアになっているか
        /// </summary>
        internal bool IsBeingCleared { get; set; } = false;

        /// <summary>
        /// ゲームオーバーになっているか
        /// </summary>
        internal bool IsBeingOvered { get; set; } = false;
    }

    /// <summary>
    /// ゲームのフラグ
    /// </summary>
    internal sealed class Flag : IDisposable
    {
        private State state;

        internal Flag(State state) => this.state = state;

        public void Dispose()
        {
            state = null;
        }

        /// <summary>
        /// しゃがめるか
        /// </summary>
        internal bool IsSquattable =>
            !state.IsSquatting && !state.IsBeingHitted && !state.IsBeingCleared && !state.IsBeingOvered;

        /// <summary>
        /// 立ち上がれるか
        /// </summary>
        internal bool IsStandUppable =>
             !state.IsOnStandUp && !state.IsBeingHitted && !state.IsBeingCleared && !state.IsBeingOvered;

        /// <summary>
        /// ハンマーを振れるか
        /// </summary>
        internal bool IsSmashable =>
            !state.IsSquatting && !state.IsBeingHitted && !state.IsBeingCleared && !state.IsBeingOvered;

        /// <summary>
        /// 流れて行ってしまったケーキが、計上され得るか
        /// </summary>
        internal bool IsMissedCakeCountable =>
            !state.IsSquatting && !state.IsOnStandUp && !state.IsBeingHitted && !state.IsBeingCleared
            && !state.IsBeingOvered;

        /// <summary>
        /// 警備員が来る時間のカウントを、進められ得るか
        /// </summary>
        internal bool IsNewGuardManVisittable =>
             !state.IsBeingHitted && !state.IsBeingCleared && !state.IsBeingOvered;

        /// <summary>
        /// クリアになれるか
        /// </summary>
        internal bool IsBeingClearable =>
             !state.IsBeingCleared && !state.IsBeingOvered;

        /// <summary>
        /// ゲームオーバーになれるか
        /// </summary>
        internal bool IsBeingOverable =>
            !state.IsSquatting && !state.IsBeingCleared && !state.IsBeingOvered;
    }
}