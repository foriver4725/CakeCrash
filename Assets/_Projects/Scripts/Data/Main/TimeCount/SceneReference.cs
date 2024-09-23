using System;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Main.TimeCount
{
    internal sealed class SceneReference : IDisposable
    {
        private Transform sun;
        private Image clockImage;

        internal SceneReference(Transform sun, Image clockImage)
        {
            this.sun = sun;
            this.clockImage = clockImage;
        }

        public void Dispose()
        {
            sun = null;
            clockImage = null;
        }

        /// <summary>
        /// p:今何パーセントの時間が進んでいるか [0, 1]
        /// </summary>
        internal void SetSunRotation(float p)
        {
            if (sun == null) return;
            if (IsPercentageNormalized(p) is false) return;

            // 計算は変更する可能性あり
            Quaternion q = Quaternion.Euler(0, 0, 180 * p);
            sun.rotation = q * sun.rotation;
        }

        /// <summary>
        /// p:今何パーセントの時間が進んでいるか [0, 1]
        /// </summary>
        internal void SetClockFillAmount(float p)
        {
            if (clockImage == null) return;
            if (IsPercentageNormalized(p) is false) return;

            // 計算は変更する可能性あり
            clockImage.fillAmount = p;
        }

        // pが[0, 1]内にあるかどうか
        private bool IsPercentageNormalized(float p) => p is >= 0 and <= 1;
    }
}