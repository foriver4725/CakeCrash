using System.Threading;
using Cysharp.Threading.Tasks;
using General;
using IA;
using Interface;

namespace Clear.Handler
{
    internal sealed class TitleTraveller : IHandler
    {
        private CancellationTokenSource cts = new();

        internal TitleTraveller() { }

        public void Start() => CheckGoToTitle(cts.Token).Forget();

        private async UniTaskVoid CheckGoToTitle(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => InputGetter.Instance.Main_RedClick.Bool, cancellationToken: ct);
            LoadScene.LoadSync(SceneName.Title);
        }

        public void Update() { }

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
            cts = null;
        }
    }
}