using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class draw : MonoBehaviour {

	private LineRenderer line;
	private EdgeCollider2D col;
	private bool isMousePressed;
	public List<Vector3> pointsList;
	private Vector3 mousePos;
	private Vector2[] coliPoints;
	public Text myText;

	void Awake()
	{
		// Create line renderer component and set its property
		line = gameObject.AddComponent<LineRenderer>();
		col = new GameObject("Collider").AddComponent<EdgeCollider2D>();
		col.enabled = false;
		col.transform.parent = line.transform;
		//line.material = new Material(Shader.Find("Particles/Additive"));
		line.SetVertexCount(0);
		line.SetWidth(0.1f, 0.1f);
		line.SetColors(Color.green, Color.green);
		line.useWorldSpace = true;
		pointsList = new List<Vector3>();
	}


	void OnGUI() {
		if (GUI.Button(new Rect(10, 10, 100, 50), "Silde"))
		{
			SceneManager.LoadScene("slide");
		}
		if (GUI.Button(new Rect(300, 10, 100, 50), "Bounce"))
		{
			SceneManager.LoadScene("bounce");
		}
	}

	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			// Handle finger movements based on touch phase.
			switch (touch.phase)
			{
				// Record initial touch position.
				case TouchPhase.Began:
					line.SetVertexCount(0);
					pointsList.RemoveRange(0, pointsList.Count);
					coliPoints = null;
					line.SetColors(Color.green, Color.green);
					col.enabled = true;
					break;

				// Determine direction by comparing the current touch position with the initial one.
				case TouchPhase.Moved:
					mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					mousePos.z = 0;
					if (!pointsList.Contains(mousePos))
					{
						pointsList.Add(mousePos);
						line.SetVertexCount(pointsList.Count);
						line.SetPosition(pointsList.Count - 1, (Vector3)pointsList[pointsList.Count - 1]);
						coliPoints = pointsList.ToArray().toVector2Array();
						col.points = coliPoints;
					}
					break;

				// Report that a direction has been chosen when the finger is lifted.
				case TouchPhase.Ended:
					//isMousePressed = false;
					break;
			}
		}


		if (Input.GetMouseButtonDown(0))
		{
			isMousePressed = true;
			line.SetVertexCount(0);
			pointsList.RemoveRange(0, pointsList.Count);
			coliPoints = null;
			line.SetColors(Color.green, Color.green);
			col.enabled = true;
		}
		if (Input.GetMouseButtonUp(0))
		{
			isMousePressed = false;
		}
		// Drawing line when mouse is moving(presses)
		if (isMousePressed)
		{
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0;
			if (!pointsList.Contains(mousePos))
			{
				pointsList.Add(mousePos);
				line.SetVertexCount(pointsList.Count);
				line.SetPosition(pointsList.Count - 1, (Vector3)pointsList[pointsList.Count - 1]);
				coliPoints = pointsList.ToArray().toVector2Array();
				col.points = coliPoints;
				print(col);
			}
		}
	}
}

public static class MyVector3Extension
{
	public static Vector2[] toVector2Array(this Vector3[] v3)
	{
		return System.Array.ConvertAll<Vector3, Vector2>(v3, getV3fromV2);
	}

	public static Vector2 getV3fromV2(Vector3 v3)
	{
		return new Vector2(v3.x, v3.y);
	}
}
