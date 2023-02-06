using Enemies;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PlayerNameSpace
{
    public class PlayerSowrd : MonoBehaviour
    {
        [SerializeField] int damageAmount = 2;

        private float speed;
        private float speedToAttack = 25f;
        private Vector3 previusPosition;

        private void FixedUpdate()
        {
            speed = Vector3.Distance(transform.position, previusPosition) / Time.deltaTime;

            previusPosition = transform.position;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<EnemyHP>() && this.speed >= speedToAttack)
            {
                collision.GetComponent<EnemyHP>().Damage(damageAmount);
            }
        }

    }
}
