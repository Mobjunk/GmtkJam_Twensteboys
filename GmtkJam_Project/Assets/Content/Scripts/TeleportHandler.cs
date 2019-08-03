using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
    [SerializeField]
    Vector3 position;

    void Start()
    {
        position = gameObject.transform.parent != null ? transform.parent.transform.position : transform.GetChild(0).transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            Transform transform = other.gameObject.transform;
            transform.localPosition = new Vector3(position.x, transform.position.y, position.z + 2);
        }
    }
}
