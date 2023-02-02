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
        private float speedToAttack = 15f;
        private Vector3 previusPosition;
        private Player player;

        private void Start()
        {
            player = transform.parent.parent.parent.GetComponent<Player>();
        }

        private void FixedUpdate()
        {
            speed = Vector3.Distance(transform.position, previusPosition) / Time.deltaTime;

            previusPosition = transform.position;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (GameManager.Instance.DamageWithSwordMethed1)
            {
                if (collision.GetComponent<EnemyHP>() && this.speed >= speedToAttack)
                {
                    collision.GetComponent<EnemyHP>().Damage(damageAmount);
                }
            }
            if (!GameManager.Instance.DamageWithSwordMethed1)
            {
                if (collision.GetComponent<EnemyHP>() && player.InputActions.LandMovement.Attack.WasPressedThisFrame())
                {
                    collision.GetComponent<EnemyHP>().Damage(damageAmount);
                }
            }

        }

    }
}
