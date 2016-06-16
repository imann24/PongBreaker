using UnityEngine;
using System.Collections;

public class BrickSpawner : MonoBehaviour {
	public GameObject BrickPrefab;

	GameObject [] bricks;

	public Sprite DefaultBrickSprite;

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

		for (int i = 0; i < transform.childCount; i++) {
			bricks[i] = transform.GetChild(i).gameObject;
		}
	}

	void FillInDestroyedBricks () {
		for (int i = 0; i < transform.childCount; i++) {
			if (!bricks[i].activeSelf) {
				SpawnBrick(bricks[i]);
			}
		}
	}

	GameObject SpawnBrick (GameObject brick) {
		brick.SetActive(true);
		brick.GetComponent<Animator>().enabled = false;
		brick.GetComponent<SpriteRenderer>().sprite = DefaultBrickSprite;
		return brick;
	}
}
