using Interface;
using System;
using static UnityEngine.Debug;

namespace Handler.Main.TimeRelated
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
            Log("ゲームクリア");
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


