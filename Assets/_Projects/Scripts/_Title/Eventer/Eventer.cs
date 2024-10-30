using Interface;
using UnityEngine;
using Title.Data;
using Title.Manager;
using Title.Handler;

namespace Title.Eventer
{
    internal sealed class Eventer : MonoBehaviour, IEventer
    {
        [SerializeField] private StartImageReference startImageReference;
        [SerializeField] private AudioSourceReference audioSourceReference;

        private SoundReference soundReference;

        private IReference[] datas;
        private IHandler[] handlers;
        private IManager manager => GameManager.Instance;

        private SO.SSound SSound => SO.SSound.Entity;
        private SO.STitleDirection SDir => SO.STitleDirection.Entity;

        public bool IsFirstUpdate { get; private set; } = true;

        private void OnEnable()
        {
            soundReference = new(audioSourceReference, new(SSound.BGM.Title, SSound.SE.General.Click));

            datas = new IReference[]
            {
                startImageReference,
                audioSourceReference,
                soundReference
            };

            handlers = new IHandler[]
            {
                new InputHandler(soundReference.PlayClickSE, SDir.WaitDurOnPlaced),
                new StartImageChanger(startImageReference, SDir.StartImageProperty),
                new BGMPlayer(soundReference.PlayBGM)
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

            startImageReference = null;
            audioSourceReference = null;
            soundReference = null;
        }
    }
}