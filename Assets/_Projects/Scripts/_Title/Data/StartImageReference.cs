using Interface;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Title.Data
{
    [Serializable]
    internal sealed class StartImageReference : IReference
    {
        [SerializeField]
        private Image[] lightImages;

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

        internal void InitLightImagesFillAmount() => SetLightImagesFillAmount(0, 1);

        public void Dispose()
        {
            Array.Clear(lightImages, 0, lightImages.Length);
            lightImages = null;
        }
    }
}