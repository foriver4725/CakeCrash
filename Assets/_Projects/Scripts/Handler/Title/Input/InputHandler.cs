using Cysharp.Threading.Tasks;
using General;
using Handler.Title.Sound;
using IA;
using Interface;
using Manager.Title;
using System;
using System.Threading;

namespace Handler.Title.Input
{
    internal sealed class InputHandler : IDisposable, IEventable
    {
        private SoundPlayer soundPlayer;
        private float waitDurOnPlaced;
        private CancellationTokenSource cts;

        private bool isAvailable
        {
            get { return GameManager.Instance.IsInputAvailable; }
            set { GameManager.Instance.IsInputAvailable = value; }
        }
        private bool isStart => InputGetter.Instance.Main_RedClick.Bool;
        private bool isConfig => InputGetter.Instance.Shortcut_LoadConfigSceneInTitleSceneClick.Bool;

        internal InputHandler(SoundPlayer soundPlayer, float waitDurOnPlaced, CancellationTokenSource cts)
        {
            this.soundPlayer = soundPlayer;
            this.waitDurOnPlaced = waitDurOnPlaced;
            this.cts = cts;
        }

        public void Dispose()
        {
            soundPlayer.Dispose();

            soundPlayer = null;

            cts.Cancel();
            cts.Dispose();
            cts = null;
        }

        public void Start() { }

        public void Update()
        {
            if (isAvailable is false) return;

            if (isStart) OnLoadSceneButtonPlaced(SceneName.Main);
            else if (isConfig) OnLoadSceneButtonPlaced(SceneName.Config);
        }

        /// <summary>
        /// ìØéûÇ…ï°êîåƒÇÒÇ≈ÇÕÇ¢ÇØÇ»Ç¢
        /// </summary>
        private void OnLoadSceneButtonPlaced(SceneName loadSceneName)
        {
            isAvailable = false;
            soundPlayer.PlayClickSE();
            waitDurOnPlaced.SecondsWaitAndDo(() => LoadScene.LoadSync(loadSceneName), cts.Token).Forget();
        }
    }
}