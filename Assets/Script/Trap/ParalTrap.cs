using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalTrap : TrapBasis
{

	public float paralDuration;


	void OnCollisionEnter2D(Collision2D other) // only triggers when it's right trigger.
	{
		if(IsRightTrigger(other))
		{
			GameObject newEffect = giveBuffToOther(other);
			BuffBasis effectSetup = newEffect.GetComponent<BuffBasis>();
			effectSetup.started = true;
			effectSetup.duration = paralDuration;
			Debug.Log("paral collides player");
		}
	}


}
