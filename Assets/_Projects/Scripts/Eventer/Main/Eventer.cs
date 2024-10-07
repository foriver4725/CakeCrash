using Handler.Main.Player.PlayerSquat;
using Handler.Main.TimeCount;
using Handler.Main.BeltConveyor;
using Data.Main.TimeCount;
using BeltConveyorReference = Data.Main.BeltConveyor.Reference;
using HammerReference = Data.Main.Hammer.Reference;
using SO;
using UnityEngine;
using Data.Main.Player.PlayerSquat;
using Manager.Main;
using Handler.Main.Hammer;

namespace Eventer.Main
{
    internal sealed class Eventer : MonoBehaviour
    {
        [SerializeField, Header("時間カウント 関連")]
        private TimeCountReference timeCountReference;

        [SerializeField, Header("カメラ移動 関連")]
        private CameraReference cameraReference;

        [SerializeField, Header("ベルトコンベア 関連")]
        private BeltConveyorReference beltConveyorReference;

        [SerializeField, Header("ハンマー 関連")]
        private HammerReference hammerReference;

        private TimeCounter timeCounter;
        private PlayerSquatter playerSquatter;
        private BeltConveyorMover beltConveyorMover;
        private HammerMover hammerMover;

        private bool isFirstUpdate = true;

        private void OnEnable()
        {
            timeCounter = new(timeCountReference, SO_Main.Entity.TimeLimit);
            playerSquatter = new(new(cameraReference), SO_Main.Entity.CameraProperty);
            beltConveyorMover = new(beltConveyorReference, SO_Main.Entity.BeltConvyorProperty);
            hammerMover = new(hammerReference, SO_Main.Entity.HammerProperty);
        }

        private void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;

                GameManager.Instance.OnStart();
                timeCounter.Start();
                playerSquatter.Start();
                beltConveyorMover.Start();
                hammerMover.Start();
            }

            GameManager.Instance.OnUpdate();
            timeCounter.Update();
            playerSquatter.Update();
            beltConveyorMover.Update();
            hammerMover.Update();
        }

        private void OnDisable()
        {
            timeCounter.Dispose();
            playerSquatter.Dispose();
            beltConveyorMover.Dispose();
            hammerMover.Dispose();

            timeCounter = null;
            playerSquatter = null;
            beltConveyorMover = null;
            hammerMover = null;
        }
    }
}