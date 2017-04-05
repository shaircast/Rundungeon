using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPotion : Potion
{
	public static bool examined; // by each potion.
	// Use this for initialization
	new void Awake ()
	{
		base.Awake();
		Debug.Log("RedPotion awake");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	override public void ConsumePotion(int index)
	{
		base.ConsumePotion(index);
		Debug.Log("RedPotion ConsumePotion");
		ConsumeRedPotion(index);
	}

	void ConsumeRedPotion(int index) // set position.
	{
		ApplyPotionEffect(ItemPair.potionType[ItemPair.potionColor.IndexOf("red")]);
	}
}
