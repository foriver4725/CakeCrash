using Handler.Title.Input;
using Manager.Title;
using UnityEngine;

namespace Eventer.Title
{
    internal sealed class Eventer : MonoBehaviour
    {
        private InputHandler inputHandler;

        private bool isFirstUpdate = true;

        private void OnEnable()
        {
            inputHandler = new();
        }

        private void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;

                GameManager.Instance.OnStart();
                inputHandler.Start();
            }

            GameManager.Instance.OnUpdate();
            inputHandler.Update();
        }

        private void OnDisable()
        {
            inputHandler = null;
        }
    }
}