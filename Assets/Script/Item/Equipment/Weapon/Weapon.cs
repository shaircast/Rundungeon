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

	override public void EquipItem() // override from Equipment.
	{
		base.EquipItem();
		Debug.Log("Weapon Equipitem");
		EquipWeapon(); // specify for Weapon.
	}

	public virtual void EquipWeapon() // overriden by each weapon item.
	{
		Debug.Log("Weapon EquipWeapon");
	}
}
