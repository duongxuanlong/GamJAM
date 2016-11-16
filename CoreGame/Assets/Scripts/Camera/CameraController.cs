using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float m_Speed;
	void Awake()
	{
		float height = Camera.main.orthographicSize * 2;
		float width = height * (Screen.width / Screen.height) * 2;
		//float width = Screen.width;
		transform.localScale = new Vector3 (width, height, 0f);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (transform.position.x, m_Speed * Time.deltaTime, 0f);
	}
}
