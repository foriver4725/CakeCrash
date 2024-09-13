using UnityEngine;

namespace Manager.Title
{
    internal sealed class GameManager : MonoBehaviour
    {
        internal static GameManager Instance { get; set; } = null;

        /// <summary>
        /// メンバOnStart()より前に呼ぶ
        /// </summary>
        internal void OnInit()
        {
            // シングルトン化
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

        /// <summary>
        /// null代入などの破棄処理
        /// </summary>
        private void OnDisable()
        {
            Instance = null;
        }
    }
}