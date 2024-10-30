using Interface;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Title.Data
{
    [Serializable]
    internal sealed class TitleImageReference : IReference
    {
        [SerializeField, Header("タイトル画像")]
        private Image[] titleImages;
        internal Image[] TitleImages => titleImages;

        public void Dispose()
        {
            Array.Clear(titleImages, 0, titleImages.Length);
            titleImages = null;
        }
    }
}