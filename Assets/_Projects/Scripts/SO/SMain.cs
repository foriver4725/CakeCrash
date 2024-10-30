using UnityEngine;
using Main.Data;
using Interface;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/Main", fileName = "SMain")]
    public sealed class SMain : AScriptableObject<SMain>
    {
        [SerializeField, Range(5.0f, 180.0f), Header("制限時間")]
        private float timeLimit;
        internal float TimeLimit => timeLimit;

        [SerializeField, Header("カメラ移動 関連")]
        private PlayerProperty cameraProperty;
        internal PlayerProperty CameraProperty => cameraProperty;

        [SerializeField, Range(-30.0f, 30.0f), Header("太陽の回転の始点/終点を、何度ずらすか\n(正：昼方向;負：夜方向)")]
        private float sunRotateOffset;
        internal float SunRotateOffset => sunRotateOffset;

        [SerializeField, Header("ベルトコンベア 関連")]
        private BeltConveyorProperty beltConvyorProperty;
        internal BeltConveyorProperty BeltConvyorProperty => beltConvyorProperty;

        [SerializeField, Header("ハンマー 関連")]
        private HammerProperty hammerProperty;
        internal HammerProperty HammerProperty => hammerProperty;
    }
}