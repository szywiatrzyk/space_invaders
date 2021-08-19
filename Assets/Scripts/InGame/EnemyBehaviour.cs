using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public int[] gridPos;
    public float minReloadTime;
    public float maxReloadTime;
    public float currMinReloadTime;
    public float currMaxReloadTime;
    public GameObject enemyBullet;
    public bool canShoot;

    private GameManager gameMenager;


    private void Awake()
    {
        gridPos = new int[2];
    }

    private void Start()
    {
        gameMenager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        currMaxReloadTime = maxReloadTime;
        currMinReloadTime = minReloadTime;
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        while (canShoot)
        {
            float rand = Random.Range(currMinReloadTime, currMaxReloadTime);
            yield return new WaitForSeconds(rand);
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
        }
    }


    public void setGridPos(int x, int y)
    {
        gridPos[0] = x;
        gridPos[1] = y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet" || collision.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "wall")
        {
            gameMenager.EndGame(1);
        }

    }


    private void OnDestroy()
    {
        GameObject contr = GameObject.FindGameObjectWithTag("controler");
        contr.GetComponent<EnemyManager>().CheckGrid();
        gameMenager.ScoreChange("EnemyDestruction");
    }
    public void MovementAction(string mode,float length)
    {
        if(mode == "left")
        {
            transform.position = new Vector2(transform.position.x -length, transform.position.y);
            setGridPos(gridPos[0]-1, gridPos[1]);
        }

        if (mode == "right")
        {
            transform.position = new Vector2(transform.position.x + length, transform.position.y);
            setGridPos(gridPos[0] + 1, gridPos[1]);
        }

        if (mode == "down")
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - length);
            setGridPos(gridPos[0], gridPos[1]+1);
        }

    }
}
