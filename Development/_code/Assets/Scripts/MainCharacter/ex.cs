using UnityEngine;
using System.Collections;

public class ex : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.Play("ex");
		StartCoroutine(destroy());
	}

	private IEnumerator destroy()
	{
		yield return new WaitForSeconds(0.2f);
		Destroy(gameObject);
	}
}
