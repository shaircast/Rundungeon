using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBasis : MonoBehaviour
{
	protected bool triggered; // traps are for one-time use.
	public GameObject effectToObject; // buff will be attached to player.
	
	// Use this for initialization
	void Awake ()
	{
		triggered = false;
		gameObject.layer = LayerMask.NameToLayer("Tile");
	}
	
	protected GameObject giveBuffToOther(Collision2D other) // make buff and make it child. returns the buff.
	{
		triggered = true;
		GameObject newEffect = Instantiate(effectToObject, other.transform.position, Quaternion.identity);
		newEffect.transform.parent = other.transform;

		Debug.Log("buff given");
		return newEffect;
	}

	protected bool IsRightTrigger(Collision2D other)
	{
		return (!triggered && other.gameObject.layer == LayerMask.NameToLayer("Walking"));
	}

}