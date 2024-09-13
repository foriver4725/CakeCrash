using UnityEngine;

namespace Eventer.Main
{
    internal sealed class Eventer : MonoBehaviour
    {
        #region Reference
        //[SerializeField, Header("プレイヤー参照")] private PlayerRef playerRef;
        #endregion

        #region Flags
        //Update関数内で最初だけ実行する処理のためのフラグ
        private bool isFirstUpdate = true;
        #endregion

        #region OnEnable

        /// <summary>
        /// インスタンス化　参照の受け渡しも
        /// </summary>
        private void OnEnable()
        {

        }
        #endregion

        #region Update

        /// <summary>
        /// イベントを呼ぶ　最初の一回はStart()も
        /// </summary>
        private void Update()
        {
            if (isFirstUpdate)
            {
                //Update関数内で最初だけ実行

                isFirstUpdate = false;
            }
            //毎フレーム処理

        }
        #endregion

        #region OnDisable
        /// <summary>
        /// Dispose()を呼ぶ null代入など
        /// </summary>
        private void OnDisable()
        {

        }
        #endregion
    }
}
