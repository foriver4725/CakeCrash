using Interface;
using System;
using UnityEngine;

namespace Data.Title.TitleImage
{
    [Serializable]
    internal sealed class StartImageProperty : IProperty
    {
        [SerializeField, Range(1, 10), Header("‰½’iŠK‚É•ª‚¯‚é‚©")]
        private int lightStep;
        internal int LightStep => lightStep;

        [SerializeField, Range(0.1f, 10.0f), Header("‰½•b‚Å1’iŠKi‚Þ‚©")]
        private float lightDuration;
        internal float LightDuration => lightDuration;

        internal static readonly float LightStartFillAmount = 0.0f;
        internal static readonly float LightEndFillAmount = 1.0f;
    }
}