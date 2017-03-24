using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOrigin : MonoBehaviour
{

	public GameObject tile;
	public GameObject roomOrigin;
	public GameObject roomManager;
	public int maxWidth;
	public int minWidth;
	public int maxHeight;
	public int minHeight;
	public StageManager.CornerType cornerType;
	public int cornerOriginOffset; // how many tiles will be overlapped when link diagonally.

	
	public void FormRoomAndOrigin() // form chunk of room and Origins at the corners.
	{
		if(cornerType == StageManager.CornerType.LL)
		{
			FormLL();
			//Debug.Log("LL");
		}
		else if(cornerType == StageManager.CornerType.LR)
		{
			FormLR();
			//Debug.Log("LR");
		}
		else if(cornerType == StageManager.CornerType.UL)
		{
			FormUL();
			//Debug.Log("UL");
		}
		else if(cornerType == StageManager.CornerType.UR)
		{
			FormUR();
			//Debug.Log("UR");
		}
	}

	// Form LL, LR, UL, UR is same each other except for the direction.
	void FormLL()
	{ 
		// set values.
		int width = Random.Range(minWidth, maxWidth+1);
		int height = Random.Range(minHeight, maxHeight+1);
		float interval = StageManager.tileSize;
		GameObject tileContainer = Instantiate(roomManager, gameObject.transform.position, Quaternion.identity);
		tileContainer.name = "Room" + StageManager.currentNumOfRooms;
		StageManager.roomList.Add(tileContainer);

		// make chunk of tiles and make other roomOrigin.
		for(int h = 0; h < height; h++)
		{
			for(int w = 0; w < width; w++)
			{
				// make tile and set WBD and wallType.
				Vector3 offset = new Vector3(-w * interval, -h * interval, 0f);
				GameObject newTile = Instantiate(tile, transform.position + offset, Quaternion.identity) as GameObject;
				newTile.name = "Tile";
				newTile.transform.parent = tileContainer.transform; // roomManager takes tile as child.
				newTile.layer = LayerMask.NameToLayer("Tile");
				Tile newTileSetup = newTile.GetComponent<Tile>();
				newTileSetup.horiPos = w;

				// set WBD.
				if(w == 0 || w == width -1 || h == 0 || h == height -1)
				{
					newTileSetup.wbd = false;
					newTile.tag = "BasicTile";
				}
				else
				{
					newTileSetup.wbd = true;
					newTile.tag = "WBD";
				}

				// set wallType.
				if(w == 0)
				{
					newTileSetup.wallType = Tile.WallType.Right;
				}
				else if(w == width-1)
				{
					newTileSetup.wallType = Tile.WallType.Left;
				}
				else if(h == 0)
				{
					newTileSetup.wallType = Tile.WallType.Up;
					if(w == 0 || w == width-1)
					{
						newTileSetup.wallType = Tile.WallType.Corner;
					}
				}
				else if(h == height-1)
				{
					newTileSetup.wallType = Tile.WallType.Down;
					if(w == 0 || w == width-1)
					{
						newTileSetup.wallType = Tile.WallType.Corner;
					}
				}
				
				// make 3 roomOrigins. LL -> no UR
				if(w == 0 +cornerOriginOffset && h == 0 +cornerOriginOffset) // UR of newRoom
				{
					//GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					//origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.UR;
					//origin.name = "RoomOrigin";
				}
				else if(w == width-1 -cornerOriginOffset && h == 0 +cornerOriginOffset)// UL of newRoom
				{
					GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.UL;
					origin.name = "RoomOrigin";

				}
				else if(w == 0 +cornerOriginOffset && h == height-1 -cornerOriginOffset)// LR of newRoom
				{
					GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.LR;
					origin.name = "RoomOrigin";
					
				}
				else if(w == width-1 -cornerOriginOffset && h == height-1 -cornerOriginOffset)// LL of newRoom
				{
					GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.LL;
					origin.name = "RoomOrigin";
				}
				
			}
		}

	}

	void FormLR()
	{ 
		// set values.
		int width = Random.Range(minWidth, maxWidth+1);
		int height = Random.Range(minHeight, maxHeight+1);
		float interval = StageManager.tileSize;
		GameObject tileContainer = Instantiate(roomManager, gameObject.transform.position, Quaternion.identity);
		tileContainer.name = "Room" + StageManager.currentNumOfRooms;
		StageManager.roomList.Add(tileContainer);

		// make chunk of tiles and make other roomOrigin.
		for(int h = 0; h < height; h++)
		{
			for(int w = 0; w < width; w++)
			{
				// make tile and set WBD and wallType.
				Vector3 offset = new Vector3(w * interval, -h * interval, 0f);
				GameObject newTile = Instantiate(tile, transform.position + offset, Quaternion.identity) as GameObject;
				newTile.name = "Tile";
				newTile.transform.parent = tileContainer.transform;
				newTile.layer = LayerMask.NameToLayer("Tile");
				Tile newTileSetup = newTile.GetComponent<Tile>();
				newTileSetup.horiPos = w;

				// set WBD.
				if(w == 0 || w == width -1 || h == 0 || h == height -1)
				{
					newTileSetup.wbd = false;
					newTile.tag = "BasicTile";
				}
				else
				{
					newTileSetup.wbd = true;
					newTile.tag = "WBD";
				}

				// set wallType.
				if(w == 0)
				{
					newTileSetup.wallType = Tile.WallType.Left;
				}
				else if(w == width-1)
				{
					newTileSetup.wallType = Tile.WallType.Right;
				}
				else if(h == 0)
				{
					newTileSetup.wallType = Tile.WallType.Up;
					if(w == 0 || w == width-1)
					{
						newTileSetup.wallType = Tile.WallType.Corner;
					}
				}
				else if(h == height-1)
				{
					newTileSetup.wallType = Tile.WallType.Down;
					if(w == 0 || w == width-1)
					{
						newTileSetup.wallType = Tile.WallType.Corner;
					}
				}
				
				// make 3 roomOrigins. LR -> no UL
				if(w == 0 +cornerOriginOffset && h == 0 +cornerOriginOffset) // UL of newRoom
				{
					//GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					//origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.LL;
					//origin.name = "RoomOrigin";
				}
				else if(w == width-1 -cornerOriginOffset && h == 0 +cornerOriginOffset)// UR of newRoom
				{
					GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.UR;
					origin.name = "RoomOrigin";

				}
				else if(w == 0 +cornerOriginOffset && h == height-1 -cornerOriginOffset)// LL of newRoom
				{
					GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.LL;
					origin.name = "RoomOrigin";
					
				}
				else if(w == width-1 -cornerOriginOffset && h == height-1 -cornerOriginOffset)// LR of newRoom
				{
					GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.LR;
					origin.name = "RoomOrigin";
				}
				
			}
		}

	}

	void FormUL()
	{ 
		// set values.
		int width = Random.Range(minWidth, maxWidth+1);
		int height = Random.Range(minHeight, maxHeight+1);
		float interval = StageManager.tileSize;
		GameObject tileContainer = Instantiate(roomManager, gameObject.transform.position, Quaternion.identity);
		tileContainer.name = "Room" + StageManager.currentNumOfRooms;
		StageManager.roomList.Add(tileContainer);

		// make chunk of tiles and make other roomOrigin.
		for(int h = 0; h < height; h++)
		{
			for(int w = 0; w < width; w++)
			{
				// make tile and set WBD and wallType.
				Vector3 offset = new Vector3(-w * interval, h * interval, 0f);
				GameObject newTile = Instantiate(tile, transform.position + offset, Quaternion.identity) as GameObject;
				newTile.name = "Tile";
				newTile.transform.parent = tileContainer.transform;
				newTile.layer = LayerMask.NameToLayer("Tile");
				Tile newTileSetup = newTile.GetComponent<Tile>();
				newTileSetup.horiPos = w;

				// set WBD.
				if(w == 0 || w == width -1 || h == 0 || h == height -1)
				{
					newTileSetup.wbd = false;
					newTile.tag = "BasicTile";
				}
				else
				{
					newTileSetup.wbd = true;
					newTile.tag = "WBD";
				}

				// set wallType.
				if(w == 0)
				{
					newTileSetup.wallType = Tile.WallType.Right;
				}
				else if(w == width-1)
				{
					newTileSetup.wallType = Tile.WallType.Left;
				}
				else if(h == 0)
				{
					newTileSetup.wallType = Tile.WallType.Down;
					if(w == 0 || w == width-1)
					{
						newTileSetup.wallType = Tile.WallType.Corner;
					}
				}
				else if(h == height-1)
				{
					newTileSetup.wallType = Tile.WallType.Up;
					if(w == 0 || w == width-1)
					{
						newTileSetup.wallType = Tile.WallType.Corner;
					}
				}
				
				// make 3 roomOrigins. UL -> no LR
				if(w == 0 +cornerOriginOffset && h == 0 +cornerOriginOffset) // LR of newRoom
				{
					//GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					//origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.LR;
					//origin.name = "RoomOrigin";
				}
				else if(w == width-1 -cornerOriginOffset && h == 0 +cornerOriginOffset)// LL of newRoom
				{
					GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.LL;
					origin.name = "RoomOrigin";

				}
				else if(w == 0 +cornerOriginOffset && h == height-1 -cornerOriginOffset)// UR of newRoom
				{
					GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.UR;
					origin.name = "RoomOrigin";
					
				}
				else if(w == width-1 -cornerOriginOffset && h == height-1 -cornerOriginOffset)// UL of newRoom
				{
					GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.UL;
					origin.name = "RoomOrigin";
				}
				
			}
		}

	}

	void FormUR()
	{ 
		// set values.
		int width = Random.Range(minWidth, maxWidth+1);
		int height = Random.Range(minHeight, maxHeight+1);
		float interval = StageManager.tileSize;
		GameObject tileContainer = Instantiate(roomManager, gameObject.transform.position, Quaternion.identity);
		tileContainer.name = "Room" + StageManager.currentNumOfRooms;
		StageManager.roomList.Add(tileContainer);

		// make chunk of tiles and make other roomOrigin.
		for(int h = 0; h < height; h++)
		{
			for(int w = 0; w < width; w++)
			{
				// make tile and set WBD and wallType.
				Vector3 offset = new Vector3(w * interval, h * interval, 0f);
				GameObject newTile = Instantiate(tile, transform.position + offset, Quaternion.identity) as GameObject;
				newTile.name = "Tile";
				newTile.transform.parent = tileContainer.transform;
				newTile.layer = LayerMask.NameToLayer("Tile");
				Tile newTileSetup = newTile.GetComponent<Tile>();
				newTileSetup.horiPos = w;

				// set WBD.
				if(w == 0 || w == width -1 || h == 0 || h == height -1)
				{
					newTileSetup.wbd = false;
					newTile.tag = "BasicTile";
				}
				else
				{
					newTileSetup.wbd = true;
					newTile.tag = "WBD";
				}

				// set wallType.
				if(w == 0)
				{
					newTileSetup.wallType = Tile.WallType.Left;
				}
				else if(w == width-1)
				{
					newTileSetup.wallType = Tile.WallType.Right;
				}
				else if(h == 0)
				{
					newTileSetup.wallType = Tile.WallType.Down;
					if(w == 0 || w == width-1)
					{
						newTileSetup.wallType = Tile.WallType.Corner;
					}
				}
				else if(h == height-1)
				{
					newTileSetup.wallType = Tile.WallType.Up;
					if(w == 0 || w == width-1)
					{
						newTileSetup.wallType = Tile.WallType.Corner;
					}
				}
				
				// make 3 roomOrigins. UR -> no LL
				if(w == 0 +cornerOriginOffset && h == 0 +cornerOriginOffset) // LL of newRoom
				{
					//GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					//origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.LL;
					//origin.name = "RoomOrigin";
				}
				else if(w == width-1 -cornerOriginOffset && h == 0 +cornerOriginOffset)// LR of newRoom
				{
					GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.LR;
					origin.name = "RoomOrigin";

				}
				else if(w == 0 +cornerOriginOffset && h == height-1 -cornerOriginOffset)// UL of newRoom
				{
					GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.UL;
					origin.name = "RoomOrigin";

					
				}
				else if(w == width-1 -cornerOriginOffset && h == height-1 -cornerOriginOffset)// UR of newRoom
				{
					GameObject origin = Instantiate(roomOrigin, transform.position + offset, Quaternion.identity) as GameObject;
					origin.GetComponent<RoomOrigin>().cornerType = StageManager.CornerType.UR;
					origin.name = "RoomOrigin";

				}

				
			}
		}

	}

}
