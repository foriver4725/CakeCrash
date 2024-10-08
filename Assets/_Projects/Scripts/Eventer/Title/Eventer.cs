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
        [SerializeField] private TitleImageReference imageReference;
        [SerializeField] private StartImageReference startImageReference;
        [SerializeField] private AudioSourceReference audioSourceReference;
        [SerializeField] private RefPro refPro;

        private SoundReference soundReference;

        private InputHandler inputHandler;
        private TitleImageChanger titleImageChanger;
        private StartImageChanger startImageChanger;
        private TutorialPlayer tutorialPlayer;
        private BGMPlayer bgmPlayer;

        private bool isFirstUpdate = true;

        private void OnEnable()
        {
            AudioClipReference audioClipReference = new(SO_Sound.Entity.BGM.Title, SO_Sound.Entity.SE.General.Click);
            soundReference = new SoundReference(audioSourceReference, audioClipReference);

            inputHandler = new(soundReference.PlayClickSE, SO_TitleDirection.Entity.WaitDurOnPlaced);
            titleImageChanger = new(imageReference, SO_TitleDirection.Entity.TitleImageChangeProperty);
            startImageChanger = new(startImageReference, SO_TitleDirection.Entity.StartImageProperty);
            tutorialPlayer = new(refPro);
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
                titleImageChanger.Start();
                startImageChanger.Start();
                bgmPlayer.Start();
            }

            GameManager.Instance.OnUpdate();
            tutorialPlayer.Update();
            inputHandler.Update();
            titleImageChanger.Update();
            startImageChanger.Update();
            bgmPlayer.Update();
        }

        private void OnDisable()
        {
            soundReference.Dispose();
            inputHandler.Dispose();
            titleImageChanger.Dispose();
            startImageChanger.Dispose();
            tutorialPlayer.Dispose();
            bgmPlayer.Dispose();

            soundReference = null;
            inputHandler = null;
            titleImageChanger = null;
            startImageChanger = null;
            tutorialPlayer = null;
            bgmPlayer = null;

            imageReference = null;
            startImageReference = null;
            audioSourceReference = null;
            refPro = null;
        }
    }
}