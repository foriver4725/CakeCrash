using Interface;
using Main.Data;
using Main.Manager;

namespace Main.Handler
{
    internal sealed class ScoreShower : IHandler
    {
        private ScoreReference reference;

        internal ScoreShower(ScoreReference reference)
        {
            this.reference = reference;
        }

        public void Start()
        {

        }

        public void Update()
        {
            reference.ScoreText = "壊した数：" + GameManager.Instance.CakeCount;
        }

        public void Dispose()
        {
            reference.Dispose();
            reference = null;
        }
    }
}
