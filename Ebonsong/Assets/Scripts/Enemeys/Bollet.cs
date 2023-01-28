using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bollet : MonoBehaviour
{
    private int lifeTime = 5;
    private float speed = 10f;

    void Update()
    {
        Destroy(gameObject, lifeTime);
        transform.localPosition += -transform.right * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
