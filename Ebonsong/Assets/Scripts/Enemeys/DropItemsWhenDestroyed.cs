using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemsWhenDestroyed : MonoBehaviour
{
    [System.Serializable]
    private class Prefab
    {
        public GameObject prefab;
        [Range(0f, 1f)] public float spawnChance;
    }

    [SerializeField] private Prefab[] prefabs;

    private void OnDestroy()
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
}
