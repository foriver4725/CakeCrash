using Interface;
using System;
using UnityEngine;

namespace Handler.Main.Time
{
    /// <summary>
    /// 時間をカウントし、ゲームクリアを判定
    /// </summary>
    internal sealed class ClearController : IDisposable, INullExistable, IEventable
    {
        public void Start()
        {
            if (IsNullExist()) return;
        }

        public void Update()
        {
            if (IsNullExist()) return;
        }

        internal void GameClear()
        {
            Debug.Log("ゲームクリア");
        }

        public void Dispose()
        {

        }

        public bool IsNullExist()
        {
            return false;
        }
    }
}


