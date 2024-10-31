using System;
using DG.Tweening;
using Interface;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Data
{
    [Serializable]
    internal sealed class GuardmanProperty : IProperty
    {
        [SerializeField, Header("下記の動きの間隔秒数")]
        private float dur;
        internal float Dur => dur;

        [SerializeField, Range(0, 100), Header("フェイントになる確率")]
        private float faintProb;
        internal float FaintProb => faintProb;

        [SerializeField, Header("ドアローカルオイラー角y開始")]
        private float doorSEulerY;
        internal float DoorSEulerY => doorSEulerY;

        [SerializeField, Header("ドアローカルオイラー角y少し開ける")]
        private float doorLEulerY;
        internal float DoorLEulerY => doorLEulerY;

        [SerializeField, Header("ドアローカルオイラー角y終了")]
        private float doorEEulerY;
        internal float DoorEEulerY => doorEEulerY;

        [SerializeField, Header("ドアを少し開ける秒数")]
        private float doorLittleOpenDur;
        internal float DoorLittleOpenDur => doorLittleOpenDur;

        [SerializeField, Header("ドアを少し開けるイージング")]
        private Ease doorLittleOpenEase;
        internal Ease DoorLittleOpenEase => doorLittleOpenEase;

        [SerializeField, Header("ドアを開ける時開")]
        private float doorOpenDur;
        internal float DoorOpenDur => doorOpenDur;

        [SerializeField, Header("ドアを開けるイージング")]
        private Ease doorOpenEase;
        internal Ease DoorOpenEase => doorOpenEase;

        [SerializeField, Header("ドアを閉める時間")]
        private float doorCloseDur;
        internal float DoorCloseDur => doorCloseDur;

        [SerializeField, Header("ドアを閉めるイージング")]
        private Ease doorCloseEase;
        internal Ease DoorCloseEase => doorCloseEase;

        [SerializeField, Header("立ちガードマンローカル座標z")]
        private float standZ;
        internal float StandZ => standZ;

        [SerializeField, Header("歩きガードマンローカル座標z開始")]
        private float walkSz;
        internal float WalkSz => walkSz;

        [SerializeField, Header("歩きガードマンローカル座標z終了")]
        private float walkEz;
        internal float WalkEz => walkEz;

        [SerializeField, Header("歩きガードマン移動時間")]
        private float walkDur;
        internal float WalkDur => walkDur;

        [SerializeField, Header("歩きガードマンイージング")]
        private Ease walkEase;
        internal Ease WalkEase => walkEase;

        [SerializeField, Header("ドアの前に立つ前に待つ秒数")]
        private float durBeforeStandBeyondDoor;
        internal float DurBeforeStandBeyondDoor => durBeforeStandBeyondDoor;

        [SerializeField, Header("ドアを少し開ける前に待つ秒数")]
        private float durBeforeDoorLittleOpen;
        internal float DurBeforeDoorLittleOpen => durBeforeDoorLittleOpen;

        [SerializeField, Header("ドアを開ける前に待つ秒数")]
        private float durBeforeDoorOpen;
        internal float DurBeforeDoorOpen => durBeforeDoorOpen;

        [SerializeField, Header("ドアを開けたあと待つ秒数")]
        private float durAfterDoorOpened;
        internal float DurAfterDoorOpened => durAfterDoorOpened;

        [SerializeField, Header("ドアを閉めたあと待つ秒数")]
        private float durAfterDoorClosed;
        internal float DurAfterDoorClosed => durAfterDoorClosed;
    }
}
