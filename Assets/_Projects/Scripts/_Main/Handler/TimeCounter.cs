using General;
using Interface;
using Main.Manager;
using Main.Data;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Main.Handler
{
    /// <summary>
    /// メインゲーム開始からの時間を計測する
    /// 時間に応じて、太陽を回し、時間のUIを変化させる
    /// 時間が来たら、クリアを判定する
    /// </summary>
    internal sealed class TimeCounter : IHandler
    {
        private float t = 0;
        private readonly float maxT;

        private TimeCountReference reference;

        internal TimeCounter(TimeCountReference reference, float maxT)
        {
            this.reference = reference;
            this.maxT = maxT;
        }

        public void Start() { }

        public void Update()
        {
            if (t >= maxT) StopOnce();
            else
            {
                t += Time.deltaTime;
                float p = t.Remap(0, maxT, 0, 1);
                reference.SetSunRotation(p);
                reference.SetClockFillAmount(p);
            }
        }

        public void Dispose()
        {
            reference = null;
        }

        internal void StopOnce()
        {
            reference.SetSunRotation(1);
            reference.SetClockFillAmount(1);

            if (GameManager.Instance.State.IsGameEnded) return;
            GameManager.Instance.State.IsGameEnded = true;
            GameManager.Instance.OnClear();
        }
    }
}