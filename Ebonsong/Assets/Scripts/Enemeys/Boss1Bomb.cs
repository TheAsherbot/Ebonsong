using PlayerNameSpace;
using System.Collections;
using UnityEngine;

public class Boss1Bomb : MonoBehaviour
{
    [SerializeField] private int damage = 5;

    private float explotionTime = 0.5f;
    private float explotionRadius = 5;
    private float raduisFromLastFram;
    private CircleCollider2D circleCollider;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(Explode(collision));
    }

    private IEnumerator Explode(Collision2D collision)
    {
        circleCollider.enabled = true;
        if (collision.gameObject.GetComponent<PlayerHealth>())
        {
            collision.gameObject.GetComponent<PlayerHealth>().Damage(damage);
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(0.05f);

        Destroy(gameObject);
    }

}
