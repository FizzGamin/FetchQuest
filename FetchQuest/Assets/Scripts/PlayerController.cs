using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    room.AttackEnemy();
        //}
    }

    override
    public void TakeDamage(int damage)
    {
        Debug.Log("player took " + damage + " damage");
        //take hit
        curHealth -= damage;
        healthBar.UpdateHealthBar(maxHealth, curHealth);

        //Check if dead
        if (curHealth <= 0)
        {
            //Player dies
            room.PlayerDied();
            Destroy(gameObject);
        }
    }
}
