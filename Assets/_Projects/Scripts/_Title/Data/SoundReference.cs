using General;
using Interface;
using System;
using UnityEngine;

namespace Title.Data
{
    [Serializable]
    internal sealed class AudioSourceReference : IReference
    {
        [SerializeField]
        private AudioSource bgm;
        internal AudioSource BGM => bgm;

        [SerializeField]
        private AudioSource clickSE;
        internal AudioSource ClickSE => clickSE;

        public void Dispose()
        {
            bgm = null;
            clickSE = null;
        }
    }

    internal sealed class AudioClipReference : IReference
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

    internal sealed class SoundReference : IReference
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
            (audioSources as IReference)?.Dispose();
            (audioClips as IReference)?.Dispose();

            audioSources = null;
            audioClips = null;
        }
    }
}