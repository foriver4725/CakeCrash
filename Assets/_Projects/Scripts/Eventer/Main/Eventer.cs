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

        private IReference[] datas;
        private IHandler[] handlers;
        private IManager manager => Manager.Main.GameManager.Instance;

        private SO.SO_Main SMain => SO.SO_Main.Entity;

        public bool IsFirstUpdate { get; private set; } = true;

        private void OnEnable()
        {
            datas = new IReference[]
            {
                timeCountReference,
                cameraReference,
                beltConveyorReference,
                hammerReference
            };

            handlers = new IHandler[]
            {
                new Handler.Main.TimeCount.TimeCounter(timeCountReference, SMain.TimeLimit),
                new Handler.Main.Player.PlayerSquat.PlayerSquatter(new(cameraReference), SMain.CameraProperty),
                new Handler.Main.BeltConveyor.BeltConveyorMover(beltConveyorReference, SMain.BeltConvyorProperty),
                new Handler.Main.Hammer.HammerMover(hammerReference, SMain.HammerProperty)
            };
        }

        private void Update()
        {
            if (IsFirstUpdate)
            {
                IsFirstUpdate = false;

                manager.OnStart();
                foreach (IHandler handler in handlers) handler.Start();
            }

            manager.OnUpdate();
            foreach (IHandler handler in handlers) handler.Update();
        }

        private void OnDisable()
        {
            foreach (IHandler handler in handlers) handler.Dispose();
            System.Array.Clear(handlers, 0, handlers.Length);
            handlers = null;

            foreach (IReference data in datas) data.Dispose();
            System.Array.Clear(datas, 0, datas.Length);
            datas = null;

            timeCountReference = null;
            cameraReference = null;
            beltConveyorReference = null;
            hammerReference = null;
        }
    }
}