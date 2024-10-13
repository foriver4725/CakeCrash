using UnityEngine;

namespace SO
{
    public enum Layer
    {
        UnHit = 6,
        OnHit = 7,
    }


    [CreateAssetMenu(menuName = "SO/SO_Cake", fileName = "SO_Cake")]
    public sealed class SO_Cake : ScriptableObject
    {
        #region

        public const string PATH = "SO_Cake";

        private static SO_Cake _entity = null;
        public static SO_Cake Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_Cake>(PATH);
                    if (_entity == null) Debug.LogError(PATH + " not found");
                }
                return _entity;
            }
        }

        #endregion

        [SerializeField, Range(0f, 50f), Header("ケーキを吹っ飛ばす力")]
        private float knockbackPower;
        internal float KnockbackPower => knockbackPower;

        [SerializeField, Header("ケーキを吹っ飛ばす方向")]
        private Vector3 knockbackVector;
        internal Vector3 KnockbackVector => knockbackVector;

        [SerializeField, Range(0f, 2f), Header("ハンマーとの接触を有効化する\nケーキのX座標の最大値（ワールド）")]
        private float hitboxXmax;
        internal float HitboxXmax => hitboxXmax;

        [SerializeField, Range(-2f, 0f), Header("ハンマーとの接触を有効化する\nケーキのX座標の最小値（ワールド）")]
        private float hitboxXmin;
        internal float HitboxXmin => hitboxXmin;

        [SerializeField, Header("ハンマーと衝突した後のケーキが、\nまだハンマーと衝突していない他のケーキと接触しないようにするためのレイヤー")]
        private Layer onHitLayer;
        internal int OnHitLayer => onHitLayer.LayerToInt();

        internal bool IsOutHitBox(float x) => x > hitboxXmax || x < hitboxXmin;

    }

    internal static class LayerEx
    {
        internal static int LayerToInt(this Layer layer)
            => (int)layer;
    }
}
