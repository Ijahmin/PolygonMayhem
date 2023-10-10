using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] private float bulletForce = 500.0f;
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * bulletForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BlockProjectiles"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
