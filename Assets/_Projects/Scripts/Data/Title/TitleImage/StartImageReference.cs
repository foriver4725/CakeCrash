using System;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Title.TitleImage
{
    [Serializable]
    internal sealed class StartImageReference
    {
        [SerializeField, Header("スタートボタンのライト部分")]
        private Image[] lightImages;

        /// <summary>
        /// lightImage.fillAmountの値をa/bにする
        /// </summary>
        internal void SetLightImagesFillAmount(int a, int b)
        {
            if (b == 0) return;

            foreach (var e in lightImages) e.fillAmount = a switch
            {
                0 => StartImageProperty.LightStartFillAmount,
                _ when a == b => StartImageProperty.LightEndFillAmount,
                _ => (float)a / b
            };
        }

        /// <summary>
        /// lightImage.fillAmountを0で初期化する
        /// </summary>
        internal void InitLightImagesFillAmount() => SetLightImagesFillAmount(0, 1);
    }
}