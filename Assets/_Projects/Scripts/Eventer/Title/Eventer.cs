using Handler.Title.Input;
using Handler.Title.Sound;
using Handler.Title.TitleImage;
using Manager.Title;
using Data.Title.Sound;
using Data.Title.TitleImage;
using Data.Title.Tutorial;
using SO;
using UnityEngine;
using Handler.Title.Tutorial;

namespace Eventer.Title
{
    internal sealed class Eventer : MonoBehaviour
    {
        [SerializeField] private StartImageReference startImageReference;
        [SerializeField] private AudioSourceReference audioSourceReference;
        [SerializeField] private RefPro refPro;

        private SoundReference soundReference;

        private TutorialPlayer tutorialPlayer;
        private InputHandler inputHandler;
        private StartImageChanger startImageChanger;
        private BGMPlayer bgmPlayer;

        private bool isFirstUpdate = true;

        private void OnEnable()
        {
            AudioClipReference audioClipReference = new(SO_Sound.Entity.BGM.Title, SO_Sound.Entity.SE.General.Click);
            soundReference = new SoundReference(audioSourceReference, audioClipReference);

            tutorialPlayer = new(refPro);
            inputHandler = new(soundReference.PlayClickSE, SO_TitleDirection.Entity.WaitDurOnPlaced);
            startImageChanger = new(startImageReference, SO_TitleDirection.Entity.StartImageProperty);
            bgmPlayer = new(soundReference.PlayBGM);
        }

        private void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;

                GameManager.Instance.OnStart();
                tutorialPlayer.Start();
                inputHandler.Start();
                startImageChanger.Start();
                bgmPlayer.Start();
            }

            GameManager.Instance.OnUpdate();
            tutorialPlayer.Update();
            inputHandler.Update();
            startImageChanger.Update();
            bgmPlayer.Update();
        }

        private void OnDisable()
        {
            soundReference.Dispose();
            inputHandler.Dispose();
            startImageChanger.Dispose();
            tutorialPlayer.Dispose();
            bgmPlayer.Dispose();

            soundReference = null;
            tutorialPlayer = null;
            inputHandler = null;
            startImageChanger = null;
            bgmPlayer = null;

            startImageReference = null;
            audioSourceReference = null;
            refPro = null;
        }
    }
}