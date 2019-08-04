using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Finish : Singleton<Finish>
{
    private UnityEvent OnPlayerFinishedEvent = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            OnPlayerFinishedEvent.Invoke();
        }
    }

    public void BindOnFinishedEvent(UnityAction action)
    {
        OnPlayerFinishedEvent.AddListener(action);
    }
    
}
