using System;
using IA;
using Interface;
using UnityEngine;
using Data.Title.Tutorial;

namespace Handler.Title.Tutorial
{
    internal sealed class TutorialPlayer : IEventable, IDisposable
    {
        private RefPro refPro;

        internal TutorialPlayer(RefPro refPro) => this.refPro = refPro;

        private float timer = 0;
        private bool isGreen => InputGetter.Instance.Main_GreenClick.Bool;
        private bool isBlue => InputGetter.Instance.Main_BlueClick.Bool;
        private bool isAnyColor => isBlue | isGreen;
        private bool isVideoActive
        {
            set
            {
                if (refPro.Video == null) return;
                refPro.Video.gameObject.SetActive(value);
            }
        }

        public void Start() { }

        public void Update()
        {
            // 何か押されたら時間をリセット
            if (isAnyColor)
            {
                timer = 0;
            }
            else if (timer < refPro.DisplayStartTime)
            {
                timer += Time.deltaTime;
            }

            // 何も押していない時間がしきい値を超えたら表示する 
            // SetActiveしてるだけ
            isVideoActive = (timer >= refPro.DisplayStartTime);
        }

        public void Dispose() => refPro = null;
    }
}