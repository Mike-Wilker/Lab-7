using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerSpawner : MonoBehaviour
{
    const float TIME_BETWEEN_SPAWNS = 5.0f;
    float spawnTimer = TIME_BETWEEN_SPAWNS;
    void Update()
    {
        spawnTimer -= Time.deltaTime * (Mathf.Log(FindObjectOfType<HUD>().researchRate, 2) * (0.25f + FindObjectOfType<HUD>().gameTimer / HUD.END_TIME));
        if (spawnTimer <= 0.0f)
        {
            Instantiate(Resources.Load(@"Prefabs\Charger"), transform.position, transform.rotation);
            Instantiate(Resources.Load<Transform>(@"Prefabs\One Shot Audio Source"), transform.position, Quaternion.identity).GetComponent<OneShotAudioSource>().setup(Resources.Load<AudioClip>(@"SFX\Rumble"), Random.Range(0.5f, 0.75f), Random.Range(1.25f, 1.75f));
            spawnTimer = TIME_BETWEEN_SPAWNS;
        }
    }
}
