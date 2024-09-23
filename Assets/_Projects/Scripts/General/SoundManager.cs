using SO;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace General
{
    internal enum SoundType
    {
        Master,
        BGM,
        SE
    }

    internal static class SoundManager
    {
        /// <summary>
        /// nullチェック済み
        /// SoundType.Masterは不可
        /// </summary>
        internal static void Raise(this AudioSource source, AudioClip clip, SoundType type)
        {
            if (source == null) return;
            if (clip == null) return;
            if (type is SoundType.Master) return;

            if (type is SoundType.BGM)
            {
                source.playOnAwake = false;
                source.loop = true;

                source.clip = clip;
                source.Play();
            }
            else if (type is SoundType.SE)
            {
                source.playOnAwake = false;
                source.loop = false;

                source.PlayOneShot(clip);
            }
            else throw new Exception("無効な種類です");
        }

        internal static float GetVolume(SoundType type)
        {
            if (am == null) return 0;
            am.GetFloat(type.ToParamNameString(), out float volume);
            return volume;
        }

        internal static void SetVolume(SoundType type, float newVolume)
        {
            if (am == null) return;
            am.SetFloat(type.ToParamNameString(), newVolume);
        }

        /// <summary>
        /// volumeDiffは[-10, 10]内
        /// </summary>
        internal static void ChangeVolume(SoundType type, float volumeDiff)
        {
            if (am == null) return;
            if (volumeDiff is not (>= -10 and <= 10)) return;

            SetVolume(type, GetVolume(type) + volumeDiff);
        }

        private static AudioMixer am => SO_Sound.Entity.AM;

        private static string ToParamNameString(this SoundType type)
            => type switch
            {
                SoundType.Master => "MasterParam",
                SoundType.BGM => "BGMParam",
                SoundType.SE => "SEParam",
                _ => throw new Exception("無効な種類です")
            };
    }
}