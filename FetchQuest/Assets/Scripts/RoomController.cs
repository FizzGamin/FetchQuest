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

    //PlayerSkillCheckChoice
    private SkillCheckController.SkillCheck playerSkillCheck = SkillCheckController.SkillCheck.Poor;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        turnController = FindObjectOfType<TurnCounterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && SkillCheckController.instance.IsSkillCheckEnabled())
        {
            playerSkillCheck = SkillCheckController.instance.GetSkillCheck();
            SkillCheckController.instance.EnableSkillCheck(false);
        }
    }

    void Start()
    {
        //Spawn first enemy
        SpawnEnemy();

        //Testing turn controller
        turnController.AddTurn(enemy.gameObject.GetComponent<Image>().sprite, enemy);
        turnController.AddTurn(player.gameObject.GetComponent<Image>().sprite, player);
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
    }

    public void AttackPlayer()
    {
        Debug.Log("enemy attacking player: " + playerSkillCheck.ToString());
        if (player != null && enemy != null)
        {
            switch(playerSkillCheck)
            {
                case SkillCheckController.SkillCheck.Perfect:
                    player.TakeDamage(0);//Blocked
                    break;
                case SkillCheckController.SkillCheck.Good:
                    player.TakeDamage(enemy.attackStrength/2);//Sorta blocked
                    break;
                case SkillCheckController.SkillCheck.Average:
                    player.TakeDamage(enemy.attackStrength);//Took the hit
                    break;
                case SkillCheckController.SkillCheck.Poor:
                    player.TakeDamage(enemy.attackStrength*2);//Took a harder hit
                    break;
            }

            turnController.AddTurn(enemy.gameObject.GetComponent<Image>().sprite, enemy);
        }
        Reset();
    }

    public void AttackEnemy()
    {
        Debug.Log("player attacking enemy: " + playerSkillCheck.ToString());
        if (player != null && enemy != null)
        {
            switch (playerSkillCheck)
            {
                case SkillCheckController.SkillCheck.Perfect:
                    enemy.TakeDamage(enemy.attackStrength * 2);//critical hit
                    break;
                case SkillCheckController.SkillCheck.Good:
                    enemy.TakeDamage(enemy.attackStrength);//Took the hit
                    break;
                case SkillCheckController.SkillCheck.Average:
                    enemy.TakeDamage(enemy.attackStrength / 2);//Sorta blocked
                    break;
                case SkillCheckController.SkillCheck.Poor:
                    enemy.TakeDamage(0);//Blocked
                    break;
            }

            turnController.AddTurn(player.gameObject.GetComponent<Image>().sprite, player);
        }
        Reset();
    }

    public void Reset()
    {
        SkillCheckController.instance.ResetSkillCheck();
        playerSkillCheck = SkillCheckController.SkillCheck.Poor;
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
