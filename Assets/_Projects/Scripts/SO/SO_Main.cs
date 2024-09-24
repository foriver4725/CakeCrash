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

        [SerializeField, Range(5.0f, 180.0f), Header("��������")]
        private float timeLimit;
        internal float TimeLimit => timeLimit;

        [SerializeField, Header("�J�����ړ� �֘A")]
        private Data.Main.Player.PlayerSquat.Property cameraProperty;
        internal Data.Main.Player.PlayerSquat.Property CameraProperty => cameraProperty;

        [SerializeField, Range(-30.0f, 30.0f), Header("���z�̉�]�̎n�_/�I�_���A���x���炷��\n(���F������;���F�����)")]
        private float sunRotateOffset;
        internal float SunRotateOffset => sunRotateOffset;
    }
}