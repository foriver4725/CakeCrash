using General;
using IA;
using Interface;
using Manager.Title;
using System;

namespace Handler.Title.Input
{
    internal sealed class InputHandler : IEventable
    {
        /// <summary>
        /// ”ñ“¯Šú‚É‚È‚Á‚½ê‡A‚±‚Ìƒtƒ‰ƒO‚ªŠˆ‚«‚é
        /// </summary>
        private bool isAvailable
        {
            get { return GameManager.Instance.IsInputAvailable; }
            set { GameManager.Instance.IsInputAvailable = value; }
        }
        private bool isStart => InputGetter.Instance.Main_RedClick.Bool;
        private bool isConfig => InputGetter.Instance.Shortcut_LoadConfigSceneInTitleSceneClick.Bool;

        internal InputHandler() { }

        public void Start() { }

        public void Update()
        {
            if (isAvailable is false) return;

            if (isStart) DisableAllAndCallThis(OnStartClicked);
            else if (isConfig) DisableAllAndCallThis(OnConfigClicked);
        }

        private void DisableAllAndCallThis(Action action)
        {
            isAvailable = false;
            action();
        }

        private void OnStartClicked() => LoadScene.LoadSync(SceneName.Main);
        private void OnConfigClicked() => LoadScene.LoadSync(SceneName.Config);
    }
}