using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
	public GameObject block;
	public int minTileHeight;
	public int maxTileHeight;
	private int tileHeight;
	

	void Awake()
	{
		tileHeight = Random.Range(minTileHeight, maxTileHeight+1);
	}

	public void makeBlockWallAtWallOrigin(GameObject wallOrigin)
	{
		for(int i = 1; i < tileHeight +1; i++)
		{
			Vector3 offset = new Vector3(0f, StageManager.tileSize * i, 0f);
			GameObject newBlock = Instantiate(block, gameObject.transform.position + offset, Quaternion.identity) as GameObject;
			newBlock.name = "Block";
			newBlock.layer = LayerMask.NameToLayer("Wall");
			newBlock.transform.parent = gameObject.transform;
		}
	}
	
}
