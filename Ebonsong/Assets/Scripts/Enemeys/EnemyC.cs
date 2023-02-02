using Pathfinding;
using PlayerNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyC : MonoBehaviour
    {
        [SerializeField] private int speed = 10;
        [SerializeField] private GameObject bolletPrefab;
        [SerializeField] private Transform[] moveTransforms;

        private bool hasSeenPlayer;
        private bool isAttacking;
        private bool isShooting;
        private int currentMoveTranssformNumber;
        private float startYPosition;
        private Quaternion y180Rotation;
        private Transform bolletSpawnPoint;
        private Transform playerTransform;
        private AIPath aiPath;

        private void Start()
        {
            aiPath = GetComponent<AIPath>();
            bolletSpawnPoint = transform.GetChild(0);

            y180Rotation = Quaternion.Euler(0, 180, 0);
            startYPosition = transform.position.y;
            aiPath.maxSpeed = speed;
        }

        private void Update()
        {
            if (hasSeenPlayer) MoveTowardsPlayer();
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
            if (isShooting == false)
            {
                Vector3 currentMovePoint = moveTransforms[currentMoveTranssformNumber].position;
                float pointRechedDistance = 0.2f;
                currentMovePoint = new Vector3(currentMovePoint.x, startYPosition);
                if (Mathf.Abs(transform.position.x - currentMovePoint.x) <= pointRechedDistance)
                {
                    currentMoveTranssformNumber++;
                    if (currentMoveTranssformNumber >= moveTransforms.Length)
                    {
                        currentMoveTranssformNumber = 0;
                        return;
                    }
                }
                currentMovePoint = new Vector3(currentMovePoint.x, startYPosition);
                aiPath.destination = currentMovePoint;
                if (transform.position.x - currentMovePoint.x < 0 && isShooting == false)
                {
                    transform.rotation = y180Rotation;
                }
                else if (transform.position.x - currentMovePoint.x > 0 && isShooting == false)
                {
                    transform.rotation = Quaternion.identity;
                }
            }
        }

        private void MoveTowardsPlayer()
        {
            float endReachedDistance = 5;
            if (Mathf.Abs(transform.position.x - playerTransform.position.x) >= endReachedDistance && isShooting == false)
            {
                Vector3 destination = new Vector3(playerTransform.position.x, transform.position.y, 0);
                aiPath.destination = destination;
            }
            else
            {
                aiPath.destination = transform.position;
            }
            if (transform.position.x - playerTransform.position.x < 0 && isShooting == false)
            {
                transform.rotation = y180Rotation;
            }
            else if (transform.position.x - playerTransform.position.x > 0 && isShooting == false)
            {
                transform.rotation = Quaternion.identity;
            }
        }

        private IEnumerator AttackSequence()
        {
            hasSeenPlayer = true;
            isAttacking = true;

            float timeTillStartShooting = 3f;
            yield return new WaitForSeconds(timeTillStartShooting);

            isShooting = true;
            float timeTillShoots = 0.5f;
            yield return new WaitForSeconds(timeTillShoots);

            Instantiate(bolletPrefab, bolletSpawnPoint.position, bolletSpawnPoint.rotation);
            isShooting = false;
            isAttacking = false;
        }

    }
}
