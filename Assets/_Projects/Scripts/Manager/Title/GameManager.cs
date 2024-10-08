using UnityEngine;

namespace Manager.Title
{
    internal sealed class GameManager : MonoBehaviour
    {
        internal static GameManager Instance { get; set; } = null;

        /// <summary>
        /// 全ての入力を受け付けるかどうか
        /// </summary>
        internal bool IsInputAvailable = true;

        internal bool IsVideoJustDeactivate = false;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        /// <summary>
        /// Start()で一番最初に呼ぶ
        /// </summary>
        internal void OnStart()
        {

        }

        /// <summary>
        /// Update()で一番最初に呼ぶ
        /// </summary>
        internal void OnUpdate()
        {
            
        }

        private void OnDisable()
        {
            Instance = null;
        }
    }
}