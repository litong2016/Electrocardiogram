using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TerrainControllerView : MonoBehaviour
{

	bool isGaming = false;
	[SerializeField]
	Camera m_MoveCamera;
	[SerializeField]
	GameObject m_Car;

	List<RoadMoveUIView> allRoadList = new List<RoadMoveUIView> ();

	// Use this for initialization
	void Start ()
	{
		StartGame ();
		RoadMoveUIView[] temp = GetComponentsInChildren<RoadMoveUIView> ();
		for (int i = 0; i < temp.Length; i++)
			allRoadList.Add (temp [i]);
	}

	Coroutine generateTerrainCoroutine;

	void StartGame ()
	{
		isGaming = true;
		generateTerrainCoroutine = StartCoroutine (GenerateTerrain ());
	}

	IEnumerator GenerateTerrain ()
	{
		float time = (float)(100) / GameData.Instance.speed;
		int generateRoadCount = 0;
		Vector3 newPos = Vector3.zero;
		RoadMoveUIView terrain;
		while (isGaming) {

			float m_targetX = m_Car.transform.localPosition.x + 100;
			LeanTween.moveLocalX (m_Car, m_targetX, time);
			GetCurrentTerrain ();
			yield return new WaitForSeconds (time);
			float lastRoadX = allRoadList [allRoadList.Count - 1].transform.localPosition.x;
			if (generateRoadCount == 0) {
				generateRoadCount = Random.Range (1, 4);
//				if (GameData.Instance.mountainList.Count <= 0) {
				terrain = Instantiate (Resources.Load<RoadMoveUIView> ("mountain"))as RoadMoveUIView;
				newPos = new Vector3 (lastRoadX + 100, -55, 0);

//			} else {
//					terrain = GameData.Instance.mountainList [0];
//					GameData.Instance.mountainList.Remove (terrain);
//				}
				
			} else {
				generateRoadCount--;
//				if (GameData.Instance.roadList.Count <= 0) {
				terrain = Instantiate (Resources.Load<RoadMoveUIView> ("road"))as RoadMoveUIView;
				newPos = new Vector3 (lastRoadX + 100, -120, 0);
//				terrain.transform.localPosition = newPos;
				//				} else {
//					terrain = GameData.Instance.roadList [0];
//					GameData.Instance.roadList.Remove (terrain);
//				}
			}

			terrain.transform.SetParent (this.transform);
			terrain.transform.localScale = Vector3.one;
			terrain.transform.localPosition = newPos;
			allRoadList.Add (terrain);
			Destroy (allRoadList [0].gameObject);
			allRoadList.RemoveAt (0);
//			terrain.GetComponent<RoadMoveUIView> ().InitPos ();

		}
	}

	void LateUpdate ()
	{
		m_MoveCamera.transform.position = new Vector3 (m_Car.transform.position.x, m_MoveCamera.transform.position.y, m_MoveCamera.transform.position.z);
	}

	void GetCurrentTerrain ()
	{
		for (int i = 0; i < allRoadList.Count; i++) {
			if ((int)(m_Car.transform.position.x - allRoadList [i].transform.position.x) == 0) {
				allRoadList [i].GetComponent<Image> ().color = Color.black;
			}
		}
	}
}
