using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
	public int durability;
	public int strengthNeed;
	public bool examined;

	public List<GameObject> buff;
	// Use this for initialization
	protected new virtual void Awake ()
	{
		base.Awake();
		// rotate 90 degree CCW
		Debug.Log("Equip awake");
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	override public void UseItem(int index) // override from Item.
	{
		base.UseItem(index);
		Debug.Log("Equip Useitem");
		EquipItem(index); // specify for Equipments.
	}

	public virtual void EquipItem(int index) // overriden by Weapon/Armor/Accessory
	{
		// Equipment : move item from inventory to player's slot & make it active.
		// Unequipment: move item from slot to inventoty and make it inactive.
		Debug.Log("Equip Equipitem");
	}

}