using Main.Manager;
using SO;
using UnityEngine;

namespace Main.Handler
{
    /// <summary>
    /// ケーキの吹っ飛び
    /// </summary>
    internal sealed class CakeKnockbacker : MonoBehaviour
    {

        [SerializeField, Header("ケーキのリジッドボディ")] private Rigidbody cakeRb;

        internal void Hit()
        {

            if (SCake.Entity.IsOutHitBox(transform.position.x)) return;

            gameObject.layer = SCake.Entity.OnHitLayer;

            if (GameManager.Instance.RecentPressedColor.ColorType == tag)
                cakeRb.AddForce(SCake.Entity.KnockbackVector * SCake.Entity.KnockbackPower, ForceMode.Impulse);

            else gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wall/CakeDestroy"))
            {
                gameObject.SetActive(false);
                OnCakeHitWall();
            }
        }

        private void OnCakeHitWall()
        {
            GameManager.Instance.CakeCount++;
        }
    }
}
