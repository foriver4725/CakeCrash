using System;
using UnityEngine;

namespace Data.Title.TitleImage
{
    [Serializable]
    internal sealed class StartImageProperty
    {
        [SerializeField, Range(1, 10), Header("何段階に分けるか")]
        private int lightStep;
        internal int LightStep => lightStep;

        [SerializeField, Range(0.1f, 10.0f), Header("何秒で1段階進むか")]
        private float lightDuration;
        internal float LightDuration => lightDuration;

        internal static readonly float LightStartFillAmount = 0.0f;
        internal static readonly float LightEndFillAmount = 1.0f;
    }
}