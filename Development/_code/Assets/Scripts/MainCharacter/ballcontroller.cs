using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ballcontroller : MonoBehaviour
{
	public float Power;
	public float InversePower;
	public float Radius;

	public GameObject ball;
	public GameObject bum;
	public AudioClip tap;
	public AudioClip tach;
	public AudioClip hop;
	public AudioClip die;
	public AudioClip bounce;
	public AudioClip coin;

	public int Level;
	private int MaxLevel;

	public bool IsDestroy;

	private float blockrate = 0.6f;
	private bool isSeperated = true;
	private float blocktime;
	private Animator anim;
	private AudioSource source;

	//private static int level = 0;
	void Awake()
	{
		Level = 1;
		MaxLevel = 3;
	}

	void Start ()
	{
//		Level = 1;
//		MaxLevel = 4;

		IsDestroy = false;
		Gamemanager.isEnd = false;
		anim = GetComponent<Animator> ();
		source = GetComponent<AudioSource>();
		GetComponent<Rigidbody2D>().isKinematic = false;
		blocktime = Time.time;
		if (!Gamemanager.isStarted) {
			Time.timeScale = 0;
			anim.SetBool("isHit", false);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		#if (UNITY_EDITOR || UNITY_WEBGL)
		if (Input.GetButtonDown ("Fire1") && !Gamemanager.isStarted) {
			GetComponent<Rigidbody2D>().isKinematic = false;
			Gamemanager.isStarted = true;
			Time.timeScale = 1;
			GameObject.Find("taptostart").GetComponent<Text>().enabled = false;

		}

		if (Input.GetButtonDown ("Fire1")) {

			Vector2 worldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (worldPoint, Vector2.zero);
			if (hit.collider == null) {
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				mousePos.z = 0;
				AddExplosionForce (GetComponent<Rigidbody2D> (), Power * 100, mousePos, Radius);
				Instantiate (bum, mousePos, Quaternion.identity);
				source.PlayOneShot(tap, 0.8f);
				anim.SetBool("isHit", true);
			
			} else if (hit.collider.tag == "Player") {
				hit.collider.GetComponent<Rigidbody2D> ().AddForce (Vector3.up * Power * 30);
				source.PlayOneShot(tap, 0.8f);
				anim.SetBool ("isHit", true);
			}
		}
		# endif	



		# if (UNITY_ANDROID || UNITY_IPHONE)
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began && !Gamemanager.isStarted) {
			GetComponent<Rigidbody2D>().isKinematic = false;
			Gamemanager.isStarted = true;
			Time.timeScale = 1;
			GameObject.Find("taptostart").GetComponent<Text>().enabled = false;
		}

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			//Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
			RaycastHit2D hit = Physics2D.Raycast (worldPoint, Vector2.zero);
			if (hit.collider == null) {
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				mousePos.z = 0;
				AddExplosionForce (GetComponent<Rigidbody2D> (), Power * 100, mousePos, Radius);
				Instantiate (bum, mousePos, Quaternion.identity);
				source.PlayOneShot(tap, 0.8f);
				anim.SetBool("isHit", true);

			} else if (hit.collider.tag == "Player") {
				GetComponent<Rigidbody2D> ().AddForce (Vector3.up.normalized * Power * 30 * hit.transform.localScale.x);
				source.PlayOneShot(tap, 0.8f);
				anim.SetBool ("isHit", true);
			}
		}
		# endif	

		if ((Time.time - blocktime) * blockrate >= 0.5f) {
			isSeperated = false;
			blocktime = Time.time;
			anim.SetBool("isHit", false);
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

	void OnGUI ()
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

	void OnCollisionEnter2D (Collision2D other)
	{
		switch (other.gameObject.tag) {

		case "BottomCollider":
			Destroy (gameObject);
			if (GameObject.FindGameObjectsWithTag ("Player").Length == 1) {
				Gamemanager.isStarted = false;
					GameObject.FindGameObjectWithTag("UI").GetComponent<Canvas>().enabled = false;
					GameObject.FindGameObjectWithTag("End").GetComponent<Canvas>().enabled = true;
					Gamemanager.isEnd = true;
				//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
			break;

		case "Obstacles":
		case "DynamicCollider":
			gameObject.GetComponent<Rigidbody2D> ().velocity -= gameObject.GetComponent<Rigidbody2D> ().velocity / 5;

				if (!isSeperated)
				{
					if (Level < MaxLevel)
					{
						Level++;
						gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - 0.15f, gameObject.transform.localScale.y - 0.15f, 0);
						seperateBall();
						StartCoroutine(tangLevel());
					}
					else if (Level == MaxLevel)
					{
						GetComponent<Rigidbody2D>().isKinematic = true;
						//anim.Play("die");
						source.PlayOneShot(die);
						if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
						{
							Gamemanager.isStarted = false;
							GameObject.FindGameObjectWithTag("UI").GetComponent<Canvas>().enabled = false;
							GameObject.FindGameObjectWithTag("End").GetComponent<Canvas>().enabled = true;
							Gamemanager.isEnd = true;
						}
					}
				}
			break;
		case "Player":
			ballcontroller controller = other.gameObject.GetComponent<ballcontroller> ();
			//Debug.Log ("Level before" + Level);
			if (controller != null && !isSeperated) {
				if (Level == controller.Level)
				if (!this.IsDestroy && !controller.IsDestroy) {
							//Debug.Log ("Enter collision here");
							//source.PlayOneShot(hop);
					Vector3 center = transform.position;// + other.transform.position / 2;
					GameObject obj = Instantiate (ball, center, Quaternion.identity) as GameObject;
					ballcontroller newcontroller = obj.GetComponent<ballcontroller> ();
							newcontroller.GetComponent<AudioSource>().PlayOneShot(hop);
					//Debug.Log ("Level inside" + Level);
					obj.transform.localScale = new Vector3(transform.localScale.x + 0.15f, transform.localScale.y + 0.15f, 0f);
					newcontroller.Level = Level - 1;
					
					this.IsDestroy = true;
					controller.IsDestroy = true;
					Destroy (this.gameObject);
					Destroy (other.gameObject);
				}
			} else
				gameObject.GetComponent<Rigidbody2D> ().velocity -= gameObject.GetComponent<Rigidbody2D> ().velocity / 5;			
			break;

		case "SceneBars":
		case "LeftCollider":
		case "RightCollider":
		case "StaticCollider":
			gameObject.GetComponent<Rigidbody2D>().velocity -= gameObject.GetComponent<Rigidbody2D>().velocity / 5;
			source.PlayOneShot(bounce);
		break;
		}
	}

	private void seperateBall ()
	{
		isSeperated = true;
		source.PlayOneShot(tach);
		GameObject obj = Instantiate (ball, transform.position, Quaternion.identity) as GameObject;
		ballcontroller con = obj.GetComponent<ballcontroller> ();
		obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 2), Random.Range(0, 2)));
		//AddExplosionForce(obj.GetComponent<Rigidbody2D>(), Power * 30, transform.position, 3);
		con.Level = Level;
		obj.transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, 0);
		blocktime = Time.time + (1.0f / blockrate);
	}


	private void endHit ()
	{
		anim.SetBool ("isHit", false);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "SceneObstacles") { 
			GameObject[] a = GameObject.FindGameObjectsWithTag ("SceneBars");
			if (a.Length > 0) {
				GameObject nearest = a [0];
				foreach (GameObject g in a) {
					if (g.gameObject.transform.position.y < nearest.gameObject.transform.position.y) {
						nearest = g;
					}
				}
				nearest.gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;
				GameObject.FindGameObjectWithTag ("SceneBars").gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;
			}
		}

		if (other.gameObject.tag == "Coin")
		{
			Gamemanager.score += 1;
			Gamemanager.time += 5;
			source.PlayOneShot(coin);
		}
		Destroy (other.gameObject);
	}

	private IEnumerator tangLevel() { 
		yield return new WaitForSeconds(0.2f);
	}

	void OnBecameInvisible()
	{
		enabled = false;
		Destroy (gameObject);
	}

	void mcdie() {
		Destroy(gameObject);
	}


}
