using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomManager : MonoBehaviour
{
	public enum RoomTheme
	{
		Normal, Treasure
	}

	public GameObject wallManager; // contains tiles and make rooms
	public RoomTheme theme;
	public GameObject enteranceDoor;
	public GameObject exitDoor;
	public GameObject[] traps;
	private float tileSize;
	public int[] themeNRatio; // type of theme = size of array. each num = percentage.

	void Awake() // set RoomTheme
	{
		tileSize = StageManager.tileSize;

		// add all percentage
		int sumRatio = 0;
		foreach(int ratio in themeNRatio)
		{
			sumRatio += ratio;
		}

		int randomlyChosen = Random.Range(0, sumRatio +1);
		// set theme
		for(int i = 0; i < themeNRatio.Length; i++)
		{
			if(themeNRatio[i] <= randomlyChosen)
			{
				theme = (RoomTheme) i;
			}
			else
			{
				randomlyChosen -= themeNRatio[i];
			}
		}
	}

	// With room design. Called in StageManager>SetRooms().
	public void MakeRoomByTheme(GameObject room)
	{
		if(theme == RoomManager.RoomTheme.Normal)
		{
			SetNormalRoom(room);
		}
		else if(theme == RoomManager.RoomTheme.Treasure)
		{
			SetTreasureRoom(room);
		}
	}

	public List<GameObject> allActiveTilesInRoom(GameObject room) // returns all active tiles in the room
	{
		List<GameObject> tilesInTheRoom = new List<GameObject>();
		foreach (Transform child in room.transform)
		{	
			if(!child.gameObject.activeSelf || child.gameObject.name != "Tile") continue;

			tilesInTheRoom.Add(child.gameObject);
		}
		return tilesInTheRoom;
	}


	
	// set the num of hazard first, and split it to each type.
	public void SetNormalRoom(GameObject room) // most common room. monster, wall, trap, etc.
	{
		// set nums of hazards & split.
		// NEED CSV here.
		int totalNumOfHazardsInARoom = Random.Range(5, 10);
		int randomNumOfWallsInARoom = Random.Range(0, totalNumOfHazardsInARoom +1); // Height of each wall -> WallOrigin.
		int randomNumOfTrapsInARoom = Random.Range(0, totalNumOfHazardsInARoom - randomNumOfWallsInARoom +1);
		int randomNumOfMonstersInARoom = Random.Range(0, totalNumOfHazardsInARoom - randomNumOfWallsInARoom - randomNumOfTrapsInARoom +1);

		// make tiles list of each side. copy the code in comment to make list in functions below.
		List<GameObject> activeTilesInRoom = allActiveTilesInRoom(room);
		//var downTileList = activeTilesInRoom.Where(tile => tile.GetComponent<Tile>().wallType == Tile.WallType.Down).Select(tile=>tile).ToList();
		//var upTileList = activeTilesInRoom.Where(tile => tile.GetComponent<Tile>().wallType == Tile.WallType.Up).Select(tile=>tile).ToList();
		//var leftTileList = activeTilesInRoom.Where(tile => tile.GetComponent<Tile>().wallType == Tile.WallType.Left).Select(tile=>tile).ToList();
		//var rightTileList = activeTilesInRoom.Where(tile => tile.GetComponent<Tile>().wallType == Tile.WallType.Right).Select(tile=>tile).ToList();

		// build hazards on unoccupied tile.
		makeNWallsInANormalRoom(randomNumOfWallsInARoom, activeTilesInRoom);
		makeNTrapsInANormalRoom(randomNumOfTrapsInARoom, activeTilesInRoom);
		
	}

	void makeNWallsInANormalRoom(int randomNumOfWallsInARoom, List<GameObject> activeTilesInRoom)
	{
		// leave only unoccupied down tile.
		var unoccupiedTileList = activeTilesInRoom.Where(tile => tile.GetComponent<Tile>().occupied == false
		 && tile.GetComponent<Tile>().wallType == Tile.WallType.Down).Select(tile=>tile).ToList();
		Debug.Log(gameObject.name + " walls:" + randomNumOfWallsInARoom);

		int currentNumOfWallsInARoom = 0;
		while(currentNumOfWallsInARoom < randomNumOfWallsInARoom)
		{	
			// pick available random tile.
			int randomIndex = Random.Range(0, unoccupiedTileList.Count);
			GameObject randomTileFromList = unoccupiedTileList[randomIndex];
			// make wallmanager at the position and make blocks.
			GameObject wallOrigin = Instantiate(wallManager, randomTileFromList.transform.position, Quaternion.identity);
			wallOrigin.name = "wallManager";
			wallOrigin.transform.parent = gameObject.transform;
			wallOrigin.GetComponent<WallManager>().makeBlockWallAtWallOrigin(wallManager);
			// process remaining property.
			randomTileFromList.GetComponent<Tile>().occupied = true;
			unoccupiedTileList.RemoveAt(randomIndex);
			currentNumOfWallsInARoom++;
		}
	}

	void makeNTrapsInANormalRoom(int randomNumOfTrapsInARoom, List<GameObject> activeTilesInRoom)
	{
		// leave only unoccupied down tile.
		var unoccupiedTileList = activeTilesInRoom.Where(tile => tile.GetComponent<Tile>().occupied == false
		 && tile.GetComponent<Tile>().wallType == Tile.WallType.Down).Select(tile=>tile).ToList();
		Debug.Log(gameObject.name + " traps:" + randomNumOfTrapsInARoom);

		int currentNumOfTrapsInARoom = 0;
		while(currentNumOfTrapsInARoom < randomNumOfTrapsInARoom)
		{	
			// pick available random tile.
			int randomIndex = Random.Range(0, unoccupiedTileList.Count);
			GameObject randomTileFromList = unoccupiedTileList[randomIndex];
			Transform temp = randomTileFromList.transform;
			// substitute tile with trap.
			randomTileFromList.GetComponent<Tile>().occupied = true;
			unoccupiedTileList.RemoveAt(randomIndex);
			randomTileFromList.SetActive(false);
			GameObject newTrap = Instantiate(traps[Random.Range(0,traps.Length)], temp.position, Quaternion.identity) as GameObject;
			newTrap.name = "Trap";
			newTrap.transform.parent = gameObject.transform;
			currentNumOfTrapsInARoom++;
		}
	}



	public void SetTreasureRoom(GameObject room)
	{
	}



	public void SetEntranceRoom(GameObject room)
	{
		// get downtiles in room.
		List<GameObject> activeTilesInRoom = allActiveTilesInRoom(room);
		var downTileList = activeTilesInRoom.Where(tile => tile.GetComponent<Tile>().wallType == Tile.WallType.Down).Select(tile=>tile).ToList();
		GameObject enterPlace = downTileList[Random.Range(0, downTileList.Count)];
		Vector3 offset = new Vector3(0f, tileSize/2, 0f);
		GameObject entrance = Instantiate(enteranceDoor, enterPlace.transform.position + offset, Quaternion.identity) as GameObject;
		entrance.name = "Entrance";
		enterPlace.GetComponent<Tile>().occupied = true;
	}

	public void SetExitRoom(GameObject room)
	{
		// get downtiles in room.
		List<GameObject> activeTilesInRoom = allActiveTilesInRoom(room);
		var downTileList = activeTilesInRoom.Where(tile => tile.GetComponent<Tile>().wallType == Tile.WallType.Down).Select(tile=>tile).ToList();
		GameObject exitPlace = downTileList[Random.Range(0, downTileList.Count)];
		Vector3 offset = new Vector3(0f, tileSize/2, 0f);
		GameObject entrance = Instantiate(exitDoor, exitPlace.transform.position + offset, Quaternion.identity) as GameObject;
		entrance.name = "Entrance";
		exitPlace.GetComponent<Tile>().occupied = true;
		SetNormalRoom(room);
	}


}
