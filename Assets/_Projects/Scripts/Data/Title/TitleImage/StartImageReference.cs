using System;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Title.TitleImage
{
    [Serializable]
    internal sealed class StartImageReference
    {
        [SerializeField, Header("�X�^�[�g�{�^���̃��C�g����")]
        private Image lightImage;

        internal float LightImageFillAmount
        {
            get => lightImage.fillAmount;
            set => lightImage.fillAmount = Mathf.Clamp(value, 0.0f, 1.0f);
        }
    }
}