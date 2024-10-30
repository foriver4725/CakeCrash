using Interface;
using System;
using UnityEngine;

namespace Title.Data
{
    [Serializable]
    internal sealed class StartImageProperty : IProperty
    {
        [SerializeField, Range(1, 10), Header("���i�K�ɕ����邩")]
        private int lightStep;
        internal int LightStep => lightStep;

        [SerializeField, Range(0.1f, 10.0f), Header("���b��1�i�K�i�ނ�")]
        private float lightDuration;
        internal float LightDuration => lightDuration;

        internal static readonly float LightStartFillAmount = 0.0f;
        internal static readonly float LightEndFillAmount = 1.0f;
    }
}