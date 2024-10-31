using Interface;
using UnityEngine;
using Main.Data;
using Main.Manager;
using Main.Handler;

namespace Main.Eventer
{
    internal sealed class Eventer : MonoBehaviour, IEventer
    {
        [SerializeField, Header("時間カウント 関連")]
        private TimeCountReference timeCountReference;
        [SerializeField, Header("カメラ移動 関連")]
        private CameraReference cameraReference;
        [SerializeField, Header("ベルトコンベア 関連")]
        private BeltConveyorReference beltConveyorReference;
        [SerializeField, Header("ガードマン関連")]
        private GuardmanReference guardmanReference;
        [SerializeField, Header("ハンマー 関連")]
        private HammerReference hammerReference;
        [SerializeField, Header("スコア表示 関連")]
        private ScoreReference scoreReference;
        [SerializeField, Header("アナウンス画像 関連")]
        private AnnounceImageReference announceImageReference;

        private IReference[] datas;
        private IHandler[] handlers;
        private IManager manager => GameManager.Instance;

        private SO.SMain SMain => SO.SMain.Entity;

        public bool IsFirstUpdate { get; private set; } = true;

        private void OnEnable()
        {
            datas = new IReference[]
            {
                timeCountReference,
                cameraReference,
                beltConveyorReference,
                guardmanReference,
                hammerReference,
                scoreReference
            };

            handlers = new IHandler[]
            {
                new TimeCounter(timeCountReference, SMain.TimeLimit),
                new PlayerSquatter(new(cameraReference), SMain.CameraProperty),
                new BeltConveyorMover(beltConveyorReference, SMain.BeltConvyorProperty),
                new GuardmanWalker(guardmanReference, SMain.GuardmanProperty),
                new HammerMover(hammerReference, SMain.HammerProperty),
                new ScoreShower(scoreReference)
            };

            (manager as GameManager).AnnounceImageReference = announceImageReference;
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