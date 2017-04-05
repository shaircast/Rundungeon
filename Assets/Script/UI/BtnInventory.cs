using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnInventory : MonoBehaviour
{
	// Use this for initialization
	bool isOpen;
	void Awake ()
	{
		isOpen = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(isOpen) // when inven's open, time slow.
		{
			Time.timeScale = 0.1f;
		}
		else
		{
			Time.timeScale = 1f;
		}
		
	}

	public void BtnInventoryOpenToggle()
	{
		isOpen = !isOpen;
		if(isOpen)
		{
			UIInventory.singleton.UpdateUIInventory();
		}
	}
	
}
