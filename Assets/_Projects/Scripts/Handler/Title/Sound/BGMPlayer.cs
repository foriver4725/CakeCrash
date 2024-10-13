using Interface;
using System;

namespace Handler.Title.Sound
{
    internal sealed class BGMPlayer : IHandler
    {
        private Action playBGM;
        internal BGMPlayer(Action playBGM) => this.playBGM = playBGM;
        public void Dispose() => playBGM = null;
        public void Start() { if (playBGM is not null) playBGM(); }
        public void Update() { }
    }
}