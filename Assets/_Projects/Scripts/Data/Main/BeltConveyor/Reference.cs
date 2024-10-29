using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interface;
using System;
using System.Threading;
using UnityEngine;

namespace Data.Main.BeltConveyor
{
    [Serializable]
    internal sealed class Reference : IReference
    {
        [SerializeField, Header("中心")]
        private Transform center;

        [SerializeField, Header("中心より左")]
        private Transform left;

        internal async UniTask Move(float d, CancellationToken ct)
        {
            float sz = left.localPosition.z;
            float cz = center.localPosition.z;
            float ez = 2 * cz - sz;

            SetLocalZ(center, cz);
            SetLocalZ(left, sz);

            while (true)
            {
                await UniTask.WhenAll(
                    center.DOLocalMoveZ(ez, d).SetEase(Ease.Linear).ToUniTask(cancellationToken: ct),
                    left.DOLocalMoveZ(cz, d).SetEase(Ease.Linear).ToUniTask(cancellationToken: ct));

                SetLocalZ(center, sz);

                await UniTask.WhenAll(
                    center.DOLocalMoveZ(cz, d).SetEase(Ease.Linear).ToUniTask(cancellationToken: ct),
                    left.DOLocalMoveZ(ez, d).SetEase(Ease.Linear).ToUniTask(cancellationToken: ct));

                SetLocalZ(left, sz);
            }
        }

        private void SetLocalZ(Transform tf, float z)
        {
            if (tf == null) return;
            Vector3 pos = tf.localPosition;
            pos.z = z;
            tf.localPosition = pos;
        }

        public void Dispose()
        {
            center = null;
            left = null;
        }
    }
}