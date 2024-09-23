using System;
using UnityEngine;

namespace Reference.Title.Sound
{
    [Serializable]
    internal sealed class TitleAudioSourceReference
    {
        [SerializeField]
        private AudioSource bgm;
        internal AudioSource BGM => bgm;

        [SerializeField]
        private AudioSource clickSE;
        internal AudioSource ClickSE => clickSE;
    }

    internal sealed class TitleAudioClipReference : IDisposable
    {
        internal AudioClip BGM { get; private set; }
        internal AudioClip ClickSE { get; private set; }

        internal TitleAudioClipReference(AudioClip bgm, AudioClip clickSE)
        {
            BGM = bgm;
            ClickSE = clickSE;
        }

        public void Dispose()
        {
            BGM = null;
            ClickSE = null;
        }
    }
}