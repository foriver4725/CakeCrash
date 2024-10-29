using IA;
using Interface;
using Data.Main.Player.PlayerSquat;
using General;
using Manager.Main;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Handler.Main.Player.PlayerSquat
{
    internal sealed class PlayerSquatter : IHandler
    {
        private CameraMovement cameraMovement;
        private Property property;

        private bool preInput = false, input = false;
        private bool isDown = false, isUp = false;

        private State state => GameManager.Instance.State;
        private bool isSquattable => !state.IsBeingHitted && !state.IsGameEnded;

        private CancellationTokenSource cts = new();

        internal PlayerSquatter(CameraMovement cameraMovement, Property property)
        {
            this.cameraMovement = cameraMovement;
            this.property = property;
        }

        public void Start()
        {
            cameraMovement.CameraLocalY = property.Sy;
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
                await UniTask.WhenAll(
                    UniTask.WaitUntil(() => isSquattable, cancellationToken: ct),
                    UniTask.WaitUntil(() => isDown, cancellationToken: ct)
                );

                state.IsSquatting = true;
                NewlyMoveDown();

                await UniTask.WhenAll(
                    UniTask.WaitUntil(() => isSquattable, cancellationToken: ct),
                    UniTask.WaitUntil(() => isUp, cancellationToken: ct),
                    UniTask.WaitUntil(() => !state.IsGameEnded && !state.IsBeingHitted, cancellationToken: ct)
                );

                state.IsSquatting = false;
                NewlyMoveUp();
            }
        }

        private void NewlyMoveUp()
            => cameraMovement.NewlyMove(property.Sy, property.Srx, CalcDur(cameraMovement.CameraLocalY, false));
        private void NewlyMoveDown()
            => cameraMovement.NewlyMove(property.Ey, property.Erx, CalcDur(cameraMovement.CameraLocalY, true));

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
            cameraMovement.Dispose();

            cameraMovement = null;
            property = null;

            cts.Cancel();
            cts.Dispose();
            cts = null;
        }
    }
}