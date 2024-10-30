using Cysharp.Threading.Tasks;
using Title.Data;
using General;
using Interface;
using System;
using System.Threading;

namespace Title.Handler
{
    internal sealed class StartImageChanger : IHandler
    {
        private StartImageReference reference;
        private StartImageProperty property;
        private LoopedInt loopedLightIndex;
        private CancellationTokenSource ctsOnDispose;

        internal StartImageChanger(StartImageReference reference, StartImageProperty property)
        {
            this.reference = reference;
            this.property = property;
            this.loopedLightIndex = new(property.LightStep);
            this.ctsOnDispose = new();
        }

        public void Start()
        {
            reference.InitLightImagesFillAmount();
            ChangeLight(ctsOnDispose.Token).Forget();
        }

        private async UniTask ChangeLight(CancellationToken ct)
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(property.LightDuration), cancellationToken: ct);
                reference.SetLightImagesFillAmount(loopedLightIndex.Value, property.LightStep - 1);
                loopedLightIndex.Value++;
            }
        }

        public void Update() { }

        public void Dispose()
        {
            reference = null;
            property = null;

            ctsOnDispose.Cancel();
            ctsOnDispose.Dispose();
            ctsOnDispose = null;
        }
    }
}