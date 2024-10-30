using Interface;

namespace Title.Manager
{
    internal sealed class GameManager : ASingleton<GameManager>, IManager
    {
        /// <summary>
        /// 全ての入力を受け付けるかどうか
        /// </summary>
        internal bool IsInputAvailable = true;

        public void OnStart() { }
        public void OnUpdate() { }
    }
}