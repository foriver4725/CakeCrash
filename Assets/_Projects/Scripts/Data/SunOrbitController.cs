using Interface;
using System;
using UnityEngine;

namespace Handler.Main.Time
{
    /// <summary>
    /// 時間に応じて太陽を回す
    /// </summary>
    internal sealed class SunOrbitController : IDisposable, INullExistable
    {
        internal Light Sun { get; private set; }

        public SunOrbitController(Light directionalLight) => Sun = directionalLight;

        public void Dispose() => Sun = null;

        public bool IsNullExist()
        {
            if (Sun == null) return true;
            return false;
        }
    }
}


