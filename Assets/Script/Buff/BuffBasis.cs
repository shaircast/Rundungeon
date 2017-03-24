using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffBasis : MonoBehaviour
{
	public bool started;
	public bool applied;
	public float duration;

	void Awake()
	{
		started = false;
		applied = false;
	}
	
	void Update ()
	{
		if(started && !applied)
		{
			StartCoroutine("StartTimer");
			DoBuff();
			applied = true;
		}
	}
	
	public abstract void DoBuff();
	public abstract void UndoBuff();
	public abstract IEnumerator StartTimer();

}