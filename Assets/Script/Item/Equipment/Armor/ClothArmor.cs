using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothArmor : Armor
{
	public GameObject clothArmorPrefab;
	// Use this for initialization
	new void Awake ()
	{
		base.Awake();
		Debug.Log("ClothArmor awake");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	override public void EquipArmor(int index)
	{
		base.EquipArmor(index);
		Debug.Log("ClothArmor EquipArmor");
		EquipClothArmor(index);
	}

	void EquipClothArmor(int index) // instantiate and set position.
	{
		
	}
}
