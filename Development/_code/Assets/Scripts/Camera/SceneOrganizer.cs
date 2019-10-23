using UnityEngine;
using System.Collections;
//

public class SceneOrganizer : MonoBehaviour {
	
	public float Distance = 0f;
	private int TotalScenes;
	float CenterX;
	GameObject[] Scenes;

	//bool IsStartGame;
	bool IsPassFirst;

	void Awake(){
		GameObject sprite = GameObject.Find ("SeaBG01");
		if (sprite != null) {
			SpriteRenderer spr = sprite.GetComponent<SpriteRenderer> ();
			Distance = Mathf.Abs(spr.bounds.size.y);

			Vector2 worldpoint = Camera.main.ScreenToWorldPoint (Vector2.zero);
			CenterX = worldpoint.x + spr.bounds.size.x / 2;
		}

		Scenes = new  GameObject[3];
		TotalScenes = 12;
		for (int i = 0; i < Scenes.Length; i++) {
			//Random map
			int index = Random.Range(0, TotalScenes);
			Scenes [i] = LoadPrefab (i + 3, CenterX, Distance + i * Distance);
		}

		//IsStartGame = true;
		IsPassFirst = false;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = Camera.main.transform.position;
		if (position.y >= Scenes [2].transform.position.y) {
			if (!IsPassFirst)
				IsPassFirst = true;
			HandleNewScene (Scenes [2].transform.position.y + Distance, 2, false);
//			int index = Random.Range (0, 3);
//			GameObject obj = LoadPrefab (index, CenterX, Scenes [2].transform.position.y, false);
//			ReplaceGameObject (obj, 2);
			return;
		}

		if (position.y <= Scenes [1].transform.position.y) {
			if (IsPassFirst) {
				HandleNewScene (Scenes [0].transform.position.y - Distance, 0, true);
			}
		}
	}

	GameObject LoadPrefab(int index, float x, float y)
	{
		GameObject obj;
		switch (index) {
		case 0:
//			obj = Instantiate (Resources.Load ("SeaBG/SeaBG02", typeof(GameObject))) as GameObject;
//			obj.transform.position = new Vector3 (x, isDown ? y - Distance : y, 0);
			obj = CreateNewObject("SeaBG/SeaBG02", x, y);
			break;
		case 1:
//			obj = Instantiate (Resources.Load ("SeaBG/SeaBG03", typeof(GameObject))) as GameObject;
//			obj.transform.position = new Vector3 (x, isDown ? y - Distance : y, 0);
			obj = CreateNewObject("SeaBG/SeaBG03", x, y);
			break;
		case 2:
//			obj = Instantiate (Resources.Load ("SeaBG/SeaBG04", typeof(GameObject))) as GameObject;
//			obj.transform.position = new Vector3 (x, isDown ? y - Distance : y, 0);
//			obj = Instantiate (Resources.Load ("SeaBG/SeaBG04", typeof(GameObject))) as GameObject;
			obj = CreateNewObject("SeaBG/SeaBG04", x, y);
			break;
		case 3:
			obj = CreateNewObject("SeaBG/map med 2", x, y);
			break;
		case 4:
			obj = CreateNewObject("SeaBG/map e 3", x, y);
			break;
		case 5:
			obj = CreateNewObject("SeaBG/map med 1", x, y);
			break;
		case 6:
			obj = CreateNewObject("SeaBG/map e 2", x, y);
			break;
		case 7:
			obj = CreateNewObject("SeaBG/map color 3", x, y);
			break;
		case 8:
			obj = CreateNewObject("SeaBG/map e 1", x, y);
			break;
		case 9:
			obj = CreateNewObject("SeaBG/map h 1", x, y);
			break;
		case 10:
			obj = CreateNewObject("SeaBG/map color 1", x, y);
			break;
		case 11:
			obj = CreateNewObject("SeaBG/map color 2", x, y);
			break;
		case 12:
			obj = CreateNewObject("SeaBG/map h 2", x, y);
			break;
		case 13:
			obj = CreateNewObject("SeaBG/map h 3", x, y);
			break;
		case 14:
			obj = CreateNewObject("SeaBG/map med 3", x, y);
			break;
		default:
			obj = null;
			break;
		}
		return obj;
	}

	GameObject CreateNewObject(string resource, float x, float y)
	{
		GameObject obj;
		obj = Instantiate (Resources.Load (resource, typeof(GameObject))) as GameObject;
		obj.transform.position = new Vector3 (x, y, 0);
		return obj;
	}

	void HandleNewScene(float y, int position, bool isBack)
	{
		if (isBack) {
			int index = Random.Range (0, 3);
			GameObject obj = LoadPrefab (index, CenterX, y);
			ReplaceGameObject (obj, position);
		} else {
			int index = Random.Range (0, TotalScenes);
			GameObject obj = LoadPrefab (index + 3, CenterX, y);
			ReplaceGameObject (obj, position);
		}
	}

	void ReplaceGameObject (GameObject newobj, int index)
	{
		if (index == 2) {
			GameObject obj0 = Scenes [0];
			GameObject obj1 = Scenes [1];
			GameObject obj2 = Scenes [2];

			Scenes [0] = obj1;
			Scenes [1] = obj2;
			Scenes [2] = newobj;

			Destroy (obj0);
		}

		if (index == 0) {
			GameObject obj0 = Scenes [0];
			GameObject obj1 = Scenes [1];
			GameObject obj2 = Scenes [2];

			Scenes [0] = newobj;
			Scenes [1] = obj0;
			Scenes [2] = obj1;

			Destroy (obj2);
		}
	}

}
