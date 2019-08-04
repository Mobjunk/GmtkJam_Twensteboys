using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Door : MonoBehaviour
{
	[SerializeField] private int doorID = 0;
	[SerializeField] private Transform[] doors;
	[SerializeField] private int openingDuration;
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
				foreach(Transform door in doors)
				{
					door.DORotate(new Vector3(-90, 0, 90), openingDuration).SetEase(Ease.InOutCirc).Play();
				}
				GetComponent<Collider>().enabled = false;
			}
		}
	}
}
