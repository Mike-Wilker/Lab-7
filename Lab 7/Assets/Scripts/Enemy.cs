using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class Enemy : MonoBehaviour
{
    protected float health = 0.0f;
    public abstract void takeDamage(float damage);
    public abstract void die();
}
