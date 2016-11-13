using UnityEngine;
using System.Collections;

public class Offset : MonoBehaviour {

	public float m_Speed;

	public void Awake()
	{
		float height = Camera.main.orthographicSize * 2;
		float width = height * (Screen.width / Screen.height) * 2;
		//float width = Screen.width;
		transform.localScale = new Vector3 (width, height, 0.1f);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float next = Mathf.Repeat (Time.time * m_Speed, 1f);
		GetComponent<Renderer> ().material.mainTextureOffset = Vector2.right * next;
	}
}
