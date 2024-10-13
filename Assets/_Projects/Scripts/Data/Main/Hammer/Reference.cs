using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

namespace Data.Main.Hammer
{
    [Serializable]
    internal sealed class Reference
    {
        [SerializeField, Header("HammerのTransform")]
        private Transform hammerTf;

        internal async UniTask Rotate
            (float sz, float ez, Vector3 se, Vector3 ee, float duration, Ease ease, CancellationToken ct)
        {
            CapsuleCollider hammerCol = hammerTf.transform.Find("Head").gameObject.GetComponent<CapsuleCollider>();
            if (hammerCol != null) hammerCol.enabled = true;

            SetLocalPosZAndLocalEuler(hammerTf, sz, se);
            await hammerTf.DOLocalRotate(ee, duration, RotateMode.FastBeyond360)
                .SetEase(ease)
                .ToUniTask(cancellationToken: ct);
            SetLocalPosZAndLocalEuler(hammerTf, ez, se);

            static void SetLocalPosZAndLocalEuler(Transform tf, float z, Vector3 euler)
            {
                if (tf == null) return;
                SetLocalPosZ(tf, z);
                SetLocalEuler(tf, euler);
            }

            static void SetLocalPosZ(Transform tf, float z)
            {
                if (tf == null) return;
                Vector3 pos = tf.localPosition;
                pos.z = z;
                tf.localPosition = pos;
            }

            static void SetLocalEuler(Transform tf, Vector3 euler)
            {
                if (tf == null) return;
                tf.localEulerAngles = euler;
            }
        }
    }
}