using Interface;
using System;
using UnityEngine;

namespace Data.Title.TitleImage
{
    [Serializable]
    internal sealed class TitleImageChangeProperty : IProperty
    {
        [SerializeField, Header("Ø‚è‘Ö‚í‚è‚ÌŠÔŠu(•b)")]
        private float changeIntervalSeconds = 0;
        internal float ChangeIntervalSeconds => changeIntervalSeconds;

        [SerializeField, Header("Ø‚è‘Ö‚í‚èŽžŠÔ(•b)")]
        private float changeDurationSeconds = 0;
        internal float ChangeDurationSeconds => changeDurationSeconds;
    }
}