using Interface;
using Title.Data;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/TitleDirection", fileName = "STitleDirection")]
    public sealed class STitleDirection : AScriptableObject<STitleDirection>
    {
        [SerializeField, Header("スタートボタンの切り替わり")]
        private StartImageProperty startImageProperty;
        internal StartImageProperty StartImageProperty => startImageProperty;

        [SerializeField, Range(+0.0f, 1.0f), Header("ボタンを押した後、何秒待つか")]
        private float waitDurOnPlaced;
        internal float WaitDurOnPlaced => waitDurOnPlaced;
    }
}