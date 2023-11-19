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
        player = FindObjectOfType<PlayerController>();
        //Spawn first enemy
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        //Check to see if this room is done
        if (curEnemyIndex > roomEnemies.Count - 1)
        {
            //Finish room
            Debug.Log("Room was finished");
        }
        else
        {
            //Spawn new enemy
            Debug.Log("Spawning enemy");
            enemy = Instantiate(roomEnemies[curEnemyIndex]).GetComponent<EnemyController>();
            enemy.transform.SetParent(enemySpot);
            enemy.transform.localPosition = Vector3.zero;
            enemy.transform.localScale = Vector3.one;
            curEnemyIndex++;
        }
    }

    public void AttackPlayer()
    {
        Debug.Log("enemy attacking player");
        if (player != null && enemy != null)
        {
            player.TakeDamage(enemy.attackStrength);

        }
    }

    public void AttackEnemy()
    {
        Debug.Log("player attacking enemy");
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

        SpawnEnemy();
    }
}
