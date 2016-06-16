using UnityEngine;
using System.Collections;

public class PhysicalObjectController : MonoBehaviour {
	protected Rigidbody2D rigibody;
	protected SpriteRenderer sprite;

	// Use this for initialization
	void Awake () {
		SetReferences();
	}

	void SetReferences () {
		rigibody = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
