using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private bool _enabled = false;

    public void StartFollow()
    {
        _enabled = true;
    }

    public void StopFollowing()
    {
        _enabled = false;
    }

    private Vector3 _camOffset;

    private Transform PlayerTransform;
  // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameManager.Instance().GetPlayer.transform;
        _camOffset = GameManager.Instance().CameraOffset;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (_enabled)
        {
            transform.position = PlayerTransform.position + _camOffset;
        }
    }
}
