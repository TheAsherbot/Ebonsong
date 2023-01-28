using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerNameSpace;

namespace Enemeys
{
    public class EnemeyA : MonoBehaviour
    {
        [SerializeField] private int speed = 10;
        [SerializeField] private GameObject bolletPrefab;
        [SerializeField] private Transform[] moveTransforms;

        private bool isAttacking;
        private bool isShooting;
        private int currentMoveTranssformNumber;
        private float rotationOffset = 180;
        private Transform turentTransform;
        private Transform playerTransform;

        private void Start()
        {
            turentTransform = transform.GetChild(1);
        }

        private void Update()
        {
            if (isAttacking && !isShooting)
            {
                turentTransform.rotation = LookAt(rotationOffset, turentTransform);
            }
            else Move();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>())
            {
                if (!isAttacking)
                {
                    playerTransform = collision.transform;
                    StartCoroutine(AttackSequence());
                }
            }
        }

        private void Move()
        {
            Transform currentMovePoint = moveTransforms[currentMoveTranssformNumber];
            float minDistanceAwayFromMovePoint = 0.2f;
            if (Vector3.Distance(transform.position, currentMovePoint.position) <= minDistanceAwayFromMovePoint)
            {
                currentMoveTranssformNumber++;
                if (currentMoveTranssformNumber >= moveTransforms.Length)
                {
                    currentMoveTranssformNumber = 0;
                    return;
                }
            }
            Vector3 moveDirection = (currentMovePoint.position - transform.position);
            transform.position += moveDirection.normalized * speed * Time.deltaTime;
        }

        private IEnumerator AttackSequence()
        {
            isAttacking = true;

            float timeTillStartShooting = 3f;
            yield return new WaitForSeconds(timeTillStartShooting);

            isShooting = true;
            float timeTillShoots = 0.5f;
            yield return new WaitForSeconds(timeTillShoots);

            Instantiate(bolletPrefab, turentTransform.position, turentTransform.rotation);
            isShooting = false;
            isAttacking = false;
        }

        private Quaternion LookAt(float rotationOffset, Transform rotatedObject)
        {
            if (playerTransform != null)
            {
                Vector3 vectorToTarget = playerTransform.position + new Vector3(0, 2) - rotatedObject.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationOffset;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                return Quaternion.Slerp(rotatedObject.rotation, q, Time.deltaTime);
            }
            return Quaternion.identity;
        }
    
    }
}
