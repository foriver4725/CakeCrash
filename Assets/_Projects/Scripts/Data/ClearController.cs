using Interface;
using System;
using static UnityEngine.Debug;

namespace Handler.Main.TimeRelated 
{
    /// <summary>
    /// ゲームクリアに関するクラス
    /// </summary>
    internal sealed class ClearController : IDisposable, INullExistable, IEventable
    {

        /// <summary>
        /// Start()で呼ぶ
        /// </summary>
        public void Start()
        {
            if (IsNullExist()) return;
        }

        /// <summary>
        /// Update()で呼ぶ
        /// </summary>
        public void Update()
        {
            if (IsNullExist()) return;
        }

        /// <summary>
        /// ゲームクリア時の処理
        /// </summary>
        internal void GameClear()
        {
            Log("ゲームクリア");
        }

        /// <summary>
        /// null代入などの破棄処理
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// nullメンバが存在するか、またはnullメンバを持つメンバがいるかを確認する処理
        /// </summary>
        public bool IsNullExist()
        {
            return false;
        }
    }
}


