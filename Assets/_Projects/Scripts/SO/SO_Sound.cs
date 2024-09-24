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
        [SerializeField, Header("�^�C�g��")]
        private AudioClip title;
        internal AudioClip Title => title;

        [SerializeField, Header("���C���Q�[��")]
        private AudioClip main;
        internal AudioClip Main => main;
    }

    [Serializable]
    internal sealed class SEReference
    {
        [SerializeField, Header("�v���C���[ �֘A")]
        private SE.Player player;
        internal SE.Player Player => player;

        [SerializeField, Header("�x���� �֘A")]
        private SE.Guardman guardman;
        internal SE.Guardman Guardman => guardman;

        [SerializeField, Header("�ėp")]
        private SE.General general;
        internal SE.General General => general;
    }

    namespace SE
    {
        [Serializable]
        internal sealed class Player
        {
            [SerializeField, Header("�n���}�[���X�}�b�V������")]
            private AudioClip smash;
            internal AudioClip Smash => smash;

            [SerializeField, Header("�n���}�[�ŃN���b�V������")]
            private AudioClip crash;
            internal AudioClip Crash => crash;

            [SerializeField, Header("�X�}�b�V���s��")]
            private AudioClip unSmashable;
            internal AudioClip UnSmashable => unSmashable;

            [SerializeField, Header("�N���b�V�����s")]
            private AudioClip crashFailed;
            internal AudioClip CrashFailed => crashFailed;

            [SerializeField, Header("�S���̌ۓ�")]
            private AudioClip heartbeat;
            internal AudioClip Heartbeat => heartbeat;

            [SerializeField, Header("�x�����Ɍ�����")]
            private AudioClip foundByGuardman;
            internal AudioClip FoundByGuardman => foundByGuardman;
        }

        [Serializable]
        internal sealed class Guardman
        {
            [SerializeField, Header("����")]
            private AudioClip walk;
            internal AudioClip Walk => walk;

            [SerializeField, Header("�h�A�Ɏ��������")]
            private AudioClip doorHandle;
            internal AudioClip DoorHandle => doorHandle;

            [SerializeField, Header("�h�A���J��")]
            private AudioClip doorOpen;
            internal AudioClip DoorOpen => doorOpen;

            [SerializeField, Header("�h�A��߂�")]
            private AudioClip doorClose;
            internal AudioClip DoorClose => doorClose;
        }

        [Serializable]
        internal sealed class General
        {
            [SerializeField, Header("�N���b�N����")]
            private AudioClip click;
            internal AudioClip Click => click;
        }
    }
}