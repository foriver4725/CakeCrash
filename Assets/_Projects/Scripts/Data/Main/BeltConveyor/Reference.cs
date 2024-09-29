using System;
using UnityEngine;

namespace Data.Main.BeltConveyor
{
    [Serializable]
    internal sealed class Reference
    {
        [SerializeField, Header("ここまで来たら戻す")]
        private Transform resetPoint;

        [SerializeField, Header("ここに戻す")]
        private Transform destinationPoint;

        [SerializeField, Header("ベルトコンベアーへの参照")]
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