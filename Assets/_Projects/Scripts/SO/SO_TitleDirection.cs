using Data.Title.TitleImage;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/SO_TitleDirection", fileName = "SO_TitleDirection")]
    public class SO_TitleDirection : ScriptableObject
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

        [SerializeField, Header("ƒ^ƒCƒgƒ‹‰æ‘œ‚ÌØ‚è‘Ö‚í‚è")]
        private TitleImageChangeProperty titleImageChangeProperty;
        internal TitleImageChangeProperty TitleImageChangeProperty => titleImageChangeProperty;
    }
}