using Manager.Main;
using UnityEngine;

namespace Handler.Main.Cake
{
    /// <summary>
    /// ケーキの吹っ飛び
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    internal sealed class CakeKnockback : MonoBehaviour
    {
        private static readonly float knockBackPower = 20f;
        [SerializeField, Header("ケーキのリジッドボディ")] Rigidbody cakeRb;

        internal void Hit()
        {
            const float hitboxXmax = 1.8f, hitboxXmin = -1.8f;
            if (transform.position.x > hitboxXmax || transform.position.x < hitboxXmin) return;
            const int onHitLayer = 7;
            gameObject.layer = onHitLayer;

            if (GameManager.Instance.RecentPressedColor.colorType == tag) 
                cakeRb.AddForce(new Vector3(-1, 0.5f, 1) * knockBackPower, ForceMode.Impulse);

            else gameObject.SetActive(false);

        }

        private void OnDisable()
        {
           cakeRb = null;
        }
    }
}


