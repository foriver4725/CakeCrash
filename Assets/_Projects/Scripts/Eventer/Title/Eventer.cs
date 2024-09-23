using Handler.Title.Input;
using Handler.Title.Sound;
using Handler.Title.TitleImage;
using Manager.Title;
using Reference.Title.Sound;
using Reference.Title.TitleImage;
using SO;
using UnityEngine;

namespace Eventer.Title
{
    internal sealed class Eventer : MonoBehaviour
    {
        [SerializeField] ImageReference imageReference;
        [SerializeField] AudioSourceReference audioSourceReference;

        private SoundReference soundReference;

        private InputHandler inputHandler;
        private TitleImageChanger titleImageChanger;
        private BGMPlayer bgmPlayer;

        private bool isFirstUpdate = true;

        private void OnEnable()
        {
            AudioClipReference audioClipReference = new(SO_Sound.Entity.BGM.Title, SO_Sound.Entity.SE.General.Click);
            soundReference = new SoundReference(audioSourceReference, audioClipReference);

            inputHandler = new(soundReference.PlayClickSE, SO_TitleDirection.Entity.WaitDurOnPlaced);
            titleImageChanger = new(imageReference, SO_TitleDirection.Entity.TitleImageChangeProperty);
            bgmPlayer = new(soundReference.PlayBGM);
        }

        private void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;

                GameManager.Instance.OnStart();
                inputHandler.Start();
                titleImageChanger.Start();
                bgmPlayer.Start();
            }

            GameManager.Instance.OnUpdate();
            inputHandler.Update();
            titleImageChanger.Update();
            bgmPlayer.Update();
        }

        private void OnDisable()
        {
            soundReference.Dispose();
            inputHandler.Dispose();
            titleImageChanger.Dispose();
            bgmPlayer.Dispose();

            soundReference = null;
            inputHandler = null;
            titleImageChanger = null;
            bgmPlayer = null;

            imageReference = null;
            audioSourceReference = null;
        }
    }
}