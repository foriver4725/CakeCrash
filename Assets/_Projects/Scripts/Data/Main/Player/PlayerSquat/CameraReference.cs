using Interface;
using System;
using UnityEngine;

namespace Data.Main.Player.PlayerSquat
{
    [Serializable]
    internal sealed class CameraReference : IReference
    {
        [SerializeField]
        private Transform cameraTf;
        internal Transform CameraTf => cameraTf;

        [SerializeField]
        private AudioSource squatSE;
        internal AudioSource SquatSE => squatSE;

        [SerializeField]
        private AudioSource standupSE;
        internal AudioSource StandupSE => standupSE;

        public void Dispose()
        {
            cameraTf = null;
            squatSE = null;
            standupSE = null;
        }
    }
}