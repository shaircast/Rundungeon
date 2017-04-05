using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
	public static UIInventory singleton;
	public Transform weaponSlot;
	public Transform armorSlot;
	public Transform accessorySlot1;
	public Transform accessorySlot2;
	
	public Transform[] slotPosition;

	// Use this for initialization
	void Awake()
	{
		singleton = this;
	}
	void Start ()
	{
		UpdateUIInventory();
	}
	
	// Update Contents.
	public void UpdateUIInventory()
	{
		
	}

	public void UIInventoryToggle()
	{
		gameObject.SetActive(!gameObject.activeSelf);
	}

	

}
