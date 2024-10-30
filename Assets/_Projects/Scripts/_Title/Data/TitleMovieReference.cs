using Interface;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Title.Data
{
    [Serializable]
    internal sealed class TitleMovieReference : IReference
    {
        [SerializeField, Range(1f, 60f), Header("表示するまでの時間")]
        private float displayStartTime;
        [SerializeField, Header("表示する動画,Canvasの子オブジェクト")]
        private Image video;

        internal float DisplayStartTime => displayStartTime;
        internal Image Video => video;

        public void Dispose()
        {
            video = null;
        }
    }
}