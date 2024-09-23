using Handler.Title.Input;
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
        [SerializeField] TitleImageReference titleImageReference;
        [SerializeField] TitleAudioSourceReference titleAudioSourceReference;

        private InputHandler inputHandler;
        private TitleImageChanger titleImageChanger;

        private bool isFirstUpdate = true;

        private void OnEnable()
        {
            inputHandler
                = new(new(titleAudioSourceReference, new(SO_Sound.Entity.BGM.Title, SO_Sound.Entity.SE.Click)),
                    SO_TitleDirection.Entity.WaitDurOnPlaced, new());
            titleImageChanger = new(titleImageReference, SO_TitleDirection.Entity.TitleImageChangeProperty, new());
        }

        private void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;

                GameManager.Instance.OnStart();
                inputHandler.Start();
                titleImageChanger.Start();
            }

            GameManager.Instance.OnUpdate();
            inputHandler.Update();
            titleImageChanger.Update();
        }

        private void OnDisable()
        {
            inputHandler.Dispose();
            titleImageChanger.Dispose();

            inputHandler = null;
            titleImageChanger = null;

            titleImageReference = null;
            titleAudioSourceReference = null;
        }
    }
}