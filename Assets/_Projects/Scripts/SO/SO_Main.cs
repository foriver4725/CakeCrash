using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/SO_Main", fileName = "SO_Main")]
    public sealed class SO_Main : ScriptableObject
    {
        #region

        public const string PATH = "SO_Main";

        private static SO_Main _entity = null;
        public static SO_Main Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_Main>(PATH);
                    if (_entity == null) Debug.LogError(PATH + " not found");
                }
                return _entity;
            }
        }

        #endregion

        [SerializeField, Range(5.0f, 180.0f), Header("制限時間")]
        private float timeLimit;
        internal float TimeLimit => timeLimit;

        [SerializeField, Header("カメラ移動 関連")]
        private Data.Main.Player.PlayerSquat.Property cameraProperty;
        internal Data.Main.Player.PlayerSquat.Property CameraProperty => cameraProperty;

        [SerializeField, Range(-30.0f, 30.0f), Header("太陽の回転の始点/終点を、何度ずらすか\n(正：昼方向;負：夜方向)")]
        private float sunRotateOffset;
        internal float SunRotateOffset => sunRotateOffset;
    }
}