using Interface;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Title.TitleImage
{
    [Serializable]
    internal sealed class TitleImageReference : IReference
    {
        [SerializeField, Header("タイトル画像(表示する順)")]
        private Image[] titleImages;
        internal Image[] TitleImages => titleImages;

        public void Dispose()
        {
            Array.Clear(titleImages, 0, titleImages.Length);
            titleImages = null;
        }
    }
}