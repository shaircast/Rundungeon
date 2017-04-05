using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public enum WallType
	{
		Up, Left, Right ,Down, Corner // up, left, right, down, corner.
	}
	public bool wbd; // will be destroyed.
	public bool occupied; // is object is there based on this tile?
	public WallType wallType;
	public int horiPos; // it's x position in the room.

	void Awake()
	{
		gameObject.layer = LayerMask.NameToLayer("Tile");
		occupied = false;
	}
	
}