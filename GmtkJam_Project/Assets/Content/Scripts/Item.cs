using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] private int itemID = 0;
	public int ItemID => itemID;

	private void OnTriggerEnter(Collider other)
	{
		PlayerInventory pINV = other.GetComponent<PlayerInventory>();
		if (pINV != null)
		{
			pINV.PickUpKey(this);
			gameObject.SetActive(false);
		}
	}

    private void Start()
    {
        KillZone.Instance().BindOnDieEvent(()=>{gameObject.SetActive(true);});
    }
}
