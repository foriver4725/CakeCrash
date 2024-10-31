using Interface;

namespace Clear.Manager
{
    internal sealed class GameManager : ASingleton<GameManager>, IManager
    {
        public void OnStart() { }

        public void OnUpdate() { }

        private void OnDisable() { }
    }
}