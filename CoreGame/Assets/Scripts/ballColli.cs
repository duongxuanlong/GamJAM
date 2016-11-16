using UnityEngine;
using System.Collections;

public class ballColli : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D c) {
		Rigidbody2D body = transform.GetComponent<Rigidbody2D>();
		Vector3 d = body.transform.position - transform.position;
		body.AddForceAtPosition(d.normalized, transform.position);
	}
}
