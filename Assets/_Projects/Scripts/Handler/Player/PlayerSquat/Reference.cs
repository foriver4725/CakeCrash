using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

namespace Data.Main.Player.PlayerSquat
{
    internal sealed class Reference : IDisposable
    {
        private Transform cameraTf;
        private AudioSource squatSE;
        private AudioSource standupSE;

        internal float CameraLocalY => cameraTf.localPosition.y;

        private CancellationTokenSource ctsOnDispose;
        private CancellationTokenSource ctsAnyTime;

        internal Reference(Transform cameraTf, AudioSource squatSE, AudioSource standupSE)
        {
            this.cameraTf = cameraTf;
            this.squatSE = squatSE;
            this.standupSE = standupSE;

            ctsOnDispose = new();
            ctsAnyTime = new();
        }

        /// <summary>
        /// カメラを、新しく動き出させ始める
        /// </summary>
        internal void NewlyMove(float endY, float dur)
        {
            if (cameraTf == null) return;
            if (cameraTf.transform.localPosition.y == endY) return;

            ctsAnyTime.Cancel();
            ctsAnyTime.Dispose();
            ctsAnyTime = new();

            Move(endY, dur,
                CancellationTokenSource.CreateLinkedTokenSource(ctsOnDispose.Token, ctsAnyTime.Token).Token).Forget();
        }

        private async UniTask Move(float endY, float dur, CancellationToken ct)
        {
            if (cameraTf == null) return;

            await cameraTf.DOMoveY(endY, dur).ToUniTask(cancellationToken: ct);
        }

        /// <summary>
        /// カメラのローカルY座標を、指定した値に変更する
        /// </summary>
        internal void SetCameraLocalY(float y)
        {
            if (cameraTf == null) return;

            Vector3 pos = cameraTf.transform.localPosition;
            pos.y = y;
            cameraTf.transform.localPosition = pos;
        }

        /// <summary>
        /// しゃがんだSEを再生する
        /// </summary>
        internal void PlaySquatSE()
        {
            if (squatSE == null) return;
        }

        /// <summary>
        /// 立ち上がったSEを再生する
        /// </summary>
        internal void PlayStandupSE()
        {
            if (standupSE == null) return;
        }

        public void Dispose()
        {
            ctsOnDispose.Cancel();
            ctsOnDispose.Dispose();
            ctsOnDispose = null;

            ctsAnyTime.Dispose();
            ctsAnyTime = null;

            cameraTf = null;
            squatSE = null;
            standupSE = null;
        }
    }
}