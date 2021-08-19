using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float rowSPC; 
    public float columnSPC;
    public float enemyMovementDelay;
    public int howManyEnemyInRow;
    public int  howManyEnemyInColumn;

    private float currentMovementDelay;
    private int numOfEnemies;
    private int currentNumOfEnemies;
    public GameObject normalEnemy;
    public GameObject shootingEnemy;
    private bool moveDirection; //true -> right false -> left
    private int[,] grid;
    bool pause;

    //table
    int sizeY = 20;
    int sizeX = 13;
    void Start()
    {
        numOfEnemies = howManyEnemyInColumn * howManyEnemyInRow;
        currentMovementDelay = enemyMovementDelay;
        pause = false;
        moveDirection = true;
        grid = new int[sizeX,sizeY];
        rowSPC = rowSPC/sizeX;
        fillTable();
        createEnemies();
        StartCoroutine(EnemyMovementControler());
    }



    void fillTable() 
    {
        for( int i = (sizeX - howManyEnemyInRow) / 2; i < howManyEnemyInRow + (sizeX - howManyEnemyInRow) / 2; i++)
        {
            grid[i, 0] = 2;

            for (int j = 1; j < howManyEnemyInColumn; j++) 
            {
                grid[i, j] = 1;
            }
        }   
        
    }

    void createEnemies()
    {
        for (int i = 0; i < sizeX; i++)
        {

            for (int j = 0; j < sizeY ; j++)
            {
                if (grid[i, j] == 1) 
                {

                    GameObject enemyTmp = Instantiate(normalEnemy, new Vector2(transform.position.x + rowSPC * i, transform.position.y - columnSPC * j), Quaternion.identity);
                    enemyTmp.GetComponent<EnemyBehaviour>().setGridPos(i, j); 
                }
                if (grid[i, j] == 2) 
                {
                    GameObject enemyTmp = Instantiate(shootingEnemy, new Vector2(transform.position.x + rowSPC * i, transform.position.y - columnSPC * j), Quaternion.identity);
                    enemyTmp.GetComponent<EnemyBehaviour>().setGridPos(i, j);
                }

            }
        }
    }


    private IEnumerator EnemyMovementControler()
    {
        while (!pause)
        {
            yield return new WaitForSeconds(currentMovementDelay);

            int count = 0;
            for (int i = 0; i < sizeX - 1; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (grid[i, j] != 0) count++;

                }
            }
            currentNumOfEnemies = count;

            
            currentMovementDelay =Mathf.Clamp((enemyMovementDelay * (float)((float)currentNumOfEnemies / (float)numOfEnemies)), 0.2f,3.0f);
            Debug.Log(currentNumOfEnemies + "   " + numOfEnemies);
            Debug.Log(currentMovementDelay);
          

            float multiply = (currentNumOfEnemies / numOfEnemies);
                GameObject[] enemyList = GameObject.FindGameObjectsWithTag("enemy");
                foreach (GameObject enemy in enemyList)
                {
                    
                    enemy.GetComponent<EnemyBehaviour>().currMinReloadTime = Mathf.Clamp(enemy.GetComponent<EnemyBehaviour>().minReloadTime * multiply,1,5);
                    enemy.GetComponent<EnemyBehaviour>().currMaxReloadTime =Mathf.Clamp(enemy.GetComponent<EnemyBehaviour>().maxReloadTime * multiply,2,8);
                }



            if (currentNumOfEnemies <= 0 && !pause ) 
            {
                pause = true;
                GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().EndGame();
                
            }



            if (moveDirection)
            {
                bool dirchange = false;
                for (int i = 0; i < sizeY; i++)
                {                 
                    if (grid[sizeX - 1,i] != 0)
                    {
                        dirchange = true;
                        break;
                    }
                }

                if (dirchange)
                {
                    moveDirection = !moveDirection;
                    MovementAction("down");
                }
                else
                {
                    MovementAction("right");
                }

            }
            else
            {
                bool dirchange = false;
                for (int i = 0; i < sizeY; i++)
                {
                    if (grid[0,i] != 0)
                    {
                        dirchange = true;
                        break;
                    }
                }

                if (dirchange)
                {
                    moveDirection = !moveDirection;
                    MovementAction("down");
                }
                else
                {
                    MovementAction("left");
                }
            }
        }
    }

    void MovementAction(string mode)
    {
        CheckGrid();
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("enemy");
        GridUpdate(mode);
        foreach (GameObject enemy in enemyList)
        {
            enemy.GetComponent<EnemyBehaviour>().MovementAction(mode,rowSPC);  
        }
    } 

    void GridUpdate(string mode)
    {
        if (mode == "left")
        {
            for (int i = 0; i < sizeX-1; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    grid[i, j] = grid[i+1,j];
                }
            }

            for(int i = 0; i < sizeY; i++)
            {
                grid[sizeX-1, i] = 0;
            }

        }

        if (mode == "right")
        {
            for (int i = sizeX-1; i >= 1; i--)
            {
                for (int j = 0; j < sizeY; j++)
                {
                
                    grid[i, j] = grid[i - 1, j];
                }
            }

            for (int i = 0; i < sizeY; i++)
            {
                grid[0, i] = 0;
            }

        }

        if (mode == "down")
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = sizeY-1; j >=1; j--)
                {
                    grid[i, j] = grid[i , j-1];
                }
            }
        }

    }


    public void DeleteEnemyFromGrid(int x, int y)
    {
        grid[x, y] = 0;
    }

    private void CheckGrid()
    {

        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("enemy");

        int[,] GridCor = new int[sizeX, sizeY];

        foreach (GameObject enemy in enemyList)
        {
            int x =  enemy.GetComponent<EnemyBehaviour>().gridPos[0];
            int y = enemy.GetComponent<EnemyBehaviour>().gridPos[1];
            GridCor[x, y] = 1;
            
        }

        for (int i = 0; i < sizeX - 1; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (grid[i, j] != 0 && GridCor[i, j] == 0)
                    grid[i, j] = 0;
            }
        }

    }
}
