using Interface;
using System;
using UnityEngine;

namespace Main.Data
{
    [Serializable]
    internal sealed class HammerProperty : IProperty
    {
        [SerializeField, Header("ハンマーの前者ローカルz座標")]
        private float sz;
        internal float Sz => sz;

        [SerializeField, Header("ハンマーの後者ローカルz座標")]
        private float ez;
        internal float Ez => ez;

        [SerializeField, Header("ハンマーの前者オイラー角")]
        private Vector3 se;
        internal Vector3 Se => se;

        [SerializeField, Header("ハンマーの後者オイラー角")]
        private Vector3 ee;
        internal Vector3 Ee => ee;

        [SerializeField, Range(0.1f, 3.0f), Header("ハンマーの回転時間(秒)")]
        private float duration;
        internal float Duration => duration;

        [SerializeField, Range(0.1f, 3.0f), Header("ハンマーの回転可能間隔(秒)")]
        private float interval;
        internal float Interval => interval;

        [SerializeField, Header("回転の補間方法")]
        private DG.Tweening.Ease ease;
        internal DG.Tweening.Ease Ease => ease;
    }
}