using Interface;
using System;
using UnityEngine;

namespace Handler.Main.BeltConbea
{
    internal sealed class BeltConbea : MonoBehaviour
    {
        [SerializeField, Header("���x")]
        float speed = 1f;
        [SerializeField, Header("���Z�b�g�̏ꏊ")]
        Transform resetPoint;

        private void Update()
        {
            // �ړ�
            transform.position += Vector3.right * speed * Time.deltaTime;

            // ���Z�b�g
            if (transform.position.x > 35f)
            {
                transform.position = resetPoint.position;
            }
        }
    }
}