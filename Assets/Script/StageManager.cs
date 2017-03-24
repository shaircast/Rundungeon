using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
	public enum CornerType
	{
		UL, // upper left.
		UR, // upper right.
		LL, // lower left.
		LR // lower right.
	}

	public GameObject player;
	public GameObject tile;
	public GameObject roomOrigin; // at corners;
	
	public int maxNumOfRooms;
	public int minNumOfRooms;
	private int numOfRooms; // by Random.Range.
	static public List<GameObject> roomList; // list of parents of tiles(=RoomManager).
	static public int currentNumOfRooms;
	private int numOfTraps; // by Random.Range. 
	static public float tileSize; // tile is square(h == w).

	void Start()
	{
		Initialize();
		TileChunk();
		Dig();
		TileUntrigger();
		//DestroyInactive();
		SetRooms();
	}


	void Initialize() // set variables.
	{
		// to get tileSize.
		GameObject temp = Instantiate(tile, transform.position, transform.rotation) as GameObject;
		tileSize = temp.GetComponent<Renderer>().bounds.size.x;
		Destroy(temp);

		numOfRooms = Random.Range(maxNumOfRooms, minNumOfRooms);
		currentNumOfRooms = 0;
		roomList = new List<GameObject>();
		Instantiate(roomOrigin, transform.position, transform.rotation);
	}


	void TileChunk() // make rooms of chunk of tiles with loop.
	{
		while(currentNumOfRooms < numOfRooms)
		{	
			// find all roomOrigin.
			GameObject[] randomOrigins = GameObject.FindGameObjectsWithTag("RoomOrigin") as GameObject[];
			if(randomOrigins != null)
			{
				// create room at roomOrigin and make it inactive.
				GameObject randomOrigin = randomOrigins[Random.Range(0, randomOrigins.Length)];
				// Make tiles and let them be child of Room#(RoomManager).
				randomOrigin.GetComponent<RoomOrigin>().FormRoomAndOrigin();
				//Debug.Log(randomOrigin.GetInstanceID());
				randomOrigin.SetActive(false);
				currentNumOfRooms++;
			}
		}
	}


	void Dig() // if other tiles overlaps with tbdTiles, deactivate it.
	{
		GameObject[] wbdTiles = GameObject.FindGameObjectsWithTag("WBD") as GameObject[];
		foreach(GameObject wbdtile in wbdTiles)
		{
			Collider2D[] bumps = Physics2D.OverlapCircleAll(wbdtile.transform.position, tileSize/2 - 0.1f) as Collider2D[];
			foreach(Collider2D bump in bumps)
			{
				bump.gameObject.SetActive(false);
			}
			wbdtile.SetActive(false);
		}
	}


	void TileUntrigger() // make colliders of the not-destroyed-tiles non-trigger for player.
	{
		GameObject[] tilesLeft = GameObject.FindGameObjectsWithTag("BasicTile") as GameObject[];
		foreach(GameObject tileLeft in tilesLeft)
		{
			tileLeft.GetComponent<Collider2D>().isTrigger = false;
		}
	}


	void SetRooms() // with loops, deal with every room.
	{
		for(int i = 0; i < numOfRooms; i++)
		{
			GameObject room = roomList[i];
			RoomManager roomManager = room.GetComponent<RoomManager>();
			if(i == 0) // first room should be Entrance.
			{
				roomManager.GetComponent<RoomManager>().SetEntranceRoom(room);
			}
			else if(i == numOfRooms -1) // last room should be Exit.
			{
				roomManager.GetComponent<RoomManager>().SetExitRoom(room);
			}
			else
			{
				roomManager.GetComponent<RoomManager>().MakeRoomByTheme(room);
			}
		}
	}
	
}