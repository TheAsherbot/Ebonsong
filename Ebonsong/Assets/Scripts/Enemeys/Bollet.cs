using UnityEngine;
using PlayerNameSpace;

namespace Enemies
{
    public class Bollet : MonoBehaviour
    {
        [SerializeField] private int damage;

        private int lifeTime = 5;
        private float speed = 10f;

        void Update()
        {
            Destroy(gameObject, lifeTime);
            transform.localPosition += -transform.right * speed * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerHealth>())
            {
                collision.gameObject.GetComponent<PlayerHealth>().Damage(damage);
            }
            Destroy(this.gameObject);
        }
    }
}
