using System;
using UnityEngine;

namespace Data.Main.BeltConveyor
{
    [Serializable]
    internal sealed class Property
    {
        [SerializeField, Range(0.1f, 5.0f), Header("横スクロールの速度")]
        private float speed;
        internal float Speed => speed;
    }
}