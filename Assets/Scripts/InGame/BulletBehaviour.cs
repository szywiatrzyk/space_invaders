using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float bulletSpeed; 
    public float destroyTime;
    public float dir;

    void Start()
    {
        Destroy(gameObject, destroyTime); 
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x , transform.position.y + dir * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "enemy" && dir > 0)
        {
            Destroy(gameObject);
        }

        if (other.tag == "Player" && dir < 0)
        {
            Destroy(gameObject);
        }

        if (other.tag == "wall")
        {
            Destroy(gameObject);
        }

    }
}
