using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerSpawner : MonoBehaviour
{
    const float TIME_BETWEEN_SPAWNS = 10.0f;
    float spawnTimer = TIME_BETWEEN_SPAWNS;
    void Update()
    {
        spawnTimer -= Time.deltaTime * (Mathf.Log(FindObjectOfType<HUD>().researchRate, 2) * (FindObjectOfType<HUD>().gameTimer / HUD.END_TIME));
        if (spawnTimer <= 0.0f)
        {
            Instantiate(Resources.Load(@"Prefabs\Boomer"), transform.position, transform.rotation);
            Instantiate(Resources.Load<Transform>(@"Prefabs\One Shot Audio Source"), transform.position, Quaternion.identity).GetComponent<OneShotAudioSource>().setup(Resources.Load<AudioClip>(@"SFX\Rumble"), Random.Range(0.5f, 0.75f), Random.Range(1.0f, 1.5f));
            spawnTimer = TIME_BETWEEN_SPAWNS;
        }
    }
}
