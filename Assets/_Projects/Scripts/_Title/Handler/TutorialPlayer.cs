using IA;
using Interface;
using UnityEngine;
using Title.Data;

namespace Title.Handler
{
    internal sealed class TutorialPlayer : IHandler
    {
        private TitleMovieReference @ref;

        internal TutorialPlayer(TitleMovieReference @ref) => this.@ref = @ref;

        private float timer = 0;
        private bool isGreen => InputGetter.Instance.Main_GreenClick.Bool;
        private bool isBlue => InputGetter.Instance.Main_BlueClick.Bool;
        private bool isAnyColor => isBlue | isGreen;
        private bool isVideoActive
        {
            set
            {
                if (@ref.Video == null) return;
                @ref.Video.gameObject.SetActive(value);
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
            else if (timer < @ref.DisplayStartTime)
            {
                timer += Time.deltaTime;
            }

            // 何も押していない時間がしきい値を超えたら表示する 
            // SetActiveしてるだけ
            isVideoActive = (timer >= @ref.DisplayStartTime);
        }

        public void Dispose() => @ref = null;
    }
}