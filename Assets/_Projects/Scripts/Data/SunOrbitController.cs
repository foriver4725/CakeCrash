using Interface;
using System;
using UnityEngine;

namespace Handler.Main.TimeRelated
{
    /// <summary>
    /// ディレクショナルライトに関するクラス
    /// </summary>
    internal sealed class SunOrbitController : IDisposable, INullExistable, IEventable
    {
        internal Light DirectionalLight { get; private set; }

        public SunOrbitController(Light light) => DirectionalLight = light;

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
        /// null代入などの破棄処理
        /// </summary>
        public void Dispose() => DirectionalLight = null;

        /// <summary>
        /// nullメンバが存在するか、またはnullメンバを持つメンバがいるかを確認する処理
        /// </summary>
        public bool IsNullExist()
        {
            if (DirectionalLight == null) return true;
            return false;
        }
    }
}


