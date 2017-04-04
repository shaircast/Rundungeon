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
	override public void EquipWeapon()
	{
		base.EquipWeapon();
		Debug.Log("Sword Equipweapon");
		EquipSword();
	}

	void EquipSword() // instantiate and set position.
	{
		PlayerController player = PlayerController.singleton;
		GameObject newSword = Instantiate(swordPrefab, player.transform.position, Quaternion.identity) as GameObject;
		
		if(player.goRight)
		{
			newSword.transform.eulerAngles = new Vector3(0, 0, -90f);
		}
		else
		{
			newSword.transform.eulerAngles = new Vector3(0, 0, 90f);
		}
		newSword.transform.parent = player.transform;
		player.weapon = newSword;
	}

}