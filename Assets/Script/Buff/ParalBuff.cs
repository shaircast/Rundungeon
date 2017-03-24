using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalBuff : BuffBasis
{
	private Living target;

	void Start()
	{
		target = gameObject.transform.parent.gameObject.GetComponent<Living>();
	}
	public override void DoBuff()
	{
		Debug.Log("do buff");
		target.cannotMove = true;
		target.cannotDo = true;
	}

	public override void UndoBuff()
	{
		Debug.Log("undo buff");
		target.cannotMove = false;
		target.cannotDo = false;
		Destroy(gameObject);
	}

	 public override IEnumerator StartTimer()
     {
         yield return new WaitForSeconds(duration);
         UndoBuff();
	 }
	 
}
