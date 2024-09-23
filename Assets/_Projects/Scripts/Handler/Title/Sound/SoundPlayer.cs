using General;
using Reference.Title.Sound;
using System;

namespace Handler.Title.Sound
{
    internal sealed class SoundPlayer : IDisposable
    {
        private TitleAudioSourceReference audioSources;
        private TitleAudioClipReference audioClips;

        internal SoundPlayer(TitleAudioSourceReference audioSources, TitleAudioClipReference audioClips)
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