using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBasis : Living
{
	public enum monsterTier
	{
		Normal, Trained, Elite, Hero
	}

	// basic stats
	public monsterTier tier;
	public int armor;
	public int atk;
	public float atkCd; // cooldown.
	public float atkPb; // pushing back.
	public int grantXp; // give to player when dies.
	public GameObject item; // drop it when dies.
	public List<GameObject> buff; // good&bad -> monsters doesn't purify themselves.

	// status
	public bool chasing;
	public bool alert;

	// Update -> moving should be defined in each monster.
	new protected virtual void Update()
	{
		base.Update();
		// monster moves when they're alert(not sleepeing)
		if(!alert)
		{
			cannotDo = true;
			cannotMove = true;
		}
	}

	void MonsterAwake()
	{
		alert = true;
		cannotDo = false;
		cannotMove = false;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			PushedMeback(other.gameObject, 30f);
		}
	}


}