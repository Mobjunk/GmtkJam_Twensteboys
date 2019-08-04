using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
    [SerializeField]
    Vector3 position;
    static bool teleporterUsed = false;
    static GameObject teleporterThatGotUSed;

    void Start()
    {
        position = gameObject.transform.parent != null ? transform.parent.transform.position : transform.GetChild(0).transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player") && !teleporterUsed)
        {
            Transform transform = other.gameObject.transform;
            transform.position = new Vector3(position.x, position.y, position.z);
            teleporterUsed = true;
            teleporterThatGotUSed = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && teleporterThatGotUSed != this.gameObject)
        {
            teleporterUsed = false;
        }
    }
}
