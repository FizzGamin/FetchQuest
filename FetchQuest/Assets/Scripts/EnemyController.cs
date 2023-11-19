using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{
    public void Start()
    {
        StartCoroutine(AttackLoop());
    }

    public IEnumerator AttackLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(attackSpeed);
            room.AttackPlayer();
        }

    }

    override
    public void TakeDamage(int damage)
    {
        Debug.Log("enemy took " + damage + " damage");
        //take hit
        health -= damage;

        //Check if dead
        if (health <= 0)
        {
            //Enemy dies
            room.EnemyDied();
            Destroy(gameObject);
        }
    }
}
