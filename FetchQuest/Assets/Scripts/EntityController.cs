using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    public int maxHealth = 0;
    protected int curHealth = 0;
    public int attackStrength = 0;
    public int attackSpeed = 0;

    protected RoomController room;
    public HealthBarVisualizer healthBar;

    // Start is called before the first frame update
    void Awake()
    {
        curHealth = maxHealth;
        room = FindAnyObjectByType<RoomController>();
    }

    public abstract void TakeDamage(int damage);
}
