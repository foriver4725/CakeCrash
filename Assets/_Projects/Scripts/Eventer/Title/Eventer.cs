using Handler.Title.Input;
using Handler.Title.TitleImage;
using Manager.Title;
using Reference.Title.TitleImage;
using SO;
using UnityEngine;

namespace Eventer.Title
{
    internal sealed class Eventer : MonoBehaviour
    {
        [SerializeField] TitleImageReference titleImageReference;

        private InputHandler inputHandler;
        private TitleImageChanger titleImageChanger;

        private bool isFirstUpdate = true;

        private void OnEnable()
        {
            inputHandler = new();
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
            titleImageChanger.Dispose();

            inputHandler = null;
            titleImageChanger = null;
        }
    }
}