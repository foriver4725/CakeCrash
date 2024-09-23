using System;
using UnityEngine;
using UnityEngine.UI;

namespace Reference.Title.TitleImage
{
    [Serializable]
    internal sealed class ImageReference
    {
        [SerializeField, Header("タイトル画像(表示する順)")]
        private Image[] titleImages;
        internal Image[] TitleImages => titleImages;
    }
}