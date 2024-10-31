using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using Main.Data;

namespace Main.Handler
{
    public class GuardmanWalker : IHandler
    {
        private GuardmanReference reference;
        private GuardmanProperty property;

        private CancellationTokenSource cts;

        internal GuardmanWalker(GuardmanReference reference, GuardmanProperty property)
        {
            this.reference = reference;
            this.property = property;

            cts = new();
        }

        public void Start() => reference.StartMove(property, cts.Token).Forget();
        public void Update() { }

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
            cts = null;

            reference.Dispose();

            reference = null;
            property = null;
        }
    }
}
