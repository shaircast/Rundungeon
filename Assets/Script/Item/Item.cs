using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public PlayerController player;
	public string itemName;
	public string description;
	public int weight;
	public int gold;

	// Use this for initialization
	protected void Awake ()
	{
		player = PlayerController.singleton;
		Debug.Log("Item awake");
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void ShowInfo(int index)
	{
		
	}

	public virtual void UseItem(int index)
	{
		// overridden by Equipment/Consumables.
		Debug.Log("Item Useitem");
	}
}