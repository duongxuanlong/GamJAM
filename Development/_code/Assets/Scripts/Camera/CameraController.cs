using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

	//public float m_Speed;
	//void Awake()
	//{
	//	float height = Camera.main.orthographicSize * 2;
	//	float width = height * (Screen.width / Screen.height) * 2;
	//	transform.localScale = new Vector3 (width, height, 0f);
	//	InvokeRepeating("addSpeed", 1f, 6f);
	//}
	
	//void Update () {
	//	transform.Translate (transform.position.x, m_Speed * Time.deltaTime, 0f);
	//}

	//private void addSpeed()
	//{
	//	m_Speed += 0.1f;
	//}


	GameObject player;
	public Text txt;
	public Text score;
	public Text end;

	private float minY = 0;

	void Start() {
		Gamemanager.score = 0;
		Gamemanager.time = 30;
		minY = transform.position.y;
		InvokeRepeating("timecountdown", 0f, 1f);
	}

	void FixedUpdate()
	{
		float maxY = -7;

		foreach (GameObject o in GameObject.FindGameObjectsWithTag("Player")) {
			if (maxY < o.gameObject.transform.position.y) {
				maxY = o.gameObject.transform.position.y;
				player = o;
			}
		}

		txt.text = Gamemanager.time.ToString();
		score.text = Gamemanager.score.ToString();

		if (Gamemanager.time < 1) { 
			SceneManager.LoadScene("SubmitScene");
			Gamemanager.time = 30;
			Gamemanager.score = 0;
			Gamemanager.isStarted = false;
			Gamemanager.SceneChange = false;
		}

		if (Gamemanager.isEnd) {
			foreach (GameObject o in GameObject.FindGameObjectsWithTag("Player")) {
				Destroy(o);
			}
			end.text = "Your Score " + Gamemanager.score + " MABU";
			Gamemanager.isStarted = false;
			Gamemanager.isEnd = false;
			Time.timeScale = 0;
		}

	}

	void LateUpdate()
	{
		if (player != null)
		{
			if (player.transform.position.y > minY)
			{
				Vector3 a = new Vector3(transform.position.x, player.transform.position.y);
				transform.position = a;
			}
			else { 
				
			}
				
		}
	}

	void timecountdown() {
		Gamemanager.time--;
	}
}
