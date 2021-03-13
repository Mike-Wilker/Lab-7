using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float SPEED = 128.0f;
    const float DAMAGE = 2.0f;
    void Update()
    {
        transform.Translate(Vector3.forward * SPEED * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().takeDamage(DAMAGE);
            Destroy(gameObject);
        }
    }
}
