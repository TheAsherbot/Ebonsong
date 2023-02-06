using System.Collections;
using UnityEngine;
using Pathfinding;
using PlayerNameSpace;

namespace Enemies
{
    public class Boss1 : MonoBehaviour
    {
        [SerializeField] private int speed = 10;
        [SerializeField] private GameObject bombPrefab;
        [SerializeField] private GameObject attackWave1;
        [SerializeField] private GameObject attackWave2;

        private bool isIdleing;
        private bool finishedTask;
        private bool isAttacking;
        private bool isShooting;
        private bool hasSeenPlayer;
        private int currentTier = 1;
        private float minMoveX = 115;
        private float maxMoveX = 145;
        private float tier1Y = -14;
        private float tier2Y = -12;
        private float tier3Y = -10;
        private float idleTime
        {
            get
            {
                float minIdleTime = 0.5f;
                float maxIdleTime = 1f;
                return Random.Range(minIdleTime, maxIdleTime);
            }
        }
        private Transform playerTransform;
        private AIPath aiPath;
        private EnemyHP hp;

        private void Start()
        {
            hp = GetComponent<EnemyHP>();
            aiPath = GetComponent<AIPath>();
            playerTransform = GameObject.Find("Player").transform;

            aiPath.maxSpeed = speed;
            aiPath.destination = transform.position;
            hp.onHealthChanged += onHealthChanged;
        }

        private void Update()
        {
            if (hasSeenPlayer)
            {
                if (aiPath.reachedDestination && !isShooting && !isIdleing)
                {
                    finishedTask = true;
                }
                Debug.Log(finishedTask);

                if (finishedTask)
                {
                    ChoseNextTask();
                }

                if (isAttacking)
                {
                    if (Vector3.Distance(transform.position, playerTransform.position) <= 5.5f)
                    {
                        isAttacking = false;
                        StartCoroutine(Attack());
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>())
            {
                hasSeenPlayer = true;
                Destroy(GetComponent<CircleCollider2D>());
            }
        }

        private void ChoseNextTask()
        {
            int randomValue = Random.Range(1, 100);
            if (randomValue >= 1 && randomValue <= 60)
            {
                Move();
            }
            else if (randomValue >= 61 && randomValue <= 90)
            {
                StartCoroutine(Idle());
            }
            else if (randomValue >= 91 && randomValue <= 100)
            {
                StartAttackPlayer();
            }
        }

        private IEnumerator Idle()
        {
            isIdleing = true;
            finishedTask = false;
            aiPath.destination = transform.position;
            yield return new WaitForSeconds(idleTime);
            isIdleing= false;
        }

        private void StartAttackPlayer()
        {
            finishedTask = false;
            isAttacking = true;
            float yOffset = 5;
            aiPath.destination = playerTransform.position + new Vector3(0, yOffset);
        }
        
        private IEnumerator Attack()
        {
            isShooting = true;
            GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            aiPath.destination = transform.position;

            float wiatTime = 0.5f;
            yield return new WaitForSeconds(wiatTime);

            bomb.GetComponent<Rigidbody2D>().gravityScale = 1;

            isShooting = false;
            finishedTask = true;
        }

        private void Move()
        {
            finishedTask = false;

            float pointRechedDistance = 0.2f;
            Vector3 destination = new Vector3(Random.Range(minMoveX, maxMoveX), 0);

            if (currentTier == 1)
            {
                destination.y = tier1Y + Random.Range(-1f, 1f);
            }
            else if (currentTier == 2)
            {
                destination.y = tier2Y + Random.Range(-1f, 1f);
            }
            else if (currentTier == 3)
            {
                destination.y = tier3Y + Random.Range(-1f, 1f);
            }

            if (Vector3.Distance(transform.position, destination) <= pointRechedDistance)
            {
                return;
            }

            aiPath.destination = destination;
        }

        private void onHealthChanged(object sender, EnemyHP.OnHealthChanged eventData)
        {
            if (eventData.hp <= 10)
            {
                currentTier = 2;
                attackWave1.SetActive(true);
                Move();
            }
            if (eventData.hp <= 5)
            {
                currentTier = 3;
                Move();
                attackWave2.SetActive(true);
            }
        }
    }
}
