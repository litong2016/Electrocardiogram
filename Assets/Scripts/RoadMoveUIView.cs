using UnityEngine;
using System.Collections;

public class RoadMoveUIView : MonoBehaviour {
	[SerializeField]
	TerrainType type;

	// Use this for initialization
	void Start () {
//		Move ();
	}

	public void InitPos()
	{
		if(type== TerrainType.Mountain)
		{
			this.transform.localPosition = GameData.Instance.initMountainPosition;
		}
		else
		{
			this.transform.localPosition = GameData.Instance.initRoadPosition;
		}
		Move ();
	}

	void Move()
	{
		float time = (this.transform.localPosition.x - GameData.Instance.targetPositionX) / GameData.Instance.speed;
		LeanTween.moveLocalX (this.gameObject, GameData.Instance.targetPositionX, time).onComplete=()=>{
			if(type== TerrainType.Mountain)
			{
				if(!GameData.Instance.mountainList.Contains(this.gameObject))
					GameData.Instance.mountainList.Add(this.gameObject);
			}
			else
			{
				if(!GameData.Instance.roadList.Contains(this.gameObject))
					GameData.Instance.roadList.Add(this.gameObject);
			}
		};
	}
}
