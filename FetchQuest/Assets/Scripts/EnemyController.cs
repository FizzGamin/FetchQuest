using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{
    override
    public void TakeDamage(int damage)
    {
        Debug.Log("enemy took " + damage + " damage");
        //take hit
        curHealth -= damage;
        healthBar.UpdateHealthBar(maxHealth, curHealth);

        //Check if dead
        if (curHealth <= 0)
        {
            //Enemy dies
            room.EnemyDied();
            Destroy(gameObject);
        }
    }
}
