using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace General
{
    internal static class Ex
    {
        /// <summary>
        /// X[a, b] をY[c, d]に線形マッピングする
        /// 0除算が起こる(a==b)ようなら、0を返す
        /// </summary>
        internal static float Remap(this float x, float a, float b, float c, float d)
        {
            if (a == b) return 0;

            return (x - a) * (d - c) / (b - a) + c;
        }

        internal static async UniTask SecondsWaitAndDo(this float seconds, Action action, CancellationToken ct)
        {
            if (action == null) return;

            await UniTask.Delay(TimeSpan.FromSeconds(seconds), cancellationToken: ct);
            action();
        }
    }
}