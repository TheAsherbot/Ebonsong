using Pathfinding;
using PlayerNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyB : MonoBehaviour
    {

        [SerializeField] private GameObject bolletPrefab;

        private bool isAttacking;
        private bool isShooting;
        private float rotationOffset = 180;
        private Transform turentTransform;
        private Transform playerTransform;
        private Quaternion rotation180 = Quaternion.Euler(0, 0, 180);

        private void Start()
        {
            turentTransform = transform.GetChild(2).GetChild(0);
        }

        private void Update()
        {
            if (isAttacking && !isShooting)
            {
                turentTransform.rotation = LookAt(rotationOffset, turentTransform);
            }
        }

        private void LateUpdate()
        {
            CerectTurretRotation();
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

        private void CerectTurretRotation()
        {
            if (turentTransform.rotation.eulerAngles.z > 0 && turentTransform.rotation.eulerAngles.z < 90)
            {
                turentTransform.rotation = Quaternion.identity;
            }
            else if (turentTransform.rotation.eulerAngles.z < 180 && turentTransform.rotation.eulerAngles.z > 90)
            {
                turentTransform.rotation = rotation180;
            }
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
