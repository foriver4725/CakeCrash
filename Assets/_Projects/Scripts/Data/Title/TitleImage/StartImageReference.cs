using System;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Title.TitleImage
{
    [Serializable]
    internal sealed class StartImageReference
    {
        [SerializeField, Header("�X�^�[�g�{�^���̃��C�g����")]
        private Image[] lightImages;

        /// <summary>
        /// lightImage.fillAmount�̒l��a/b�ɂ���
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
        /// lightImage.fillAmount��0�ŏ���������
        /// </summary>
        internal void InitLightImagesFillAmount() => SetLightImagesFillAmount(0, 1);
    }
}