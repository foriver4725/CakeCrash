using System;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Title.Tutorial
{
    [Serializable]
    internal sealed class RefPro
    {
        [SerializeField, Range(1f, 60f), Header("�\������܂ł̎���")]
        private float displayStartTime;
        [SerializeField, Header("�\�����铮��,Canvas�̎q�I�u�W�F�N�g")]
        private Image video;

        internal float DisplayStartTime => displayStartTime;
        internal Image Video => video;
    }
}