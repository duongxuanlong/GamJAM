using UnityEngine;
using System.Collections;

public class tap : MonoBehaviour {

	private Vector3 mousePos;
	private bool isMousePressed = false;

	// Use this for initialization
	void Start () {
	
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
					mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					mousePos.z = 0;
					transform.position = mousePos;
					break;

				// Determine direction by comparing the current touch position with the initial one.
				case TouchPhase.Moved:
					break;

				// Report that a direction has been chosen when the finger is lifted.
				case TouchPhase.Ended:
					break;
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			isMousePressed = true;
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0;
			transform.position = mousePos;
		}
		if (Input.GetMouseButtonUp(0))
		{
			isMousePressed = false;
		}
	}
}
