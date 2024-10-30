using General;
using Interface;
using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/GameState", fileName = "SGameState")]
    public sealed class SGameState : AScriptableObject<SGameState>
    {
        [SerializeField, Header("ゲームステート\nデフォルトは最初、順番に切り替わる")]
        private SerializedGameState[] gameStates;
        internal ReadOnlyCollection<SerializedGameState> GameStates => Array.AsReadOnly(gameStates);
    }
}