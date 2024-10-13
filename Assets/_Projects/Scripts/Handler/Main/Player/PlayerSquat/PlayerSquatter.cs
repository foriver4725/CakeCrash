using IA;
using Interface;
using System;
using Data.Main.Player.PlayerSquat;
using General;
using Manager.Main;

namespace Handler.Main.Player.PlayerSquat
{
    internal sealed class PlayerSquatter : IHandler
    {
        private CameraMovement cameraMovement;
        private Property property;

        private bool preInput = false, input = false;  // 入力の監視フラグ
        private bool preEnable = true, enable = true;  // 入力可否の監視フラグ

        internal PlayerSquatter(CameraMovement cameraMovement, Property property)
        {
            this.cameraMovement = cameraMovement;
            this.property = property;
        }

        public void Start()
        {
            cameraMovement.CameraLocalY = property.Sy;
        }

        public void Update()
        {
            // 負荷軽減のため、動きが変化した瞬間のみを捉え、新しく動かさせ始める

            try { enable = (GameManager.Instance.Flag as Flag).IsSquattable; }
            catch (NullReferenceException) { return; }

            // 無効になった瞬間
            if (preEnable && !enable)
            {
                NewlyMoveUp();
            }
            // 有効である時
            else if (enable)
            {
                input = InputGetter.Instance.Main_SquatValue0.Bool;

                // 押された瞬間
                if (!preInput && input)
                {
                    NewlyMoveDown();
                }
                // 離された瞬間
                else if (preInput && !input)
                {
                    NewlyMoveUp();
                }
            }

            preInput = input;
            preEnable = enable;
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
        }
    }
}