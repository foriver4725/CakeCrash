using Interface;
using System;
using Data.Main.BeltConveyor;


namespace Handler.Main.BeltConveyor
{
    internal sealed class BeltConveyorMover : IDisposable, IEventable
    {
        private Reference reference;
        private Property property;

        internal BeltConveyorMover(Reference reference, Property property)
        {
            this.reference = reference;
            this.property = property;
        }

        public void Start()
        {
            
        }

        public void Update()
        {
            reference.MoveDelta(property.Speed);
        }

        public void Dispose()
        {
            reference = null;
            property = null;
        }
    }
}