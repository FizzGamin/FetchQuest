using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{
    private const float TURN_TIMER = 3f;

    PlayerController player;
    EnemyController enemy;
    TurnCounterController turnController;

    public Transform playerSpot; 
    public Transform enemySpot;

    public List<GameObject> roomEnemies = new List<GameObject>();
    public int curEnemyIndex = 0;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        turnController = FindObjectOfType<TurnCounterController>();
 
    }

    void Start()
    {
        //Spawn first enemy
        SpawnEnemy();

        //Testing turn controller
        turnController.AddTurn(enemy.gameObject.GetComponent<Image>().sprite, enemy);
        turnController.AddTurn(enemy.gameObject.GetComponent<Image>().sprite, enemy);
        turnController.AddTurn(player.gameObject.GetComponent<Image>().sprite, player);
        turnController.AddTurn(enemy.gameObject.GetComponent<Image>().sprite, enemy);
        turnController.AddTurn(enemy.gameObject.GetComponent<Image>().sprite, enemy);
        turnController.AddTurn(player.gameObject.GetComponent<Image>().sprite, player);

        StartCoroutine(GameLoop());
    }

    public IEnumerator GameLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(TURN_TIMER);

            //Enemy attack
            if(!turnController.IsPlayerTurn())
            {
                //Did player defend?
                //if()
                //else
                AttackPlayer();
            }
            //Player Attacking
            else
            {
                //Did attack succeed?
                //if()
                //else
                AttackEnemy();
            }
            turnController.RemoveCurrentTurn();
        }
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

        //populate turn cont based off attack speeds

    }

    public void AttackPlayer()
    {
        Debug.Log("enemy attacking player");
        if (player != null && enemy != null)
        {
            player.TakeDamage(enemy.attackStrength);
            turnController.AddTurn(enemy.gameObject.GetComponent<Image>().sprite, enemy);
        }
    }

    public void AttackEnemy()
    {
        Debug.Log("player attacking enemy");
        if (player != null && enemy != null)
        {
            enemy.TakeDamage(player.attackStrength);
            turnController.AddTurn(player.gameObject.GetComponent<Image>().sprite, player);
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
