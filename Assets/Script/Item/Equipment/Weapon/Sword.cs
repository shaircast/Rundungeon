using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon 
{
	public GameObject swordPrefab;
	// Use this for initialization
	new void Awake ()
	{
		base.Awake();
		Debug.Log("Sword awake");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	override public void EquipWeapon(int index)
	{
		base.EquipWeapon(index);
		Debug.Log("Sword Equipweapon");
		EquipSword(index);
	}

	void EquipSword(int index) // set position.
	{
		
	}

}