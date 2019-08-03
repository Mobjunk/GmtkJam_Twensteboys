using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCamera : MonoBehaviour
{
    public Vector3 cinematicEndPosition;
    public float speed;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3((gameObject.transform.position.x - cinematicEndPosition.x) / speed * Time.fixedDeltaTime, (gameObject.transform.position.y - cinematicEndPosition.y) / speed * Time.fixedDeltaTime, (gameObject.transform.position.z - cinematicEndPosition.z) / speed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.transform.position != cinematicEndPosition)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - offset.x, gameObject.transform.position.y - offset.y, gameObject.transform.position.z - offset.z);
        }
    }
}
