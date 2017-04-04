using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public string itemName;
	public string description;
	public int weight;

	// Use this for initialization
	protected void Awake ()
	{
		Debug.Log("Item awake");
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void ShowInfo()
	{
		
	}

	public virtual void UseItem()
	{
		// overridden by Equipment/Consumables.
		Debug.Log("Item Useitem");
	}
}
