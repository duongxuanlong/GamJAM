using UnityEngine;
using System.Collections;

public class screenCollider : MonoBehaviour {

	private Vector2 rtop, rbot, lbot, ltop;

	// Use this for initialization
	void Awake () {
		rtop.x = Camera.main.pixelWidth;
		rtop.y = -Camera.main.pixelHeight / 2;
		rbot.x = Camera.main.pixelWidth;
		rbot.y = Camera.main.pixelHeight;
		lbot.x = -Camera.main.pixelWidth / 2;
		lbot.y = Camera.main.pixelHeight;
		ltop.x = -Camera.main.pixelWidth / 2;
		ltop.y = -Camera.main.pixelHeight / 2;



		Vector2[] points = { rtop, rbot, lbot, ltop };
		EdgeCollider2D col = gameObject.AddComponent<EdgeCollider2D>();
		col.points = points;
	}

	// Update is called once per frame
	void Update () {
	}
}
