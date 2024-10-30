using Interface;

namespace Title.Manager
{
    internal sealed class GameManager : ASingleton<GameManager>, IManager
    {
        public void OnStart() { }
        public void OnUpdate() { }
    }
}