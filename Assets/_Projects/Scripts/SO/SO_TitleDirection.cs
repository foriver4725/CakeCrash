﻿using Data.Title.TitleImage;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/SO_TitleDirection", fileName = "SO_TitleDirection")]
    public sealed class SO_TitleDirection : ScriptableObject
    {
        #region

        public const string PATH = "SO_TitleDirection";

        private static SO_TitleDirection _entity = null;
        public static SO_TitleDirection Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = Resources.Load<SO_TitleDirection>(PATH);
                    if (_entity == null) Debug.LogError(PATH + " not found");
                }
                return _entity;
            }
        }

        #endregion

        [SerializeField, Header("タイトル画像の切り替わり")]
        private TitleImageChangeProperty titleImageChangeProperty;
        internal TitleImageChangeProperty TitleImageChangeProperty => titleImageChangeProperty;

        [SerializeField, Header("スタートボタンの切り替わり")]
        private StartImageProperty startImageProperty;
        internal StartImageProperty StartImageProperty => startImageProperty;

        [SerializeField, Range(+0.0f, 1.0f), Header("ボタンを押した後、何秒待つか")]
        private float waitDurOnPlaced;
        internal float WaitDurOnPlaced => waitDurOnPlaced;
    }
}