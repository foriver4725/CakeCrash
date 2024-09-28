using Cysharp.Threading.Tasks;
using Data.Title.TitleImage;
using Interface;
using System;
using System.Threading;

namespace Handler.Title.TitleImage
{
    internal sealed class StartImageChanger : IDisposable, IEventable
    {
        private StartImageReference reference;
        private StartImageProperty property;

        private int _lightIndex = 0;
        private int loopedLightIndex
        {
            get => _lightIndex;
            set
            {
                int val = value;
                while (val >= property.LightStep) val -= property.LightStep;
                while (val < 0) val += property.LightStep;
                _lightIndex = val;
            }
        }

        private CancellationTokenSource ctsOnDispose;

        internal StartImageChanger(StartImageReference reference, StartImageProperty property)
        {
            this.reference = reference;
            this.property = property;

            this.loopedLightIndex = 0;
            ctsOnDispose = new();
        }

        public void Start()
        {
            reference.LightImageFillAmount = StartImageProperty.LightStartFillAmount;
            ChangeLight(ctsOnDispose.Token).Forget();
        }

        private async UniTask ChangeLight(CancellationToken ct)
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(property.LightDuration), cancellationToken: ct);

                reference.LightImageFillAmount = loopedLightIndex switch
                {
                    0 => StartImageProperty.LightStartFillAmount,
                    _ when loopedLightIndex == (property.LightStep - 1) => StartImageProperty.LightEndFillAmount,
                    _ => (float)loopedLightIndex / (property.LightStep - 1)
                };

                loopedLightIndex++;
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