using System.Collections;
using UnityEngine;
using PlayerNameSpace;
using Pathfinding;

namespace Enemies
{
    public class EnemyA : MonoBehaviour
    {
        [SerializeField] private bool isInBossFight = false;
        [SerializeField] private int speed = 10;
        [SerializeField] private GameObject bolletPrefab;
        [SerializeField] private Transform[] moveTransforms;

        private bool hasSeenPlayer;
        private bool isAttacking;
        private bool isShooting;
        private int currentMoveTranssformNumber;
        private float rotationOffset = 180;
        private Transform turentTransform;
        private Transform playerTransform;
        private Quaternion rotation180 = Quaternion.Euler(0, 0, 180);
        private AIPath aiPath;

        private void Start()
        {
            aiPath = GetComponent<AIPath>();
            turentTransform = transform.GetChild(1);

            aiPath.maxSpeed = speed;
        }

        private void Update()
        {
            if (isAttacking && !isShooting && hasSeenPlayer)
            {
                turentTransform.rotation = LookAt(rotationOffset, turentTransform);
            }
            else if (hasSeenPlayer) MoveTowardsPlayer();
            else Move();
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
            if (turentTransform.rotation.eulerAngles.z < 359 && turentTransform.rotation.eulerAngles.z > 270)
            {
                turentTransform.rotation = Quaternion.identity;
            }
            else if (turentTransform.rotation.eulerAngles.z > 180 && turentTransform.rotation.eulerAngles.z < 270)
            {
                turentTransform.rotation = rotation180;
            }
        }

        private void Move()
        {
            if (isInBossFight == false)
            {
                Transform currentMovePoint = moveTransforms[currentMoveTranssformNumber];
                float pointRechedDistance = 0.2f;
                if (Vector3.Distance(transform.position, currentMovePoint.position) <= pointRechedDistance)
                {
                    currentMoveTranssformNumber++;
                    if (currentMoveTranssformNumber >= moveTransforms.Length)
                    {
                        currentMoveTranssformNumber = 0;
                        return;
                    }
                }
                aiPath.destination = currentMovePoint.position;
            }
        }

        private void MoveTowardsPlayer()
        {
            float yOffset = 5;
            aiPath.destination = playerTransform.position + new Vector3(0, yOffset);
        }

        private IEnumerator AttackSequence()
        {
            hasSeenPlayer = true;
            isAttacking = true;
;
            float timeTillStartShooting = 1f;
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
