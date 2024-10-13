using Interface;
using System;
using UnityEngine;

namespace Data.Title.TitleImage
{
    [Serializable]
    internal sealed class TitleImageChangeProperty : IProperty
    {
        [SerializeField, Header("�؂�ւ��̊Ԋu(�b)")]
        private float changeIntervalSeconds = 0;
        internal float ChangeIntervalSeconds => changeIntervalSeconds;

        [SerializeField, Header("�؂�ւ�莞��(�b)")]
        private float changeDurationSeconds = 0;
        internal float ChangeDurationSeconds => changeDurationSeconds;
    }
}