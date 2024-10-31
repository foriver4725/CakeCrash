using System;
using UnityEngine;
using Interface;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;
using General;
using Main.Manager;

namespace Main.Data
{
    [Serializable]
    internal sealed class GuardmanReference : IReference
    {
        [SerializeField]
        private Transform door;

        [SerializeField]
        private Transform stand;

        [SerializeField]
        private Transform walk;

        private void SetLocalPositionZ(Transform tf, float value)
        {
            if (tf == null) return;
            Vector3 pos = tf.localPosition;
            pos.z = value;
            tf.localPosition = pos;
        }

        internal async UniTaskVoid StartMove(GuardmanProperty p, CancellationToken ct)
        {
            stand.gameObject.SetActive(false);
            walk.gameObject.SetActive(true);

            door.localEulerAngles = new(0, p.DoorSEulerY, 0);
            SetLocalPositionZ(stand, p.StandZ);
            SetLocalPositionZ(walk, p.WalkSz);

            while (true)
            {
                // 待つ
                await p.Dur.SecWait(ct);

                // 歩く
                await walk.DOLocalMoveZ(p.WalkEz, p.WalkDur).SetEase(p.WalkEase).ToUniTask(cancellationToken: ct);
                SetLocalPositionZ(walk, p.WalkSz);

                // 立つ
                await p.DurBeforeStandBeyondDoor.SecWait(ct);
                stand.gameObject.SetActive(true);

                // 少し開ける
                await p.DurBeforeDoorLittleOpen.SecWait(ct);
                await door.DOLocalRotate(new(0, p.DoorLEulerY, 0), p.DoorLittleOpenDur, RotateMode.Fast).SetEase(p.DoorLittleOpenEase).ToUniTask(cancellationToken: ct);

                await p.DurBeforeDoorOpen.SecWait(ct);
                if (UnityEngine.Random.value > p.FaintProb / 100)
                {
                    // 開ける
                    await door.DOLocalRotate(new(0, p.DoorEEulerY, 0), p.DoorOpenDur, RotateMode.Fast).SetEase(p.DoorOpenEase).ToUniTask(cancellationToken: ct);
                    GameManager.Instance.State.IsLooking = true;
                    await UniTask.NextFrame(cancellationToken: ct);
                    if (GameManager.Instance.State.IsBeingHitted)
                    {
                        GameManager.Instance.State.IsLooking = false;
                        await UniTask.WaitUntil(() => !GameManager.Instance.State.IsBeingHitted, cancellationToken: ct);
                    }
                    else
                    {
                        await p.DurAfterDoorOpened.SecWait(ct);
                        GameManager.Instance.State.IsLooking = false;
                    }
                }

                // 閉める
                await door.DOLocalRotate(new(0, p.DoorSEulerY, 0), p.DoorCloseDur, RotateMode.Fast).SetEase(p.DoorCloseEase).ToUniTask(cancellationToken: ct);

                // 帰る
                await p.DurAfterDoorClosed.SecWait(ct);
                stand.gameObject.SetActive(false);
            }
        }

        public void Dispose()
        {
            stand = null;
            walk = null;
        }
    }
}
