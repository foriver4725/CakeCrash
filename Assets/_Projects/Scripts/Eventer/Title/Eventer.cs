using Interface;
using UnityEngine;

namespace Eventer.Title
{
    internal sealed class Eventer : MonoBehaviour, IEventer
    {
        [SerializeField] private Data.Title.TitleImage.StartImageReference startImageReference;
        [SerializeField] private Data.Title.Sound.AudioSourceReference audioSourceReference;
        [SerializeField] private Data.Title.Tutorial.RefPro refPro;

        private Data.Title.Sound.SoundReference soundReference;

        private IReference[] datas;
        private IHandler[] handlers;
        private IManager manager => Manager.Title.GameManager.Instance;

        private SO.SO_Sound SSound => SO.SO_Sound.Entity;
        private SO.SO_TitleDirection SDir => SO.SO_TitleDirection.Entity;

        public bool IsFirstUpdate { get; private set; } = true;

        private void OnEnable()
        {
            soundReference = new(audioSourceReference, new(SSound.BGM.Title, SSound.SE.General.Click));

            datas = new IReference[]
            {
                startImageReference,
                audioSourceReference,
                refPro,
                soundReference
            };

            handlers = new IHandler[]
            {
                new Handler.Title.Tutorial.TutorialPlayer(refPro),
                new Handler.Title.Input.InputHandler(soundReference.PlayClickSE, SDir.WaitDurOnPlaced),
                new Handler.Title.TitleImage.StartImageChanger(startImageReference, SDir.StartImageProperty),
                new Handler.Title.Sound.BGMPlayer(soundReference.PlayBGM)
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
            refPro = null;
            soundReference = null;
        }
    }
}