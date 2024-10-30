using Cysharp.Threading.Tasks;
using General;
using IA;
using Interface;
using System;
using System.Threading;

namespace Title.Handler
{
    internal sealed class InputHandler : IHandler
    {
        private Action playClickSE;
        private readonly float waitDurOnPlaced;
        private CancellationTokenSource cts;

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

        public void Start() => SceneChange(cts.Token).Forget();
        public void Update() { }

        private async UniTask SceneChange(CancellationToken ct)
        {
            int i = await UniTask.WhenAny(
                UniTask.WaitUntil(() => isStart, cancellationToken: ct),
                UniTask.WaitUntil(() => isConfig, cancellationToken: ct)
            );

            playClickSE();
            await waitDurOnPlaced.SecWait(ct);

            if (i == 0) LoadScene.LoadSync(SceneName.Main);
            else if (i == 1) LoadScene.LoadSync(SceneName.Config);
            else return;
        }
    }
}