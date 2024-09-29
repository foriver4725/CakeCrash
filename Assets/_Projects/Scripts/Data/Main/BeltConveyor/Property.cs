using System;
using UnityEngine;

namespace Data.Main.BeltConveyor
{
    [Serializable]
    internal sealed class Property
    {
        [SerializeField, Range(1.0f, 50.0f), Header("何秒でsx=>cx,cx=>exを動くか")]
        private float duration;
        internal float Duration => duration;
    }
}