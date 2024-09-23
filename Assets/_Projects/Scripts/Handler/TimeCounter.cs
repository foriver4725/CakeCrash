using Interface;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Handler.Main.Time
{
    /// <summary>
    /// メインゲーム開始からの時間を計測する
    /// 時間に応じて、太陽を回し、クリアを判定する
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

        public void Start()
        {
            if (IsNullExist()) return;
        }

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

        public void Dispose()
        {
            clearControl.Dispose();
            clearControl = null;

            sunOrbitControl.Dispose();
            sunOrbitControl = null;
        }

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


