using UnityEngine;
using System.Collections;

public class BrickSpawner : MonoBehaviour {
	public GameObject BrickPrefab;

	GameObject [] bricks;
	Vector3 [] brickPositions;

	void Awake () {
		Init();
	}

	void Init () {
		Subscribe();
		GenerateListOfBricks();
	}

	void OnDestroy () {
		Unsubscribe();
	}
	
	void Subscribe () {
		EventControler.OnNamedEvent += HandleNamedEvent;
	}

	void Unsubscribe () {
		EventControler.OnNamedEvent -= HandleNamedEvent;
	}

	void HandleNamedEvent (string eventName) {
		if (eventName == EventList.GOAL) {
			FillInDestroyedBricks();
		}
	}

	void GenerateListOfBricks () {
		bricks = new GameObject[transform.childCount];
		brickPositions = new Vector3[transform.childCount];

		for (int i = 0; i < transform.childCount; i++) {
			bricks[i] = transform.GetChild(i).gameObject;
			brickPositions[i] = bricks[i].transform.position;
		}
	}

	void FillInDestroyedBricks () {
		for (int i = 0; i < transform.childCount; i++) {
			if (bricks[i] == null) {
				bricks[i] = SpawnBrick(brickPositions[i]);
			}
		}
	}

	GameObject SpawnBrick (Vector3 brickPosition) {
		GameObject brick = (GameObject) Instantiate(BrickPrefab);
		brick.transform.SetParent(transform);
		brick.transform.position = brickPosition;
		return brick;
	}
}
