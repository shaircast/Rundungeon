using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
	public int durability;
	public int strengthNeed;
	public bool examined;
	public bool equipped;

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

	override public void UseItem() // override from Item.
	{
		base.UseItem();
		Debug.Log("Equip Useitem");
		EquipItem(); // specify for Equipments.
	}

	public virtual void EquipItem() // overriden by Weapon/Armor/Accessory
	{
		Debug.Log("Equip Equipitem");
		equipped = true;
	}

}