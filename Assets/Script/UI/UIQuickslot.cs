using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuickslot : MonoBehaviour
{
	public static UIQuickslot singleton;

	public Transform[] slotPosition;

	public int[] inventoryIndexForSlot;

	// Use this for initialization
	void Awake()
	{
		singleton = this;
	}
	void Start ()
	{
		UpdateUIQuickslot();
	}
	
	public void LinkInventoryToSlot1 (int index)
	{

	}
	public void LinkInventoryToSlot2 (int index)
	{

	}
	public void LinkInventoryToSlot3 (int index)
	{

	}

	public void UpdateUIQuickslot()
	{
		
	}
}
