using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStack : MonoBehaviour {

	private const float BOUNCE_SIZE = 3.5f;
	private const float STACK_MOVING_SPEED = 5.0f;

	private GameObject[] theStack;

	private int stackIndex ;
	private int scorecount = 0;

	private float tileTransition = 0.0f;
	private float tileSpeed = 2.5f;
	private float secondaryposition;

	private bool isMovingOnX = true;

	private Vector3 desiredPosition;


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
			if (PlaceTile ()) {
				SpawnTile ();
				scorecount++;
			} else {
				EndGame ();
			}
		}

		MoveTile ();

		transform.position = Vector3.Lerp (transform.position, desiredPosition, STACK_MOVING_SPEED * Time.deltaTime);
	}

	private void MoveTile() {
		tileTransition += Time.deltaTime * tileSpeed; 

		if (isMovingOnX) {
			theStack [stackIndex].transform.localPosition = new Vector3 (Mathf.Sin (tileTransition) * 1.25f, scorecount, secondaryposition);
		} else {
			theStack [stackIndex].transform.localPosition = new Vector3 (secondaryposition, scorecount, Mathf.Sin (tileTransition) * 1.25f);
		}

	}

	private void SpawnTile() {
		stackIndex--;
		if (stackIndex < 0)
			stackIndex = transform.childCount - 1;

		desiredPosition = (Vector3.down) * scorecount;

		theStack [stackIndex].transform.localPosition = new Vector3 (0, scorecount, 0);

	}

	private bool PlaceTile(){
		
		Transform t = theStack [stackIndex].transform;

		secondaryposition = (isMovingOnX)
			? t.localPosition.x
			: t.localPosition.z;
		
		isMovingOnX = !isMovingOnX;
		return true;
	}

	private void EndGame () {
	}
		
}
