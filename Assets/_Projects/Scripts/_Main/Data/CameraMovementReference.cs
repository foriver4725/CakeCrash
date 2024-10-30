using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interface;
using System.Threading;
using UnityEngine;

namespace Main.Data
{
    internal sealed class CameraMovementReference : IReference
    {
        private Transform cameraTf;
        private AudioSource squatSE;
        private AudioSource standupSE;

        internal float CameraLocalY
        {
            get
            {
                if (cameraTf == null) return 0;
                return cameraTf.localPosition.y;
            }
            set
            {
                if (cameraTf == null) return;
                Vector3 pos = cameraTf.transform.localPosition;
                pos.y = value;
                cameraTf.transform.localPosition = pos;
            }
        }

        internal float CameraLocalRotateX
        {
            get
            {
                if (cameraTf == null) return 0;
                return cameraTf.localRotation.x;
            }
            set
            {
                if (cameraTf == null) return;
                cameraTf.localRotation = Quaternion.Euler(value, 0, 0);
            }
        }

        private CancellationTokenSource ctsOnDispose;
        private CancellationTokenSource ctsAnyTime;

        internal CameraMovementReference(CameraReference cameraReference)
        {
            this.cameraTf = cameraReference.CameraTf;
            this.squatSE = cameraReference.SquatSE;
            this.standupSE = cameraReference.StandupSE;

            ctsOnDispose = new();
            ctsAnyTime = new();
        }

        /// <summary>
        /// カメラを、新しく動き出させ始める
        /// </summary>
        internal void NewlyMove(float endY, float endRx, float dur)
        {
            if (cameraTf == null) return;

            ctsAnyTime.Cancel();
            ctsAnyTime.Dispose();
            ctsAnyTime = new();

            Move(endY, endRx, dur,
                CancellationTokenSource.CreateLinkedTokenSource(ctsOnDispose.Token, ctsAnyTime.Token).Token).Forget();
        }

        private async UniTask Move(float endY, float endRx, float dur, CancellationToken ct)
        {
            if (cameraTf == null) return;

            await UniTask.WhenAll(
                cameraTf.DOMoveY(endY, dur).ToUniTask(cancellationToken: ct),
                cameraTf.DORotate(new(endRx, 0, 0), dur).ToUniTask(cancellationToken: ct)
                );
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