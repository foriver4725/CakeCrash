using Interface;
using System;
using UnityEngine;

namespace Data.Main.BeltConveyor
{
    [Serializable]
    internal sealed class Property : IProperty
    {
        [SerializeField, Range(10.0f, 1000.0f), Header("何秒でsx=>cx,cx=>exを動くか")]
        private float duration;
        internal float Duration => duration;
    }
}