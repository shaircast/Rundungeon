using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{

	// Use this for initialization
	protected new virtual void Awake ()
	{
		base.Awake();
		// rotate 90 degree CCW
		Debug.Log("Consum awake");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	override public void UseItem(int index) // override from Item.
	{
		base.UseItem(index);
		Debug.Log("Consum Useitem");
		ConsumeItem(index); // specify for Equipments.
	}

	public virtual void ConsumeItem(int index) // overriden by Weapon/Armor/Accessory
	{
		Debug.Log("Consum Consumeitem");
		player.inventory.RemoveAt(index);
		UIInventory.singleton.UpdateUIInventory();
		UIQuickslot.singleton.UpdateUIQuickslot();
	}
}
