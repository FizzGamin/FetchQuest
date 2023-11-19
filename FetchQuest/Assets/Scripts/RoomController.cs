using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    PlayerController player;
    EnemyController enemy;

    public Transform playerSpot; 
    public Transform enemySpot;

    public List<GameObject> roomEnemies = new List<GameObject>();
    public int curEnemyIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Spawn first enemy
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        //Check to see if this room is done
        if(curEnemyIndex > roomEnemies.Count - 1)
        {
            //Finish room
        }
        else
        {
            //Spawn new enemy
            enemy = Instantiate(roomEnemies[curEnemyIndex]).GetComponent<EnemyController>();
            enemy.transform.SetParent(enemySpot);
            enemy.transform.position = Vector3.zero;
            curEnemyIndex++;
        }

    }

    public void AttackPlayer()
    {
        if(player != null && enemy != null)
        {
            player.TakeDamage(enemy.attackStrength);

        }
    }

    public void AttackEnemy()
    {
        if (player != null && enemy != null)
        {
            enemy.TakeDamage(player.attackStrength);

        }
    }

    public void PlayerDied()
    {
        Debug.Log("player died");
        //Calc score or end game loop
    }

    public void EnemyDied()
    {
        Debug.Log("enemy died");
        //Add to the score, spawn next enemy
    }
}
