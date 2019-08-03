using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTile : MonoBehaviour
{

    public float lowestPosition = -20;
    public float speed = 10; // in sec
    public GameObject tile;
    public bool reset = false;

    bool destroyed = false;
    float offset;
    float topPosition;

    private void OnTriggerExit(Collider other)
    {
        if (!destroyed)
        {
            destroyed = true;
        }
    }

    void Fall()
    {
        if (tile.transform.position.y != lowestPosition)
        {
            tile.transform.position = new Vector3(tile.transform.position.x, Mathf.Clamp(tile.transform.position.y - offset * Time.fixedDeltaTime, lowestPosition, topPosition), tile.transform.position.z);
        }
    }

    void Rise()
    {
        if (tile.transform.position.y != topPosition)
        {
            tile.transform.position = new Vector3(tile.transform.position.x, Mathf.Clamp(tile.transform.position.y + offset * Time.fixedDeltaTime, lowestPosition, topPosition), tile.transform.position.z);
        }
        else
        {
            destroyed = false;
            reset = false;
        }
    }

    private void FixedUpdate()
    {
        if (destroyed && !reset)
        {
            Fall();
        }
        else if (reset)
        {
            Rise();
        }
    }

    private void Start()
    {
        topPosition = tile.transform.position.y;
        offset = (topPosition - lowestPosition) / speed;
    }
}
