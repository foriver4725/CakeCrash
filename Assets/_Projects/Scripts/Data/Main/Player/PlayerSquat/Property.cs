namespace Data.Main.Player.PlayerSquat
{
    internal sealed class Property
    {
        /// <summary>
        /// 何秒かけて変化しきるか
        /// </summary>
        internal float Duration { get; private set; }

        /// <summary>
        /// 開始/終了のローカルy座標
        /// </summary>
        internal float Sy { get; private set; }
        internal float Ey { get; private set; }

        internal Property(float duration, float sy, float ey)
        {
            this.Duration = duration;
            this.Sy = sy;
            this.Ey = ey;
        }
    }
}