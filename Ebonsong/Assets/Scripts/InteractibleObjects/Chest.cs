using UnityEngine;

public class Chest : MonoBehaviour
{
    [System.Serializable]
    private class Prefab
    {
        public GameObject prefab;
        [Range(0f, 1f)] public float spawnChance;
    }

    [SerializeField] Sprite halfChestSprite;
    [SerializeField] private Prefab[] prefabs;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnInteract()
    {
        int minIteams = 1;
        int maxIteams = 3;
        int numberOfIteamsSpawned = Random.Range(minIteams, maxIteams);

        for (int i = 0; i < numberOfIteamsSpawned; i++)
        {
            float minValue = 0.00000001f;
            float maxValue = 1f;
            float randomValue = Random.Range(minValue, maxValue);
            foreach (Prefab prefab in prefabs)
            {
                if (randomValue > prefab.spawnChance)
                {
                    randomValue -= prefab.spawnChance;
                }
                else
                {
                    GameObject gameObject = Instantiate(prefab.prefab, transform.position, Quaternion.identity);
                    try
                    {
                        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(1f, 10.1f));
                    }
                    catch (System.NullReferenceException error)
                    {
                        Debug.LogWarning(error);
                    }
                    break;
                }
            }
        }
        try
        {
            spriteRenderer.sprite = halfChestSprite;
            transform.position -= new Vector3(0, 0.5f);
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(this);
        }
        catch (System.NullReferenceException error)
        {
            Debug.LogWarning(error.ToString());
        }
    }
}
