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

        private IHandler[] handlers;

        private bool isFirstUpdate = true;

        private void OnEnable()
        {
            soundReference = new Data.Title.Sound.SoundReference
                (audioSourceReference, new(SO.SO_Sound.Entity.BGM.Title, SO.SO_Sound.Entity.SE.General.Click));

            handlers = new IHandler[]
            {
                new Handler.Title.Tutorial.TutorialPlayer
                (refPro),

                new Handler.Title.Input.InputHandler
                (soundReference.PlayClickSE, SO.SO_TitleDirection.Entity.WaitDurOnPlaced),

                new Handler.Title.TitleImage.StartImageChanger
                (startImageReference, SO.SO_TitleDirection.Entity.StartImageProperty),

                new Handler.Title.Sound.BGMPlayer
                (soundReference.PlayBGM)
            };
        }

        private void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;

                Manager.Title.GameManager.Instance.OnStart();
                foreach (IHandler handler in handlers) handler.Start();
            }

            Manager.Title.GameManager.Instance.OnUpdate();
            foreach (IHandler handler in handlers) handler.Update();
        }

        private void OnDisable()
        {
            foreach (IHandler handler in handlers) handler.Dispose();
            System.Array.Clear(handlers, 0, handlers.Length);
            handlers = null;

            (startImageReference as IReference)?.Dispose();
            (audioSourceReference as IReference)?.Dispose();
            (refPro as IReference)?.Dispose();
            (soundReference as IReference)?.Dispose();

            startImageReference = null;
            audioSourceReference = null;
            refPro = null;
            soundReference = null;
        }
    }
}