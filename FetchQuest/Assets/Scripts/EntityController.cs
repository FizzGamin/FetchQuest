using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    public int health = 0;
    public int attackStrength = 0;
    public int attackSpeed = 0;

    public RoomController room;

    // Start is called before the first frame update
    void Awake()
    {
        room = FindAnyObjectByType<RoomController>();
    }

    public abstract void TakeDamage(int damage);
}
