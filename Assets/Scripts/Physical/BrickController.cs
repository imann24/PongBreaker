using UnityEngine;
using System.Collections;

public class BrickController : PhysicalObjectController {

	Animator animator;

	void Start () {
		SetReferences();
	}

	void SetReferences () {
		animator = GetComponent<Animator>();
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == Global.PUCK) {
			BreakBrick();
		}
	}

	void BreakBrick () {
		EventControler.Event(EventList.BRICK_BROKEN);
		animator.enabled = true;
	}
}
