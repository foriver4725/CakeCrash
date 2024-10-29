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

        [SerializeField, Header("ケーキのプレハブ(赤)")]
        private ColoredCake redCakes;

        [SerializeField, Header("ケーキのプレハブ(青)")]
        private ColoredCake blueCakes;

        [SerializeField, Header("ケーキのプレハブ(緑)")]
        private ColoredCake greenCakes;

        private void RearrangeCenter() => Rearrange(true);
        private void RearrangeLeft() => Rearrange(false);

        private void Rearrange(bool isCenter)
        {
            try
            {
                Transform tf = isCenter ? center : left;

                int len = tf.childCount;
                Transform[] cakeSets = new Transform[len];
                for (int i = 0; i < len; i++) cakeSets[i] = tf.GetChild(i);

                // lenと同じ長さの前提
                float[] cakeLocalZs =
                { 22.5f, 19.5f, 16.5f, 13.5f, 10.5f, 7.5f, 4.5f, 1.5f, -1.5f, -4.5f, -7.5f, -10.5f, -13.5f, -16.5f, -19.5f, -22.5f, -25.5f };
                for (int i = 0; i < len; i++)
                {
                    Transform cakeSet = cakeSets[i];
                    Transform large = cakeSet.GetChild(2);
                    Transform medium = cakeSet.GetChild(1);
                    Transform small = cakeSet.GetChild(0);

                    large.gameObject.SetActive(true);
                    medium.gameObject.SetActive(true);
                    small.gameObject.SetActive(true);

                    large.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    medium.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    small.GetComponent<Rigidbody>().velocity = Vector3.zero;

                    cakeSet.localPosition = new(1.25f, -1.75f, cakeLocalZs[i]);
                    large.localPosition = new(0, -0.75f, 0);
                    medium.localPosition = new(0, 0, 0);
                    small.localPosition = new(0, 0.75f, 0);
                }
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                Debug.LogWarning(e);
#endif
                return;
            }
        }

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
                RearrangeCenter();

                await UniTask.WhenAll(
                    center.DOLocalMoveZ(cz, d).SetEase(Ease.Linear).ToUniTask(cancellationToken: ct),
                    left.DOLocalMoveZ(ez, d).SetEase(Ease.Linear).ToUniTask(cancellationToken: ct));

                SetLocalZ(left, sz);
                RearrangeLeft();
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

        [Serializable]
        private sealed class ColoredCake
        {
            [SerializeField, Header("小")]
            private GameObject small;
            public GameObject Small => small;

            [SerializeField, Header("中")]
            private GameObject medium;
            public GameObject Medium => medium;

            [SerializeField, Header("大")]
            private GameObject large;
            public GameObject Large => large;
        }
    }
}