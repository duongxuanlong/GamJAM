using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BallManager : MonoBehaviour {
	public GameObject Prefab;
	private int MaximumObjects;

	// Use this for initialization
	void Start () {
		MaximumObjects = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DestroyBall (GameObject obj, bool unhestiate)
	{
		if (!unhestiate) {
			if (transform.childCount > 0)
				Destroy (obj);
		} else {
			Destroy (obj);
			if (transform.childCount == 0)
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}

	public void CreateNewBall (Transform trans)
	{
		if (transform.childCount < MaximumObjects)
			Instantiate (Prefab, trans.localPosition, Quaternion.identity);
	}
}
