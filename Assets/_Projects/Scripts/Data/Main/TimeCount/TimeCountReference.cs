using General;
using SO;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Main.TimeCount
{
    [Serializable]
    internal sealed class TimeCountReference
    {
        [SerializeField]
        private Transform sun;

        [SerializeField]
        private Image clockImage;

        private float sunOfst => SO_Main.Entity.SunRotateOffset;
        private static readonly Quaternion sunInitRotation = Quaternion.Euler(180, -90, 0);

        /// <summary>
        /// p:今何パーセントの時間が進んでいるか [0, 1]
        /// </summary>
        internal void SetSunRotation(float p)
        {
            if (sun == null) return;
            if (IsPercentageNormalized(p) is false) return;

            Quaternion q = Quaternion.AngleAxis(p.Remap(0, 1, -sunOfst, 180 + sunOfst), Vector3.forward);
            sun.rotation = q * sunInitRotation;
        }

        /// <summary>
        /// p:今何パーセントの時間が進んでいるか [0, 1]
        /// </summary>
        internal void SetClockFillAmount(float p)
        {
            if (clockImage == null) return;
            if (IsPercentageNormalized(p) is false) return;

            clockImage.fillAmount = p;
        }

        // pが[0, 1]内にあるかどうか
        private bool IsPercentageNormalized(float p) => p is >= 0 and <= 1;
    }
}