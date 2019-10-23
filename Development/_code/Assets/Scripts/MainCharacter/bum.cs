using UnityEngine;
using System.Collections;

public class bum : MonoBehaviour {

	void Start () {
		StartCoroutine(destroy());
	}
	
	private IEnumerator destroy() {
			yield return new WaitForSeconds(0.5f);
			Destroy(gameObject);
	} 
}
