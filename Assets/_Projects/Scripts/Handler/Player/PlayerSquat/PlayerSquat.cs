using IA;
using Interface;
using System;
using Data.Main.Player.PlayerSquat;
using General.Main.Player.PlayerSquat;
using Manager.Main;
using UnityEngine;

namespace Handler.Main.Player.PlayerSquat
{
    internal sealed class PlayerSquat : IDisposable, INullExistable, IEventable
    {
        private Reference reference;
        private Property property;

        private bool preInput = false, input = false;  // 入力の監視フラグ
        private bool preEnable = true, enable = true;  // 入力可否の監視フラグ

        internal PlayerSquat(Reference reference, Property property)
        {
            this.reference = reference;
            this.property = property;
        }

        public void Start()
        {
            if (IsNullExist()) return;

            reference.SetCameraLocalY(property.Sy);
        }

        public void Update()
        {
            if (IsNullExist()) return;

            // 負荷軽減のため、動きが変化した瞬間のみを捉え、新しく動かさせ始める

            try { enable = GameManager.Instance.Flag.IsSquattable; }
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

        private void NewlyMoveUp() => reference.NewlyMove(property.Sy, CalcDur(reference.CameraLocalY, false));
        private void NewlyMoveDown() => reference.NewlyMove(property.Ey, CalcDur(reference.CameraLocalY, true));

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
            reference.Dispose();

            reference = null;
            property = null;
        }

        public bool IsNullExist()
        {
            if (reference == null) return true;
            if (property == null) return true;

            return false;
        }
    }
}