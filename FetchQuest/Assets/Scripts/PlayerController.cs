using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            room.AttackEnemy();
        }
    }

    override
    public void TakeDamage(int damage)
    {
        Debug.Log("player took " + damage + " damage");
        //take hit
        health -= damage;

        //Check if dead
        if (health <= 0)
        {
            //Player dies
            room.PlayerDied();
            Destroy(gameObject);
        }
    }
}
