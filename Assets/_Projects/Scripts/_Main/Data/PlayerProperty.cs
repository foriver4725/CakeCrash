using Interface;
using System;
using UnityEngine;

namespace Main.Data
{
    [Serializable]
    internal sealed class PlayerProperty : IProperty
    {
        [SerializeField, Range(0.1f, 1.0f), Header("何秒かけて変化しきるか")]
        private float duration;
        internal float Duration => duration;

        [SerializeField, Header("開始/終了のローカルy座標")]
        private Vector2 y;
        internal float Sy => Mathf.Clamp(y.x, -100, 100);
        internal float Ey => Mathf.Clamp(y.y, -100, Sy - 0.01f);

        [SerializeField, Header("開始/終了のローカル回転x")]
        private Vector2 rx;
        internal float Srx => Mathf.Clamp(rx.x, +0, 60);
        internal float Erx => Mathf.Clamp(rx.y, -60, -0);
    }
}