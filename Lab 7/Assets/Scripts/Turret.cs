using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    const float MAX_RANGE = 32.0f;
    const float SHOT_COOLDOWN = 1.0f;
    const float MAX_HEALTH = 10.0f;
    float shotTimer = 0.0f;
    float health = MAX_HEALTH;
    void Update()
    {
        shotTimer -= Time.deltaTime;
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            if ((transform.position - enemy.transform.position).magnitude < MAX_RANGE)
            {
                transform.LookAt(new Vector3(enemy.transform.position.x, enemy.transform.position.y + 4.0f, enemy.transform.position.z));
                if (shotTimer <= 0.0f)
                {
                    shotTimer = SHOT_COOLDOWN;
                    Instantiate(Resources.Load(@"Prefabs\Bullet"), transform.Find(@"Body/Barrel").position, transform.rotation);
                    Instantiate(Resources.Load(@"Prefabs\Muzzle Flash"), transform.Find("Body/Barrel").position, transform.rotation);
                    Instantiate(Resources.Load<Transform>(@"Prefabs\One Shot Audio Source"), transform.position, Quaternion.identity).GetComponent<OneShotAudioSource>().setup(Resources.Load<AudioClip>(@"SFX\Gun"), Random.Range(0.5f, 0.75f), Random.Range(1.5f, 2.0f));
                }
                break;
            }
        }
    }
    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0.0f)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Find("Indicator").GetComponent<Light>().color = new Color(Mathf.Clamp01(1.0f - 2.0f * health / MAX_HEALTH), health / MAX_HEALTH, health / MAX_HEALTH);
        }
    }
}
