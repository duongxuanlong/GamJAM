using UnityEngine;
using System.Collections.Generic;

public class GenerateColliders : MonoBehaviour {

	public float widthOfCollider = 2f;
	public float z_axis = 0f;
	private Vector2 screenSize;

	Dictionary<string,Transform> colliders;

	void Start ()
	{
		colliders = new Dictionary<string,Transform>();
		colliders.Add("Bottom",new GameObject().transform);
//		colliders.Add("Right",new GameObject().transform);
//		colliders.Add("Left",new GameObject().transform);
		//colliders.Add("Top",new GameObject().transform);

		Vector3 cameraPos = Camera.main.transform.position;
		screenSize.x = Vector2.Distance (Camera.main.ScreenToWorldPoint(new Vector2(0,0)),Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
		screenSize.y = Vector2.Distance (Camera.main.ScreenToWorldPoint(new Vector2(0,0)),Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

		foreach(KeyValuePair<string,Transform> bc in colliders)
		{
			bc.Value.gameObject.AddComponent<BoxCollider2D>();
			bc.Value.name = bc.Key + "Collider";
			bc.Value.parent = transform;
			if (bc.Key == "Left" || bc.Key == "Right")
				bc.Value.localScale = new Vector2 (1, screenSize.y * 2);// widthOfCollider);
			else
				bc.Value.localScale = new Vector2 (screenSize.x * 2, 1);// widthOfCollider);

			if (bc.Key == "Left") {
				bc.Value.gameObject.tag = "LeftCollider";
				bc.Value.gameObject.layer = LayerMask.NameToLayer ("LeftCollider");
			}
			if (bc.Key == "Right") {
				bc.Value.gameObject.tag = "RightCollider";
				bc.Value.gameObject.layer = LayerMask.NameToLayer ("RightCollider");
			}
			if (bc.Key == "Bottom") {
				bc.Value.gameObject.tag = "BottomCollider";
				bc.Value.gameObject.layer = LayerMask.NameToLayer ("BottomCollider");
			}
//			if (bc.Key == "Top")
//				bc.Value.gameObject.tag = "TopCollider";
//			if (bc.Key == "Top")
//				bc.Value.gameObject.layer = LayerMask.NameToLayer ("SpecialEdgeCollider");
//			else
				//bc.Value.gameObject.layer = LayerMask.NameToLayer ("EdgeCollider");
//			if (bc.Key == "Left" || bc.Key == "Right")
//				bc.Value.gameObject.tag = "EdgeCollider";
//			else
//				bc.Value.gameObject.tag = "BottomCollider";
		}

//		colliders["Right"].position = new Vector3(cameraPos.x + screenSize.x + (colliders["Right"].localScale.x * 0.5f), cameraPos.y, 0);
//		colliders["Left"].position = new Vector3(cameraPos.x - screenSize.x - (colliders["Left"].localScale.x * 0.5f), cameraPos.y, 0);
		//colliders["Top"].position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (colliders["Top"].localScale.y * 0.5f));
		colliders["Bottom"].position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (colliders["Bottom"].localScale.y * 0.5f), 0);

	}

	void Update()
	{
		Vector3 cameraPos = Camera.main.transform.position;
		screenSize.x = Vector2.Distance (Camera.main.ScreenToWorldPoint(new Vector2(0,0)),Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
		screenSize.y = Vector2.Distance (Camera.main.ScreenToWorldPoint(new Vector2(0,0)),Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

//		colliders["Right"].position = new Vector3(cameraPos.x + screenSize.x + (colliders["Right"].localScale.x * 0.5f), cameraPos.y, 0);
//		colliders["Left"].position = new Vector3(cameraPos.x - screenSize.x - (colliders["Left"].localScale.x * 0.5f), cameraPos.y, 0);
		//colliders["Top"].position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (colliders["Top"].localScale.y * 0.5f));
		colliders["Bottom"].position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (colliders["Bottom"].localScale.y * 0.5f), 0);
	}
}