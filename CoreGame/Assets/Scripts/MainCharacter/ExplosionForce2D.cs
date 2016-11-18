// Developed by Ananda Gupta
// info@anandagupta.com
// http://anandagupta.com

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExplosionForce2D : MonoBehaviour
{
	public float Power;
	public float InversePower;
	public float Radius;
	public GameObject ball;
	public GameObject bum;


	private float blockrate = 0.3f;
	private bool isSeperated = true;
	private float blocktime;
	private int maxBall = 3;
	// Use this for initialization

	void Start ()
	{
		blocktime = Time.time;
		if (!Gamemanager.isStarted) {
			Time.timeScale = 0;
		}
	}

	// Update is called once per frame
	void Update()
	{
		# if (UNITY_EDITOR || UNITY_WEBGL)
		if (Input.GetButtonDown ("Fire1") && !Gamemanager.isStarted) {
			Gamemanager.isStarted = true;
			Time.timeScale = 1;
			GameObject.Find("taptostart").GetComponent<Canvas>().enabled = false;
		}

		if (Input.GetButtonDown ("Fire1")) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0;
			AddExplosionForce(GetComponent<Rigidbody2D>(), Power * 100, mousePos, Radius);
			Instantiate (bum, mousePos, Quaternion.identity);
		}
		# endif	



		# if (UNITY_ANDROID || UNITY_IPHONE)
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began && !Gamemanager.isStarted) {
			Gamemanager.isStarted = true;
			Time.timeScale = 1;
			GameObject.Find("taptostart").GetComponent<Canvas>().enabled = false;
		}

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0;
			AddExplosionForce(GetComponent<Rigidbody2D>(), Power * 100, mousePos, Radius);
			Instantiate (bum, mousePos, Quaternion.identity);
		}
		# endif	

		//		# if (UNITY_ANDROID || UNITY_IPHONE)
		//
		//		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
		//			Vector3 fingerPos = Input.GetTouch(0).position;
		//			fingerPos.z = 10;
		//			Vector3 objPos = Camera.main.ScreenToWorldPoint(fingerPos);
		//			AddExplosionForce(GetComponent<Rigidbody2D>(), Power * 100, objPos, Radius);
		//		}
		//
		//		# endif
		//
		//		# if (UNITY_EDITOR || #IF UNITY_WEBGL)
		//
		//		if (Input.GetButtonDown("Fire1")){
		//			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//			mousePos.z = 0;
		//			AddExplosionForce(GetComponent<Rigidbody2D>(), Power * 100, mousePos, Radius);
		//		}
		//		# endif	

		if ((Time.time - blocktime) * blockrate >= 0.5f) {
			isSeperated = false;
			blocktime = Time.time;
		}
	}

	public static void AddExplosionForce (Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius)
	{
		var dir = (body.transform.position - expPosition);
		float calc = 1 - (dir.magnitude / expRadius);
		if (calc <= 0) {
			calc = 0;		
		}

		body.AddForce (dir.normalized * expForce * calc);
	}

	void OnGUI()
	{
		//		if (GUI.Button(new Rect(10, 10, 100, 50), "Silde"))
		//		{
		//			SceneManager.LoadScene("slide");
		//		}
		//		if (GUI.Button(new Rect(300, 10, 100, 50), "Bounce"))
		//		{
		//			SceneManager.LoadScene("bounce");
		//		}
		//		if (GUI.Button(new Rect(300, 10, 100, 50), "Bounce"))
		//		{
		//			Time.timeScale = 0;
		//		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "BottomCollider") {
			if (GameObject.FindGameObjectsWithTag ("Player").Length > 1) {
				Destroy (gameObject);
			} else {
				Gamemanager.isStarted = false;
				SceneManager.LoadScene ("General");
			}
		}

		if (other.gameObject.tag == "SceneObstacles") {
			Destroy (other.gameObject);
			Gamemanager.SceneChange = true;
		}

		if (other.gameObject.tag == "SceneBars") {
			if (Gamemanager.SceneChange) {
				Gamemanager.isStarted = false;
				Gamemanager.SceneChange = false;
				SceneManager.LoadScene ("General");
			}

		}
			
		//other.gameObject.tag == "EdgeCollider"

		if (other.gameObject.tag == "Obstacles") {
			if (!isSeperated && GameObject.FindGameObjectsWithTag ("Player").Length < maxBall) {
				seperateBall ();
			}
		}
	}

	private void seperateBall() {
		isSeperated = true;
		Instantiate (ball, transform.position, Quaternion.identity);
		blocktime = Time.time + (1.0f/blockrate);
	}

}
