using Interface;
using System;
using UnityEngine;

namespace Handler.Main.Time
{
    /// <summary>
    /// 時間に応じて太陽を回す
    /// </summary>
    internal sealed class SunOrbitController : IDisposable, INullExistable, IEventable
    {
        internal Light DirectionalLight { get; private set; }

        public SunOrbitController(Light light) => DirectionalLight = light;

        public void Start()
        {
            if (IsNullExist()) return;
        }

        public void Update()
        {
            if (IsNullExist()) return;
        }

        public void Dispose() => DirectionalLight = null;

        public bool IsNullExist()
        {
            if (DirectionalLight == null) return true;
            return false;
        }
    }
}


