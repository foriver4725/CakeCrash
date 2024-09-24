using Handler.Main.Player.PlayerSquat;
using Handler.Main.TimeCount;
using Data.Main.TimeCount;
using SO;
using UnityEngine;
using Data.Main.Player.PlayerSquat;
using Manager.Main;

namespace Eventer.Main
{
    internal sealed class Eventer : MonoBehaviour
    {
        [SerializeField, Header("時間カウント 関連")]
        private TimeCountReference timeCountReference;

        [SerializeField, Header("カメラ移動 関連")]
        private CameraReference cameraReference;

        private TimeCounter timeCounter;
        private PlayerSquatter playerSquatter;

        private bool isFirstUpdate = true;

        private void OnEnable()
        {
            timeCounter = new(timeCountReference, SO_Main.Entity.TimeLimit);
            playerSquatter = new(new(cameraReference), SO_Main.Entity.CameraProperty);
        }

        private void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;

                GameManager.Instance.OnStart();
                timeCounter.Start();
                playerSquatter.Start();
            }

            GameManager.Instance.OnUpdate();
            timeCounter.Update();
            playerSquatter.Update();
        }

        private void OnDisable()
        {
            timeCounter.Dispose();
            playerSquatter.Dispose();

            timeCounter = null;
            playerSquatter = null;
        }
    }
}