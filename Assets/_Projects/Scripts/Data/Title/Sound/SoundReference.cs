using General;
using System;
using UnityEngine;

namespace Data.Title.Sound
{
    [Serializable]
    internal sealed class AudioSourceReference
    {
        [SerializeField]
        private AudioSource bgm;
        internal AudioSource BGM => bgm;

        [SerializeField]
        private AudioSource clickSE;
        internal AudioSource ClickSE => clickSE;
    }

    internal sealed class AudioClipReference : IDisposable
    {
        internal AudioClip BGM { get; private set; }
        internal AudioClip ClickSE { get; private set; }

        internal AudioClipReference(AudioClip bgm, AudioClip clickSE)
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

    internal sealed class SoundReference : IDisposable
    {
        private AudioSourceReference audioSources;
        private AudioClipReference audioClips;

        internal SoundReference(AudioSourceReference audioSources, AudioClipReference audioClips)
        {
            this.audioSources = audioSources;
            this.audioClips = audioClips;
        }

        internal void PlayBGM() => audioSources.BGM.Raise(audioClips.BGM, SoundType.BGM);
        internal void PlayClickSE() => audioSources.ClickSE.Raise(audioClips.ClickSE, SoundType.SE);

        public void Dispose()
        {
            audioClips.Dispose();

            audioSources = null;
            audioClips = null;
        }
    }
}