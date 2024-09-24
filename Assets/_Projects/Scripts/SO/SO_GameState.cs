using Data.General;
using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/SO_GameState", fileName = "SO_GameState")]
    public sealed class SO_GameState : ScriptableObject
    {
        #region

        public const string PATH = "SO_GameState";

        private static SO_GameState _entity = null;
        public static SO_GameState Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_GameState>(PATH);
                    if (_entity == null) Debug.LogError(PATH + " not found");
                }
                return _entity;
            }
        }

        #endregion

        [SerializeField, Header("ゲームステート\nデフォルトは最初、順番に切り替わる")]
        private SerializedGameState[] gameStates;
        internal ReadOnlyCollection<SerializedGameState> GameStates => Array.AsReadOnly(gameStates);
    }
}