using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    const float DAMAGE = 5.0f;
    const float MAX_HEALTH = 5.0f;
    float health = MAX_HEALTH;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().takeDamage(DAMAGE);
            health -= 1.0f;
            if(health <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
