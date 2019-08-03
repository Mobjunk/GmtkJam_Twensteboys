using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class KillZone : Singleton<KillZone>
{
    [SerializeField, HideInInspector]
    private BoxCollider boxCollder;

    [SerializeField, HideInInspector]
    private Vector3 colliderSize;

    private UnityEvent onPlayerDiedEvent = new UnityEvent();

    private void OnValidate()
    {
        if (boxCollder == null)
        {
            boxCollder = GetComponent<BoxCollider>();

            boxCollder.center = Vector3.zero;
            boxCollder.size = colliderSize;
            boxCollder.isTrigger = true;
        }
    }

    public void SetCenter(Vector3 location)
    {
        boxCollder.center = Vector3.zero;
        transform.position = location;
    }

    public void SetSize(Vector2 size)
    {
        colliderSize = new Vector3(size.x, 2, size.y);
        boxCollder.size = colliderSize;
    }

    public void BindOnDieEvent(UnityAction action)
    {
        onPlayerDiedEvent.AddListener(action);
    }

    private void OnTriggerEnter(Collider other)
    {
        onPlayerDiedEvent.Invoke();
    }
}
