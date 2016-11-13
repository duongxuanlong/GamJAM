using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {
	public float m_Speed;
	public float m_Size;

	public Vector2 m_StartPosition;
	// Use this for initialization
	void Start () {
		m_StartPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float next = Mathf.Repeat (Time.time * m_Speed, m_Size);
		Debug.Log ("Time.time: " + Time.time);
		Debug.Log ("next: " + next);
		transform.position = m_StartPosition + Vector2.left * next;
	}
}
