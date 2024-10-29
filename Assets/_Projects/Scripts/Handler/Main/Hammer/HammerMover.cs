using Cysharp.Threading.Tasks;
using Data.Main.Hammer;
using IA;
using Interface;
using System;
using System.Threading;
using Manager.Main;

namespace Handler.Main.Hammer
{
    internal sealed class HammerMover : IHandler
    {

        private Reference reference;
        private Property property;

        private CancellationTokenSource ctsOnDispose = null;

        private State state => GameManager.Instance.State;
        private bool isSmashable => !state.IsSquatting && !state.IsBeingHitted && !state.IsGameEnded;

        private bool isRed => isSmashable && InputGetter.Instance.Main_RedClick.Bool;
        private bool isBlue => isSmashable && InputGetter.Instance.Main_BlueClick.Bool;
        private bool isGreen => isSmashable && InputGetter.Instance.Main_GreenClick.Bool;

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

                string color = i switch
                {
                    0 => "Cake/Red",
                    1 => "Cake/Blue",
                    2 => "Cake/Green",
                    _ => string.Empty
                };

                if (string.IsNullOrEmpty(color) == false)
                    GameManager.Instance.RecentPressedColor.ColorType = color;


                await reference.Rotate
                    (property.Sz, property.Ez, property.Se, property.Ee, property.Duration, property.Ease, ct);

                GameManager.Instance.RecentPressedColor.ColorType = string.Empty;

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