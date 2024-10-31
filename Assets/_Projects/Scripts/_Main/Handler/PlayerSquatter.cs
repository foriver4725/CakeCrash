using IA;
using Interface;
using Main.Data;
using General;
using Main.Manager;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using System.Data.Common;

namespace Main.Handler
{
    internal sealed class PlayerSquatter : IHandler
    {
        private CameraMovementReference cameraMovementReference;
        private PlayerProperty property;

        private bool preInput = false, input = false;
        private bool isDown = false, isUp = false;

        private State state => GameManager.Instance.State;
        private bool isSquattable => !state.IsBeingHitted && !state.IsGameEnded;

        private CancellationTokenSource cts = new();

        internal PlayerSquatter(CameraMovementReference cameraMovementReference, PlayerProperty property)
        {
            this.cameraMovementReference = cameraMovementReference;
            this.property = property;
        }

        public void Start()
        {
            cameraMovementReference.CameraLocalY = property.Sy;
            CheckSquat(cts.Token).Forget();
        }

        public void Update()
        {
            input = InputGetter.Instance.Main_SquatValue0.Bool;
            isDown = !preInput && input;
            isUp = preInput && !input;
            preInput = input;
        }

        private async UniTaskVoid CheckSquat(CancellationToken ct)
        {
            while (true)
            {
                await UniTask.WaitUntil(() => isSquattable && isDown, cancellationToken: ct);
                state.IsSquatting = true;
                NewlyMoveDown();

                await UniTask.WaitUntil(() => isUp, cancellationToken: ct);
                state.IsSquatting = false;
                NewlyMoveUp();
            }
        }

        private void NewlyMoveUp()
            => cameraMovementReference.NewlyMove(property.Sy, property.Srx, CalcDur(cameraMovementReference.CameraLocalY, false));
        private void NewlyMoveDown()
            => cameraMovementReference.NewlyMove(property.Ey, property.Erx, CalcDur(cameraMovementReference.CameraLocalY, true));

        /// <summary>
        /// 現在の、カメラのローカルy座標を基に、目的座標までの移動時間を計算する
        /// </summary>
        private float CalcDur(float y, bool isDown)
        {
            if (isDown) return y.Remap(property.Sy, property.Ey, property.Duration, 0);
            else return y.Remap(property.Ey, property.Sy, property.Duration, 0);
        }

        public void Dispose()
        {
            cameraMovementReference.Dispose();

            cameraMovementReference = null;
            property = null;

            cts.Cancel();
            cts.Dispose();
            cts = null;
        }
    }
}