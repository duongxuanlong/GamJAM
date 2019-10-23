using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGController : MonoBehaviour {
	//Mode
	public static int ModeEasy = 0;
	public static int ModeMedium = 1;
	public static int ModeHard = 2;

	public static int NumberEasy = 4;
	public static int NumberMedium = 4;
	public static int NumberHard = 2;

	public bool IsGenerated = false;

	private float StartX;
	private SpriteRenderer Renderer;
	private float Factor;

	List<GameObject> MyObjects;

	void OnDestroy() {
		foreach (GameObject obj in MyObjects)
		{
			Destroy(obj);
		}
	}

	void Start ()
	{
		Renderer = GetComponent<SpriteRenderer> ();

		Vector2 worldpoint = Camera.main.ScreenToWorldPoint (Vector2.zero);
		StartX = worldpoint.x;
		//CenterX = worldpoint.x + renderer.bounds.size.x / 2;

		Factor = 0f;

		MyObjects = new List<GameObject> ();

		GenerateObstacles ();
//		if (IsGenerated) {
//			GenerateObstacles ();
//		}
	}

	public void GenerateObstacles()
	{
		int numberstate = Random.Range (0,
			BGController.NumberEasy + BGController.NumberMedium + BGController.NumberHard);
		if (numberstate < BGController.NumberEasy) {
			int index = Random.Range (0, BGController.NumberEasy);
			GenerateEasyObstacles (index);
		} else if (numberstate < (BGController.NumberEasy + BGController.NumberMedium)) {
			int index = Random.Range (0, BGController.NumberMedium);
			GenerateMediumObstacles (index);
		} else {
			int index = Random.Range (0, BGController.NumberHard);
			GenerateHardObstacles (index);
		}
	}

	void GenerateEasyObstacles(int index)
	{
		switch (index) {
		case 0:
			GameObject leaf01, leaf02, leaf03;
			GameObject soc01;
			float x0, y0;
	
			//Create Obstacles
			x0 = StartX + Renderer.bounds.size.x * 2 / 3;
			y0 = transform.position.y + Renderer.bounds.size.y + Factor;
			leaf01 = CreateRandomLeaf (x0, y0);
			MyObjects.Add (leaf01);

			x0 = StartX;
			y0 = transform.position.y + Renderer.bounds.size.y * 2 / 5;// + Factor;sta
			leaf02 = CreateRandomLeaf (x0, y0);
			MyObjects.Add (leaf02);

			x0 = StartX;
			y0 = transform.position.y + Renderer.bounds.size.y * 4 / 5;// + Factor;
			leaf03 = CreateRandomLeaf (x0, y0);
			MyObjects.Add (leaf03);

			x0 = StartX + Renderer.bounds.size.x / 3;
			y0 = transform.position.y + Renderer.bounds.size.y * 2 / 5;// + Factor;
			soc01 = CreateSoc (x0, y0);
			MyObjects.Add (soc01);

			//Generate Coins
			x0 = StartX + Renderer.bounds.size.x / 3;
			y0 = transform.position.y + Factor;
			GameObject coin01 = CreateRandomCoin (x0, y0);
			if (coin01 != null)
				MyObjects.Add (coin01);

			x0 = StartX + Renderer.bounds.size.x / 3;
			y0 = transform.position.y + Renderer.bounds.size.y / 5 + Factor;
			GameObject coin02 = CreateRandomCoin (x0, y0);
			if (coin02 != null)
				MyObjects.Add (coin02);

			x0 = StartX + Renderer.bounds.size.x / 3;
			y0 = transform.position.y + Renderer.bounds.size.y * 2 / 5 + Factor;
			GameObject coin03 = CreateRandomCoin (x0, y0);
			if (coin03 != null) {
				MyObjects.Add (coin03);
			}
			 
			x0 = StartX + Factor;
			y0 = transform.position.y + Renderer.bounds.size.y * 4 / 5 + Factor;
			GameObject coin04 = CreateRandomCoin (x0, y0);
			if (coin04 != null)
				MyObjects.Add (coin04);

			x0 = StartX +  Renderer.bounds.size.x / 3 + Factor;
			y0 = transform.position.y + Renderer.bounds.size.y * 4 / 5 + Factor;
			GameObject coin05 = CreateRandomCoin (x0, y0);
			if (coin05 != null)
				MyObjects.Add (coin05);
			break;
		case 1:
			//Generate Obstacles
			GameObject leaf11, leaf12;
			GameObject soc11, soc12;
			float x1, y1;

			x1 = StartX;
			y1 = transform.position.y + Renderer.bounds.size.y * 2 / 5;
			leaf11 = CreateRandomLeaf (x1, y1);
			MyObjects.Add (leaf11);

			x1 = StartX + Renderer.bounds.size.x * 2 / 3;
			y1 = transform.position.y + Renderer.bounds.size.y * 2 / 5;
			leaf12 = CreateRandomLeaf (x1, y1);
			MyObjects.Add (leaf12);

			x1 = StartX;
			y1 = transform.position.y + Renderer.bounds.size.y * 2 / 5 + Factor;
			soc11 = CreateSoc (x1, y1);
			MyObjects.Add (soc11);

			x1 = StartX + Renderer.bounds.size.x * 2 / 3;
			y1 = transform.position.y + Renderer.bounds.size.y * 4 / 5;
			soc12 = CreateSoc (x1, y1);
			MyObjects.Add (soc12);

			x1 = StartX;
			y1 = transform.position.y;
			GameObject coin11 = CreateRandomCoin (x1, y1);
			if (coin11 != null)
				MyObjects.Add (coin11);

			x1 = StartX + Renderer.bounds.size.x * 2 / 3;
			y1 = transform.position.y + Renderer.bounds.size.y * 2 / 5 + Factor;
			GameObject coin12 = CreateRandomCoin (x1, y1);
			if (coin12 != null)
				MyObjects.Add (coin12);

			x1 = StartX + Renderer.bounds.size.x * 2 / 3;
			y1 = transform.position.y + Renderer.bounds.size.y * 3 / 5 + Factor;
			GameObject coin13 = CreateRandomCoin (x1, y1);
			if (coin13 != null)
				MyObjects.Add (coin13);
			break;
		case 2:
			GameObject leaf21;
			GameObject soc21;
			GameObject chim21;
			float x2, y2;

			x2 = StartX + Renderer.bounds.size.x / 3;
			y2 = transform.position.y + Renderer.bounds.size.y / 5;
			chim21 = CreateChim (x2, y2);
			MyObjects.Add (chim21);

			x2 = StartX + Renderer.bounds.size.x / 3;
			y2 = transform.position.y + Renderer.bounds.size.y * 3 / 5;
			soc21 = CreateSoc (x2, y2);
			MyObjects.Add (soc21);

			x2 = StartX + Renderer.bounds.size.x * 2 / 3;
			y2 = transform.position.y + Renderer.bounds.size.y * 4 / 5;
			leaf21 = CreateRandomLeaf (x2, y2);
			MyObjects.Add (leaf21);

			y2 = transform.position.y + Renderer.bounds.size.y * 2 / 5;
			x2 = StartX;
			GameObject coin21 = CreateRandomCoin (x2, y2);
			if (coin21 != null)
				MyObjects.Add (coin21);

			x2 = StartX + Renderer.bounds.size.x / 3;
			GameObject coin22 = CreateRandomCoin (x2, y2);
			if (coin22 != null) {
				MyObjects.Add (coin22);
			}

			x2 = StartX + Renderer.bounds.size.x * 2 / 3;
			GameObject coin23 = CreateRandomCoin (x2, y2);
			if (coin23 != null) {
				MyObjects.Add (coin23);
			}
			break;
		case 3:
			GameObject chim31, chim32;
			GameObject soc31;
			float x3, y3;

			x3 = StartX + Renderer.bounds.size.x / 3;
			y3 = transform.position.y + Renderer.bounds.size.y / 5;
			chim31 = CreateChim (x3, y3);
			MyObjects.Add (chim31);

			y3 = transform.position.y + Renderer.bounds.size.y * 3 / 5;
			chim32 = CreateChim (x3, y3);
			MyObjects.Add (chim32);

			x3 = StartX;
			y3 = transform.position.y + Renderer.bounds.size.y * 4 / 5;
			soc31 = CreateSoc (x3, y3);
			MyObjects.Add (soc31);

			x3 = StartX + Renderer.bounds.size.x / 3;
			y3 = transform.position.y + Renderer.bounds.size.y * 2 / 5;
			GameObject coin31 = CreateRandomCoin (x3, y3);
			if (coin31 != null)
				MyObjects.Add (coin31);

			x3 = StartX;
			y3 = transform.position.y + Renderer.bounds.size.y * 3 / 5 + Factor;
			GameObject coin32 = CreateRandomCoin (x3, y3);
			if (coin32 != null)
				MyObjects.Add (coin32);
			break;
		default:
			break;
		}
	}

	void GenerateMediumObstacles(int index)
	{
		float x, y;
		switch (index) {
		case 0:
			GameObject leaf01, soc01, chim01, chim02;
			x = StartX;
			y = transform.position.y;
			leaf01 = CreateRandomLeaf (x, y);
			MyObjects.Add (leaf01);

			y = transform.position.y + Factor;
			soc01 = CreateSoc (x, y);
			MyObjects.Add (soc01);

			x = StartX + transform.position.x / 3;
			y = transform.position.y + Renderer.bounds.size.y * 2 / 5;
			chim01 = CreateChim (x, y);
			MyObjects.Add (chim01);

			y = transform.position.y + Renderer.bounds.size.y * 4 / 5;
			chim02 = CreateChim (x, y);
			MyObjects.Add (chim02);

			y = transform.position.y + Renderer.bounds.size.y / 5;
			GameObject coin01 = CreateRandomCoin (x, y);
			if (coin01 != null)
				MyObjects.Add (coin01);
			break;
		case 1:
			GameObject leaf11, leaf12, leaf13, leaf15, soc11;
			x = StartX + Renderer.bounds.size.x / 3;
			y = transform.position.y;
			leaf11 = CreateRandomLeaf (x, y);
			MyObjects.Add (leaf11);

			x = StartX + Renderer.bounds.size.x * 2 / 3;
			leaf12 = CreateRandomLeaf (x, y);
			MyObjects.Add (leaf12);

			x = StartX;
			y = transform.position.y + Renderer.bounds.size.y * 3 / 5;
			leaf13 = CreateRandomLeaf (x, y);
			MyObjects.Add (leaf13);

			x = StartX + Renderer.bounds.size.x / 3;
			leaf15 = CreateRandomLeaf (x, y);
			MyObjects.Add (leaf15);

			x = StartX;
			y = transform.position.y + Renderer.bounds.size.y * 3 / 5 + Factor;
			soc11 = CreateSoc (x, y);
			MyObjects.Add (soc11);

			x = StartX + Renderer.bounds.size.x / 3;
			y = transform.position.y + Renderer.bounds.size.y / 5 + Factor;
			GameObject coin11 = CreateRandomCoin (x, y);
			if (coin11 != null)
				MyObjects.Add (coin11);

			x = StartX + Renderer.bounds.size.x * 2 / 3;
			GameObject coin12 = CreateRandomCoin (x, y);
			if (coin12 != null)
				MyObjects.Add (coin12);

			x = StartX + Renderer.bounds.size.x / 3;
			y = transform.position.y + Renderer.bounds.size.y * 3 / 5 + Factor;
			GameObject coin13 = CreateRandomCoin (x, y);
			if (coin13 != null)
				MyObjects.Add (coin13);
			//x = StartX + Renderer.bounds.size.x
			break;
		case 2:
			GameObject leaf21, leaf22, leaf23, soc21, chim21;
			x = StartX + Renderer.bounds.size.x * 2 / 3;
			y = transform.position.y;
			leaf21 = CreateRandomLeaf (x, y);
			MyObjects.Add (leaf21);

			x = StartX;
			y = transform.position.y + Renderer.bounds.size.y * 2 / 5;
			leaf22 = CreateRandomLeaf (x, y);
			MyObjects.Add (leaf22);

			x = StartX + Renderer.bounds.size.x / 3;
			leaf23 = CreateRandomLeaf (x, y);
			MyObjects.Add (leaf23);

			x = StartX;
			y += Factor;
			soc21 = CreateSoc (x, y);
			MyObjects.Add (soc21);

			x = StartX + Renderer.bounds.size.x * 2 / 3;
			y = transform.position.y + Renderer.bounds.size.y * 4 / 5;
			chim21 = CreateChim (x, y);
			MyObjects.Add (chim21);

			y = transform.position.y + Factor;
			GameObject coin21 = CreateRandomCoin (x, y);
			if (coin21 != null)
				MyObjects.Add (coin21);
			break;
		case 3:
			GameObject leaf31, leaf32, leaf33, chim31;
			x = StartX + Renderer.bounds.size.x / 3;
			y = transform.position.y;
			chim31 = CreateChim (x, y);
			MyObjects.Add (chim31);

			y = transform.position.y * 2 / 5;
			leaf31 = CreateRandomLeaf (x, y);
			MyObjects.Add (leaf31);

			y = transform.position.y * 3 / 5;
			leaf32 = CreateRandomLeaf (x, y);
			MyObjects.Add (leaf32);

			y = transform.position.y * 4 / 5;
			leaf33 = CreateRandomLeaf (x, y);
			MyObjects.Add (leaf33);

			y = transform.position.y * 2 / 5 + Factor;
			GameObject coin31 = CreateRandomCoin (x, y);
			if (coin31 != null)
				MyObjects.Add (coin31);

			y = transform.position.y * 3 / 5;
			GameObject coin32 = CreateRandomCoin (x, y);
			if (coin31 != null)
				MyObjects.Add (coin32);

			y = transform.position.y * 4 / 5;
			GameObject coin33 = CreateRandomCoin (x, y);
			if (coin31 != null)
				MyObjects.Add (coin33);
			break;
		default:
			break;
		}
	}

	void GenerateHardObstacles(int index)
	{
		float x, y;
		switch (index) {
		case 0:
			GameObject chim01, chim02, leaf01, leaf02;
			x = StartX;
			y = transform.position.y + Renderer.bounds.size.y / 5;
			chim01 = CreateChim (x, y);
			MyObjects.Add (chim01);

			x = StartX + Renderer.bounds.size.x / 3;
			y = transform.position.y + Renderer.bounds.size.y * 3 / 5;
			chim02 = CreateChim (x, y);
			MyObjects.Add (chim02);

			x = StartX;
			y = transform.position.y + Renderer.bounds.size.y * 4 / 5;
			leaf01 = CreateRandomLeaf (x, y);
			MyObjects.Add (leaf01);

			x = StartX + Renderer.bounds.size.x * 2 / 3;
			leaf02 = CreateRandomLeaf (x, y);
			MyObjects.Add (leaf02);

			x = StartX + Renderer.bounds.size.x / 3;
			y = transform.position.y + Renderer.bounds.size.y * 2 / 5;
			GameObject coin01 = CreateRandomCoin (x, y);
			MyObjects.Add (coin01);
			break;
		case 1:
			break;
		default:
			break;
		}
	}

	GameObject CreateRandomCoin(float x, float y)
	{
		GameObject obj = null;
		int isCreated = Random.Range (0, 2);
		if (isCreated != 0){
			obj = CreateCoin (x, y);
		}
		return obj;
	}

	GameObject CreateRandomLeaf(float x, float y)
	{
		GameObject obj;
		int leaf = Random.Range (0, 2);
		if (leaf == 0)
			obj = CreateLeaf1 ();
		else
			obj = CreateLeaf2 ();
//		x = StartX + Renderer.bounds.size.x * 2 / 3;
//		y = transform.position.y + Renderer.bounds.size.x * 4 / 5;
		obj.transform.position = new Vector3 (x, y, 0);
		return obj;
	}
		
	GameObject CreateLeaf1()
	{
		GameObject obj = Instantiate (Resources.Load ("SeaBG/Leaf1", typeof(GameObject))) as GameObject;
		return obj;
	}

	GameObject CreateLeaf2()
	{
		GameObject obj = Instantiate (Resources.Load ("SeaBG/Leaf2", typeof(GameObject))) as GameObject;
		return obj;
	}

	GameObject CreateSoc(float x, float y)
	{
		GameObject obj = Instantiate (Resources.Load ("SeaBG/soc", typeof(GameObject))) as GameObject;
		obj.transform.position = new Vector3 (x, y, 0);
		return obj;
	}

	GameObject CreateChim(float x, float y)
	{
		GameObject obj = Instantiate (Resources.Load ("SeaBG/chim", typeof(GameObject))) as GameObject;
		obj.transform.position = new Vector3 (x, y, 0);
		return obj;
	}
		
	GameObject CreateCoin(float x, float y)
	{
		GameObject obj = Instantiate (Resources.Load ("SeaBG/coin", typeof(GameObject))) as GameObject;
		obj.transform.position = new Vector3 (x, y, 0);
		return obj;
	}
}
