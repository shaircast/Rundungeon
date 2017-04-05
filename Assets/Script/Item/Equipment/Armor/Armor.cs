using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Equipment
{
	public int defense;

	// Use this for initialization
	protected virtual new void Awake ()
	{
		base.Awake();
		Debug.Log("Armor awake");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	override public void EquipItem(int index) // override from Equipment.
	{
		base.EquipItem(index);
		Debug.Log("Armor Equipitem");
		EquipArmor(index); // specify for Armor.
	}

	public virtual void EquipArmor(int index) // overriden by each Armor item.
	{
		Debug.Log("Armor EquipArmor");
		// swap.
		GameObject temp;
		temp = player.armor;
		player.armor = gameObject;
		player.inventory[index] = temp;
		
		// make active/inactive
		player.armor.SetActive(true);
		if(player.inventory[index] != null)
		{
			player.inventory[index].SetActive(false);
		}
	}
}
