using System;
using UnityEngine;
using Interface;
using TMPro;

namespace Main.Data
{
    [Serializable]
    internal sealed class ScoreReference : IReference
    {
        [SerializeField]
        private TextMeshProUGUI scoreText;
        internal string ScoreText
        {
            get => scoreText == null ? string.Empty : scoreText.text;
            set { if (scoreText != null) scoreText.text = value; }
        }

        public void Dispose()
        {
            scoreText = null;
        }
    }
}