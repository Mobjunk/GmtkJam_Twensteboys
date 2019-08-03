using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField] private int doorID = 0;
	public int DoorID => doorID;

	private void OnCollisionEnter(Collision collision)
	{
		PlayerInventory pINV = collision.collider.GetComponent<PlayerInventory>();
		if (pINV != null)
		{
			TryOpenDoor(pINV.Items);
		}
	}

	private void TryOpenDoor(List<Item> keyChain)
	{
		foreach(Item key in keyChain)
		{
			if(key.ItemID == doorID)
			{
				Destroy(this.gameObject);
			}
		}
	}
}
