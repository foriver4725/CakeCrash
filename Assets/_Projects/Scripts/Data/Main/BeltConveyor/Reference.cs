using System;
using UnityEngine;

namespace Data.Main.BeltConveyor
{
    [Serializable]
    internal sealed class Reference
    {
        [SerializeField, Header("�����܂ŗ�����߂�")]
        private Transform resetPoint;

        [SerializeField, Header("�����ɖ߂�")]
        private Transform destinationPoint;

        [SerializeField, Header("�x���g�R���x�A�[�ւ̎Q��")]
        private Transform[] beltConveyors;

        internal void MoveDelta(float speed)
        {
            if (beltConveyors == null) return;

            foreach (var e in beltConveyors)
            {
                if (e == null) continue;

                e.localPosition += e.transform.right * (-speed * Time.deltaTime);
                if (e.localPosition.z >= resetPoint.localPosition.z) e.localPosition = destinationPoint.localPosition;
            }
        }
    }
}