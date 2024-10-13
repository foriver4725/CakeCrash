using Manager.Main;
using SO;
using UnityEngine;

namespace Handler.Main.Cake
{
    /// <summary>
    /// ケーキの吹っ飛び
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    internal sealed class CakeKnockback : MonoBehaviour
    {

        [SerializeField, Header("ケーキのリジッドボディ")] Rigidbody cakeRb;

        internal void Hit()
        {
            float
                hitboxXmax = SO_Cake.Entity.HitboxXmax,
                hitboxXmin = SO_Cake.Entity.HitboxXmin;

            if (transform.position.x > hitboxXmax || transform.position.x < hitboxXmin) return;

            gameObject.layer = SO_Cake.Entity.OnHitLayer;

            if (GameManager.Instance.RecentPressedColor.colorType == tag)
                cakeRb.AddForce(SO_Cake.Entity.KnockbackVector * SO_Cake.Entity.KnockbackPower, ForceMode.Impulse);

            else gameObject.SetActive(false);

        }

        private void OnDisable()
        {
            cakeRb = null;
        }
    }
}


