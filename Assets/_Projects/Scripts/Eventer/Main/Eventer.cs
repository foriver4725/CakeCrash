using Handler.Main.Player.PlayerSquat;
using Handler.Main.TimeCount;
using Handler.Main.BeltConveyor;
using Data.Main.TimeCount;
using BeltConveyorReference = Data.Main.BeltConveyor.Reference;
using SO;
using UnityEngine;
using Data.Main.Player.PlayerSquat;
using Manager.Main;

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

        private TimeCounter timeCounter;
        private PlayerSquatter playerSquatter;
        private BeltConveyorMover beltConveyorMover;

        private bool isFirstUpdate = true;

        private void OnEnable()
        {
            timeCounter = new(timeCountReference, SO_Main.Entity.TimeLimit);
            playerSquatter = new(new(cameraReference), SO_Main.Entity.CameraProperty);
            beltConveyorMover = new(beltConveyorReference, SO_Main.Entity.BeltConvyorProperty);
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
            }

            GameManager.Instance.OnUpdate();
            timeCounter.Update();
            playerSquatter.Update();
            beltConveyorMover.Update();
        }

        private void OnDisable()
        {
            timeCounter.Dispose();
            playerSquatter.Dispose();
            beltConveyorMover.Dispose();

            timeCounter = null;
            playerSquatter = null;
            beltConveyorMover = null;
        }
    }
}