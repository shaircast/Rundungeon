using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment 
{
	public int attack;
	public float pushbackPower;
	public float cooldown;
	
	// Use this for initialization
	protected virtual new void Awake () 
	{
		base.Awake();
		Debug.Log("Weapon awake");
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	override public void EquipItem(int index) // override from Equipment.
	{
		base.EquipItem(index);
		Debug.Log("Weapon Equipitem");
		EquipWeapon(index); // specify for Weapon.
	}

	public virtual void EquipWeapon(int index) // overriden by each weapon item.
	{
		Debug.Log("Weapon EquipWeapon");
		// swap.
		GameObject temp;
		temp = player.weapon;
		player.weapon = gameObject;
		player.inventory[index] = temp;
		
		// make active/inactive
		player.weapon.SetActive(true);
		if(player.inventory[index] != null)
		{
			player.inventory[index].SetActive(false);
		}
	}

}
