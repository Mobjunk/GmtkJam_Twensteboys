using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	[SerializeField]private List<Item> items = new List<Item>();
	public List<Item> Items => items;

	public void PickUpKey(Item pickedUpKey)
	{
		foreach(Item key in items)
		{
			if(key == pickedUpKey)
			{
				return;
			}
		}
		items.Add(pickedUpKey);
	}

    private void Start()
    {
        KillZone.Instance().BindOnDieEvent(()=>{items.Clear();});
    }
}
