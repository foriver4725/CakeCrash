using Interface;
using Main.Data;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Main.Handler
{
    internal sealed class BeltConveyorMover : IHandler
    {
        private BeltConveyorReference reference;
        private BeltConveyorProperty property;
        private CancellationTokenSource cts;

        internal BeltConveyorMover(BeltConveyorReference reference, BeltConveyorProperty property)
        {
            this.reference = reference;
            this.property = property;
            this.cts = new();
        }

        public void Start() => reference.Move(property.Duration, cts.Token).Forget();
        public void Update() { }

        public void Dispose()
        {
            reference = null;
            property = null;

            cts.Cancel();
            cts.Dispose();
            cts = null;
        }
    }
}