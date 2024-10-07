using Cysharp.Threading.Tasks;
using Data.Main.Hammer;
using IA;
using Interface;
using System;
using System.Threading;

namespace Handler.Main.Hammer
{
    internal sealed class HammerMover : IEventable, IDisposable
    {
        private Reference reference;
        private Property property;

        private CancellationTokenSource ctsOnDispose = null;

        private bool isRed = InputGetter.Instance.Main_RedClick.Bool;
        private bool isBlue = InputGetter.Instance.Main_BlueClick.Bool;
        private bool isGreen = InputGetter.Instance.Main_GreenClick.Bool;

        internal HammerMover(Reference reference, Property property)
        {
            this.reference = reference;
            this.property = property;

            this.ctsOnDispose = new();
        }

        public void Start() => WaitAndRotate(ctsOnDispose.Token).Forget();
        public void Update() { }

        private async UniTask WaitAndRotate(CancellationToken ct)
        {
            while (true)
            {
                int i = await UniTask.WhenAny
                   (
                   UniTask.WaitUntil(() => isRed, cancellationToken: ct),
                   UniTask.WaitUntil(() => isBlue, cancellationToken: ct),
                   UniTask.WaitUntil(() => isGreen, cancellationToken: ct)
                   );

                await reference.Rotate
                    (property.Sz, property.Ez, property.Se, property.Ee, property.Duration, property.Ease, ct);

                await UniTask.Delay(TimeSpan.FromSeconds(property.Interval), cancellationToken: ct);
            }
        }

        public void Dispose()
        {
            ctsOnDispose.Cancel();
            ctsOnDispose.Dispose();
            ctsOnDispose = null;

            reference = null;
            property = null;
        }
    }
}