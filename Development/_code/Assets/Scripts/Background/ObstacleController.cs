using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {
	Vector2 LeftContraint;
	Vector2 RightContraint;

	public float Speed;
	int direction;
	bool IsRunning;

	float Factor;
	// Use this for initialization
	void Start () {
		LeftContraint = Camera.main.ScreenToWorldPoint (new Vector3 (0f, 0f, 0f));
		RightContraint = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0f, 0f));

		float deltaleft = LeftContraint.x - transform.position.x;
		float deltaright = RightContraint.x - transform.position.x;
		if (Mathf.Abs (deltaleft) >= Mathf.Abs (deltaright))
			direction = -1;
		else {
			Flip ();
			direction = 1;
		}
		//Speed = 100;
		IsRunning = false;
		Factor = 0.001f;
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		if (IsRunning) {
			float distance = Time.fixedDeltaTime * Speed * direction;
			GetComponent<Rigidbody2D> ().velocity += new Vector2 (distance, 0);
		}
			
//		if (Mathf.Abs (distance) > MaxX) {
//			distance = 0;
//			direction *= -1;
//		}
		//transform.Translate(new Vector3(transform.position.x + distance, transform.position.y, 0));
//		if (direction < 0)
//			if (distance + transform.position.x < LeftContraint.x) {
//				distance = LeftContraint.x;
//				direction = 1;
//			} else
//				distance += transform.position.x;
//		else if (distance + transform.position.x > RightContraint.x) {
//				distance = RightContraint.x;
//				direction = -1;
//		} else
//			distance += transform.position.x;
//		transform.Translate(new Vector3(distance, transform.position.y, 0));
			
	}

	void OnCollisionEnter2D(Collision2D collider)
	{
		//Debug.Log ("Collider: " + collider.gameObject.tag);
		switch (collider.gameObject.tag) {
		case "LeftCollider":
			direction *= -1;
			Flip ();
			break;
		case "RightCollider":
			direction *= -1;
			Flip ();
			break;
		case "Player":
			float value = collider.relativeVelocity.magnitude;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
			direction *= -1;
			Flip ();
			break;
				
			default:
				direction *= -1;
				Flip();
				break;
				
			
		}
	}
		
	void Flip()
	{
		Vector3 localscale = transform.localScale;
		localscale.x *= -1;
		transform.localScale = localscale;
	}

	void OnBecameVisible()
	{
		IsRunning = true;
		//Debug.Log ("OnBecameVisible");
		//EnableEdgeCollision ();
	}

	void OnBecameInvisible()
	{
		IsRunning = false;
		//EnableEdgeCollision ();
	}

	void EnableEdgeCollision()
	{
		
	}
}
