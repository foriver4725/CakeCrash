using Interface;
using System;
using UnityEngine;

namespace Handler.Main.BeltConbea
{
    internal sealed class BeltConbea : MonoBehaviour
    {
        [SerializeField, Header("速度")]
        float speed = 1f;
        [SerializeField, Header("リセットの場所")]
        Transform resetPoint;

        private void Update()
        {
            // 移動
            transform.position += Vector3.right * speed * Time.deltaTime;

            // リセット
            if (transform.position.x > 35f)
            {
                transform.position = resetPoint.position;
            }
        }
    }
}