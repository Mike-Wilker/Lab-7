using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boomer : Enemy
{
    const float MAX_HEALTH = 15.0f;
    const float EXPLODE_RADIUS = 16.0f;
    const float EXPLOSION_RANGE = 20.0f;
    const float EXPLOSION_DAMAGE = 7.5f;
    void Start()
    {
        health = MAX_HEALTH;
        GetComponent<NavMeshAgent>().SetDestination(FindObjectOfType<Goal>().transform.position);
    }
    void Update()
    {
        foreach (Turret turret in FindObjectsOfType<Turret>())
        {
            if ((turret.transform.position - transform.position).magnitude <= EXPLODE_RADIUS)
            {
                die();
            }
        }
        foreach (DoubleTurret turret in FindObjectsOfType<DoubleTurret>())
        {
            if ((turret.transform.position - transform.position).magnitude <= EXPLODE_RADIUS)
            {
                die();
            }
        }
    }
    public override void takeDamage(float damage)
    {
        health -= damage;
        Instantiate(Resources.Load<Transform>(@"Prefabs\One Shot Audio Source"), transform.position, Quaternion.identity).GetComponent<OneShotAudioSource>().setup(Resources.Load<AudioClip>(@"SFX\Hurt"), Random.Range(0.25f, 0.5f), Random.Range(1.5f, 2.0f));
        if (health <= 0.0f)
        {
            die();
        }
    }
    public override void die()
    {
        Instantiate(Resources.Load<Transform>(@"Prefabs\One Shot Audio Source"), transform.position, Quaternion.identity).GetComponent<OneShotAudioSource>().setup(Resources.Load<AudioClip>(@"SFX\Gun"), Random.Range(0.75f, 1.0f), Random.Range(0.75f, 1.25f));
        Instantiate(Resources.Load(@"Prefabs\Explosion"), transform.position, transform.rotation);
        foreach (Turret turret in FindObjectsOfType<Turret>())
        {
            if ((turret.transform.position - transform.position).magnitude <= EXPLOSION_RANGE)
            {
                turret.takeDamage(EXPLOSION_DAMAGE);
            }
        }
        foreach (DoubleTurret turret in FindObjectsOfType<DoubleTurret>())
        {
            if ((turret.transform.position - transform.position).magnitude <= EXPLOSION_RANGE)
            {
                turret.takeDamage(EXPLOSION_DAMAGE);
            }
        }
        Destroy(gameObject);
    }
}
