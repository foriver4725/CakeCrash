﻿using Manager.Main;
using SO;
using UnityEngine;

namespace Handler.Main.Cake
{
    /// <summary>
    /// ケーキの吹っ飛び
    /// </summary>
    internal sealed class CakeKnockback : MonoBehaviour
    {

        [SerializeField, Header("ケーキのリジッドボディ")] Rigidbody cakeRb;

        internal void Hit()
        {

            if (SO_Cake.Entity.IsOutHitBox(transform.position.x)) return;

            gameObject.layer = SO_Cake.Entity.OnHitLayer;

            if (GameManager.Instance.RecentPressedColor.colorType == tag)
                cakeRb.AddForce(SO_Cake.Entity.KnockbackVector * SO_Cake.Entity.KnockbackPower, ForceMode.Impulse);

            else gameObject.SetActive(false);

            GameManager.Instance.CakeClear();
        }

        private void OnDisable()
        {
            cakeRb = null;
        }
    }
}


