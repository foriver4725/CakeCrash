using Interface;
using UnityEngine;

namespace Manager.Title
{
    internal sealed class GameManager : MonoBehaviour, IManager
    {
        public static GameManager Instance { get; set; } = null;

        /// <summary>
        /// 全ての入力を受け付けるかどうか
        /// </summary>
        internal bool IsInputAvailable = true;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }
        public void OnStart() { }
        public void OnUpdate() { }
        private void OnDisable()
        {
            Instance = null;
        }
    }
}