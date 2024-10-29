using System;
using UnityEngine;
using UnityEngine.Audio;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/SO_Sound", fileName = "SO_Sound")]
    public sealed class SO_Sound : ScriptableObject
    {
        #region

        public const string PATH = "SO_Sound";

        private static SO_Sound _entity = null;
        public static SO_Sound Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_Sound>(PATH);
                    if (_entity == null) Debug.LogError(PATH + " not found");
                }
                return _entity;
            }
        }

        #endregion

        [SerializeField]
        private AudioMixer am;
        internal AudioMixer AM => am;

        [Space(25)]

        [SerializeField, Header("BGM")]
        private BGMReference bgm;
        internal BGMReference BGM => bgm;

        [SerializeField, Header("SE")]
        private SEReference se;
        internal SEReference SE => se;
    }

    [Serializable]
    internal sealed class BGMReference
    {
        [SerializeField, Header("タイトル")]
        private AudioClip title;
        internal AudioClip Title => title;

        [SerializeField, Header("メインゲーム")]
        private AudioClip main;
        internal AudioClip Main => main;
    }

    [Serializable]
    internal sealed class SEReference
    {
        [SerializeField, Header("プレイヤー 関連")]
        private SE.Player player;
        internal SE.Player Player => player;

        [SerializeField, Header("警備員 関連")]
        private SE.Guardman guardman;
        internal SE.Guardman Guardman => guardman;

        [SerializeField, Header("汎用")]
        private SE.General general;
        internal SE.General General => general;
    }

    namespace SE
    {
        [Serializable]
        internal sealed class Player
        {
            [SerializeField, Header("ハンマーをスマッシュする")]
            private AudioClip smash;
            internal AudioClip Smash => smash;

            [SerializeField, Header("ハンマーでクラッシュする")]
            private AudioClip crash;
            internal AudioClip Crash => crash;

            [SerializeField, Header("スマッシュ不可")]
            private AudioClip unSmashable;
            internal AudioClip UnSmashable => unSmashable;

            [SerializeField, Header("クラッシュ失敗")]
            private AudioClip crashFailed;
            internal AudioClip CrashFailed => crashFailed;

            [SerializeField, Header("心臓の鼓動")]
            private AudioClip heartbeat;
            internal AudioClip Heartbeat => heartbeat;

            [SerializeField, Header("警備員に見つかる")]
            private AudioClip foundByGuardman;
            internal AudioClip FoundByGuardman => foundByGuardman;
        }

        [Serializable]
        internal sealed class Guardman
        {
            [SerializeField, Header("歩く")]
            private AudioClip walk;
            internal AudioClip Walk => walk;

            [SerializeField, Header("ドアに手をかける")]
            private AudioClip doorHandle;
            internal AudioClip DoorHandle => doorHandle;

            [SerializeField, Header("ドアを開く")]
            private AudioClip doorOpen;
            internal AudioClip DoorOpen => doorOpen;

            [SerializeField, Header("ドアを閉める")]
            private AudioClip doorClose;
            internal AudioClip DoorClose => doorClose;
        }

        [Serializable]
        internal sealed class General
        {
            [SerializeField, Header("クリックする")]
            private AudioClip click;
            internal AudioClip Click => click;
        }
    }
}