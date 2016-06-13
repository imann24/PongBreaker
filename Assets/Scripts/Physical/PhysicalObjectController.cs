using UnityEngine;
using System.Collections;

public class PhysicalObjectController : MonoBehaviour {
	protected Rigidbody2D rigibody;
	// Use this for initialization
	void Awake () {
		SetReferences();
	}

	void SetReferences () {
		rigibody = GetComponent<Rigidbody2D>();
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
