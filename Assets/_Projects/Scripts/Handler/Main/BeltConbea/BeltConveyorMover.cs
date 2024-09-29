using Interface;
using System;
using Data.Main.BeltConveyor;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Handler.Main.BeltConveyor
{
    internal sealed class BeltConveyorMover : IDisposable, IEventable
    {
        private Reference reference;
        private Property property;
        private CancellationTokenSource cts;

        internal BeltConveyorMover(Reference reference, Property property)
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