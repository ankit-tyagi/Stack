using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStack : MonoBehaviour {

	private GameObject[] theStack;

	private int stackIndex ;
	private int scorecount = 0;

	// Use this for initialization
	private void Start () {
		theStack = new GameObject[transform.childCount];
		for (int i = 0; i < transform.childCount; i++)
			theStack [i] = transform.GetChild (i).gameObject;

		stackIndex = transform.childCount - 1;
	}
	
	// Update is called once per frame
	private void Update () {
		if (Input.GetMouseButtonDown (0)) {
			SpawnTile ();
			scorecount++;
		}
	}

	private void SpawnTile() {
		stackIndex--;
		if (stackIndex < 0)
			stackIndex = transform.childCount - 1;
		
		theStack [stackIndex].transform.localPosition = new Vector3 (0, scorecount, 0);

	}

	private void PlaceTile(){
		theStack [stackIndex].transform.localPosition = new Vector3 (0, scorecount, 0);

	}
		
}
