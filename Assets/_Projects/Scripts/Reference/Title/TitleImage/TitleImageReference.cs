using System;
using UnityEngine;
using UnityEngine.UI;

namespace Reference.Title.TitleImage
{
    [Serializable]
    internal sealed class TitleImageReference
    {
        [SerializeField, Header("�^�C�g���摜(�\�����鏇)")]
        private Image[] titleImages;
        internal Image[] TitleImages => titleImages;
    }
}