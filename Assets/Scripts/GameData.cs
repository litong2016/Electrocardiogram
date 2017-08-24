using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TerrainType
{
	None,
	Road,
	Mountain
}

public class GameData : MonoBehaviour
{

	private static GameData instance;

	public static GameData Instance {
		get { 
			if(instance == null)
				instance = new GameData();
			return instance;
		}
	}



	void Start ()
	{
//		instance = this;
	}

	public Vector3 initRoadPosition = new Vector3 (500, -120, 0);
	public Vector3 initMountainPosition = new Vector3 (500, -55f, 0);
	public float targetPositionX = -500;
	public float speed = 100;

	public List<GameObject> roadList= new List<GameObject>();
	public List<GameObject> mountainList= new List<GameObject>();
}
