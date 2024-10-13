using Interface;
using UnityEngine;

namespace Eventer.Main
{
    internal sealed class Eventer : MonoBehaviour, IEventer
    {
        [SerializeField, Header("時間カウント 関連")]
        private Data.Main.TimeCount.TimeCountReference timeCountReference;
        [SerializeField, Header("カメラ移動 関連")]
        private Data.Main.Player.PlayerSquat.CameraReference cameraReference;
        [SerializeField, Header("ベルトコンベア 関連")]
        private Data.Main.BeltConveyor.Reference beltConveyorReference;
        [SerializeField, Header("ハンマー 関連")]
        private Data.Main.Hammer.Reference hammerReference;

        private IHandler[] handlers;

        private bool isFirstUpdate = true;

        private void OnEnable()
        {
            handlers = new IHandler[]
            {
                new Handler.Main.TimeCount.TimeCounter
                (timeCountReference, SO.SO_Main.Entity.TimeLimit),

                new Handler.Main.Player.PlayerSquat.PlayerSquatter
                (new(cameraReference), SO.SO_Main.Entity.CameraProperty),

                new Handler.Main.BeltConveyor.BeltConveyorMover
                (beltConveyorReference, SO.SO_Main.Entity.BeltConvyorProperty),

                new Handler.Main.Hammer.HammerMover
                (hammerReference, SO.SO_Main.Entity.HammerProperty)
            };
        }

        private void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;

                Manager.Main.GameManager.Instance.OnStart();
                foreach (IHandler handler in handlers) handler.Start();
            }

            Manager.Main.GameManager.Instance.OnUpdate();
            foreach (IHandler handler in handlers) handler.Update();
        }

        private void OnDisable()
        {
            foreach (IHandler handler in handlers) handler.Dispose();
            System.Array.Clear(handlers, 0, handlers.Length);
            handlers = null;

            (timeCountReference as IReference)?.Dispose();
            (cameraReference as IReference)?.Dispose();
            (beltConveyorReference as IReference)?.Dispose();
            (hammerReference as IReference)?.Dispose();

            timeCountReference = null;
            cameraReference = null;
            beltConveyorReference = null;
            hammerReference = null;
        }
    }
}