using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    // movement
    public float speed;
    bool movementLeft;
    bool movementRight;

    // shoot 
    public GameObject bullet;
    public float reloadTime;
    private float atackSpeed;
    public bool canShoot;
    Coroutine shooting;

    GameManager gameManager;

    void Start()
    {
        atackSpeed = reloadTime;
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        movementLeft = false;
        movementRight = false;
        shooting = StartCoroutine(Shoot());
    }

    void Update()
    {
        if (movementLeft) transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        if (movementRight) transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }


    public IEnumerator Shoot()
    {
        while (canShoot)
        {
            yield return new WaitForSeconds(atackSpeed);
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }

    public void AddShootBoost()
    {
        StartCoroutine(ShootBoost());
    }
    public IEnumerator ShootBoost() 
    {
        StopCoroutine(shooting);
        atackSpeed =  reloadTime/4f;
        shooting = StartCoroutine(Shoot());
        yield return new WaitForSeconds(5);
        atackSpeed = reloadTime;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemyBullet" || collision.tag == "enemy")
        {
            gameManager.ScoreChange("PlayerHit");
        }
    }


    public void MovementLeftDown(){
        movementLeft = true;
    }
    public void MovementLeftUp()
    {
        movementLeft = false;
    }

    public void MovementRightDown()
    {
        movementRight = true;
    }
    public void MovementRightUp()
    {
        movementRight = false;
    }

}
