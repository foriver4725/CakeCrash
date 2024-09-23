using System;
using UnityEngine;
using UnityEngine.Audio;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/SO_Sound", fileName = "SO_Sound")]
    public class SO_Sound : ScriptableObject
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
        [SerializeField, Header("クリック")]
        private AudioClip click;
        internal AudioClip Click => click;
    }
}