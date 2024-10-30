using Interface;
using UnityEngine;

namespace SO
{
    public enum Layer
    {
        UnHit = 6,
        OnHit = 7,
    }


    [CreateAssetMenu(menuName = "SO/Cake", fileName = "SCake")]
    public sealed class SCake : AScriptableObject<SCake>
    {
        [SerializeField, Range(0f, 50f), Header("ケーキを吹っ飛ばす力")]
        private float knockbackPower;
        internal float KnockbackPower => knockbackPower;

        [SerializeField, Header("ケーキを吹っ飛ばす方向")]
        private Vector3 knockbackVector;
        internal Vector3 KnockbackVector => knockbackVector;

        [SerializeField, Range(0f, 2f), Header("ハンマーとの接触を有効化する\nケーキのX座標の最大値（ワールド）")]
        private float hitboxXmax;

        [SerializeField, Range(-2f, 0f), Header("ハンマーとの接触を有効化する\nケーキのX座標の最小値（ワールド）")]
        private float hitboxXmin;

        [SerializeField, Header("ケーキの衝突時のレイヤー")]
        private Layer onHitLayer;
        internal int OnHitLayer => (int)onHitLayer;

        [SerializeField, Header("ケーキの非衝突時のレイヤー")]
        private Layer unhitLayer;
        internal int UnhitLayer => (int)unhitLayer;

        internal bool IsOutHitBox(float x) => x > hitboxXmax || x < hitboxXmin;
    }
}
