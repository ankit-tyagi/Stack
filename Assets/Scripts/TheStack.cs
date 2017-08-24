using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStack : MonoBehaviour {

	private const float BOUNCE_SIZE = 3.5f;
	private const float STACK_MOVING_SPEED = 5.0f;
	private const float ERROR_MARGIN = 0.1f;

	private GameObject[] theStack;
	private Vector2 stackBounds = new Vector2 (BOUNCE_SIZE, BOUNCE_SIZE);

	private int stackIndex ;
	private int scorecount = 0;
	private int combo = 0;

	private float tileTransition = 0.0f;
	private float tileSpeed = 2.5f;
	private float secondaryposition;

	private bool isMovingOnX = true;
	private bool gameOver = false;

	private Vector3 desiredPosition;
	private Vector3 lastTilePosition;

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

		transform.position = Vector3.Lerp (transform.position,desiredPosition,STACK_MOVING_SPEED * Time.deltaTime);
	}

	private void MoveTile() {
		if (gameOver)
			return;  

		tileTransition += Time.deltaTime * tileSpeed; 

		if (isMovingOnX) {
			theStack [stackIndex].transform.localPosition = new Vector3 (Mathf.Sin(tileTransition) *BOUNCE_SIZE, scorecount, secondaryposition);
		} else {
			theStack [stackIndex].transform.localPosition = new Vector3 (secondaryposition, scorecount, Mathf.Sin(tileTransition) *BOUNCE_SIZE);
		}

	}

	private void SpawnTile() {
		lastTilePosition = theStack [stackIndex].transform.localPosition; 
		stackIndex--;
		if (stackIndex < 0)
			stackIndex = transform.childCount - 1;

		desiredPosition = (Vector3.down) * scorecount;

		theStack [stackIndex].transform.localPosition = new Vector3 (0, scorecount, 0);

	}

	private bool PlaceTile(){
		
		Transform t = theStack [stackIndex].transform;

		if (isMovingOnX) {
			float deltaX = lastTilePosition.x - t.position.x;
			if (Mathf.Abs (deltaX) > ERROR_MARGIN) {
				//cut the tile
				combo = 0;
				stackBounds.x -= Mathf.Abs (deltaX);
				if (stackBounds.x <= 0) 
					return false;

				float middle = lastTilePosition.x + t.localPosition.x / 2;
				t.localScale = new Vector3 (stackBounds.x, 1, stackBounds.y);
				t.localPosition = new Vector3 (middle - (lastTilePosition.x / 2), scorecount, lastTilePosition.z);
			}
		} else {
			float deltaZ = lastTilePosition.z - t.position.z;
			if (Mathf.Abs (deltaZ) > ERROR_MARGIN) {

				//cut the tile
				combo = 0;
				stackBounds.y -= Mathf.Abs (deltaZ);
				if (stackBounds.y <= 0)
					return false;

				float middle = lastTilePosition.z + t.localPosition.z / 2;
				t.localScale = new Vector3 (stackBounds.x, 1, stackBounds.y);
				t.localPosition = new Vector3 ( lastTilePosition.x, scorecount,middle - (lastTilePosition.z / 2));
			}
		}

		secondaryposition = (isMovingOnX)
			? t.localPosition.x
			: t.localPosition.z;
		
		isMovingOnX = !isMovingOnX;
		return true;
	}

	private void EndGame () {
		Debug.Log ("Lose");
		gameOver = true;
		theStack [stackIndex].AddComponent<Rigidbody> ();
	}
		
}


