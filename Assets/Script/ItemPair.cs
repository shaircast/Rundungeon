using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemPair : MonoBehaviour
{
	public static List<string> potionColor;
	public static List<string> potionType;

	public static List<string> scrollRune;
	public static List<string> scrollType;

	// Use this for initialization
	void Awake()
	{	
		potionColor = new List<string>();
		potionType = new List<string>();
		scrollRune = new List<string>();
		scrollType = new List<string>();
		// data from csv;
		string[] potionColorData = {"red", "orange","yellow","green","blue"};
		string[] potionTypeData = {"heal", "jump", "immune", "poison", "haste"};
		string[] scrollRuneData = {"a","b","c","d","e"};
		string[] scrollTypeData = {"teleport", "examine", "xp", "upgrade", "cleanse"};
		
		foreach(string x in potionColorData){potionColor.Add(x);}
		foreach(string x in potionTypeData){potionType.Add(x);}
		foreach(string x in scrollRuneData){potionColor.Add(x);}
		foreach(string x in scrollTypeData){potionType.Add(x);}
	}
	void Start ()
	{
		RandomizePair();
	}

	void RandomizePair()
	{
		potionType = potionType.OrderBy(x => Random.value).ToList( );
		scrollType = scrollType.OrderBy(x => Random.value).ToList( );
	}

}
