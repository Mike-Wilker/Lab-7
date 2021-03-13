using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Charger : Enemy
{
    const float MAX_HEALTH = 10.0f;
    void Start()
    {
        health = MAX_HEALTH;
        GetComponent<NavMeshAgent>().SetDestination(FindObjectOfType<Goal>().transform.position);
    }
    public override void takeDamage(float damage)
    {
        health -= damage;
        Instantiate(Resources.Load<Transform>(@"Prefabs\One Shot Audio Source"), transform.position, Quaternion.identity).GetComponent<OneShotAudioSource>().setup(Resources.Load<AudioClip>(@"SFX\Hurt"), Random.Range(0.25f, 0.5f), Random.Range(1.75f, 2.25f));
        if (health <= 0.0f)
        {
            die();
        }
    }
    public override void die()
    {
        Destroy(gameObject);
    }
}
