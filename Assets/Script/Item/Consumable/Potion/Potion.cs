using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Consumable
{
	// each potion's property and value.
	//int heal_healAmount; fullhp


	// Use this for initialization
	protected virtual new void Awake () 
	{
		base.Awake();
		Debug.Log("Potion awake");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	override public void ConsumeItem(int index) // override from Consumement.
	{
		base.ConsumeItem(index);
		Debug.Log("Potion Consumeitem");
		ConsumePotion(index); // specify for Potion.
	}

	public virtual void ConsumePotion(int index) // overriden by each Potion item.
	{
		Debug.Log("Potion ConsumePotion");
		
	}

	public void ApplyPotionEffect(string effect)
	{
		if(effect == "heal")
		{
			player.hp = player.maxHp;
			Debug.Log("healPotion");
		}
		else if(effect == "jump")
		{
			Debug.Log("jumpPotion");
		}
		else if(effect == "immune")
		{
			Debug.Log("immunePotion");
		}
		else if(effect == "haste")
		{
			Debug.Log("hastePotion");
		}
		else if(effect == "poison")
		{
			Debug.Log("poisonPotion");
		}
	}
}
