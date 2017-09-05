using UnityEngine;
using System.Collections;

public class PhysicalObjectController : MonoBehaviourExtended
{
	protected Rigidbody2D rigibody;
	protected SpriteRenderer sprite;

	// Use this for initialization
	protected override void Awake() 
	{
		base.Awake();
		SetReferences();
	}

	void SetReferences()
	{
		rigibody = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
	}
}
