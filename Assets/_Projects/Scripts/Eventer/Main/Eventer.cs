using UnityEngine;

namespace Eventer.Main
{
    internal sealed class Eventer : MonoBehaviour
    {
        private bool isFirstUpdate = true;

        private void OnEnable()
        {

        }

        private void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;
            }
        }

        private void OnDisable()
        {

        }
    }
}