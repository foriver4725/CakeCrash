using Interface;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Handler.Main.Time
{
    /// <summary>
    /// 時間を計る
    /// </summary>
    internal sealed class TimeCounter : IDisposable, INullExistable, IEventable
    {
        private float elapsedTime = 0;
        private readonly float maxTimeLimit;
        private bool isClear = false;

        private Image clockImage;

        private ClearController clearControl;
        private SunOrbitController sunOrbitControl;


        internal TimeCounter(float _maxtimeLimit, Image _clockImage, Light _directionalLight)
        {
            maxTimeLimit = _maxtimeLimit;
            clearControl = new();
            clockImage = _clockImage;
            sunOrbitControl = new(_directionalLight);

        }

        /// <summary>
        /// Start()で呼ぶ
        /// </summary>
        public void Start()
        {
            if (IsNullExist()) return;
        }

        /// <summary>
        /// Update()で呼ぶ
        /// </summary>
        public void Update()
        {
            if (IsNullExist()) return;

            if (isClear) return;

            if (elapsedTime >= maxTimeLimit)
            {
                isClear = true;
                clearControl.GameClear();
                return;
            }

            elapsedTime += UnityEngine.Time.deltaTime;
            sunOrbitControl.Sun.transform.rotation = Quaternion.Euler(new Vector3(elapsedTime * 180f, 0.0f, 0.0f));

        }

        /// <summary>
        /// null代入などの破棄処理
        /// </summary>
        public void Dispose()
        {
            clearControl.Dispose();
            clearControl = null;

            sunOrbitControl.Dispose();
            sunOrbitControl = null;
        }

        /// <summary>
        /// nullメンバが存在するか、またはnullメンバを持つメンバがいるかを確認する処理
        /// </summary>
        public bool IsNullExist()
        {
            if (clearControl == null) return true;
            if (clearControl.IsNullExist()) return true;

            if (sunOrbitControl == null) return true;
            if (sunOrbitControl.IsNullExist()) return true;

            return false;
        }
    }
}


