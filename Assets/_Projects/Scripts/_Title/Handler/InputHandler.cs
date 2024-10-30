using Cysharp.Threading.Tasks;
using General;
using IA;
using Interface;
using Title.Manager;
using System;
using System.Threading;

namespace Title.Handler
{
    internal sealed class InputHandler : IHandler
    {
        private Action playClickSE;
        private readonly float waitDurOnPlaced;
        private CancellationTokenSource cts;

        private bool isAvailable
        {
            get { return GameManager.Instance.IsInputAvailable; }
            set { GameManager.Instance.IsInputAvailable = value; }
        }
        private bool isStart => InputGetter.Instance.Main_RedClick.Bool;
        private bool isConfig => InputGetter.Instance.Shortcut_LoadConfigSceneInTitleSceneClick.Bool;

        internal InputHandler(Action playClickSE, float waitDurOnPlaced)
        {
            this.playClickSE = playClickSE;
            this.waitDurOnPlaced = waitDurOnPlaced;

            this.cts = new();
        }

        public void Dispose()
        {
            playClickSE = null;

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

        private void OnLoadSceneButtonPlaced(SceneName loadSceneName)
        {
            isAvailable = false;
            if (playClickSE is not null) playClickSE();
            waitDurOnPlaced.SecondsWaitAndDo(() => LoadScene.LoadSync(loadSceneName), cts.Token).Forget();
        }
    }
}