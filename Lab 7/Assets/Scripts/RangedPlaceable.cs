using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RangedPlaceable : MonoBehaviour
{
    const float TILE_SIZE = 8.0f;
    public GameObject placedObject;
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 128.0f, LayerMask.GetMask("Ranged")))
        {
            if (hit.normal.y == 1)
            {
                transform.position = new Vector3(round_to_tile_size(hit.point.x) + (hit.point.x >= 0 ? 0.5f * TILE_SIZE : -0.5f * TILE_SIZE), hit.point.y, round_to_tile_size(hit.point.z) + (hit.point.z >= 0 ? 0.5f * TILE_SIZE : -0.5f * TILE_SIZE));
                if (Input.GetMouseButton(0))
                {
                    Instantiate(placedObject, transform.position, transform.rotation);
                    FindObjectOfType<HUD>().placing = false;
                    Destroy(gameObject);
                }
            }
        }
    }
    private float round_to_tile_size(float value)
    {
        return value - value % TILE_SIZE;
    }
}
