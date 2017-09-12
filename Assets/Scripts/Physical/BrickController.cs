using UnityEngine;
using System.Collections;

public class BrickController : PhysicalObjectController 
{
	public bool IsDestroyed
	{
		get
		{
			return gameObject.activeSelf;
		}
	}

	Animator animator;
	SpriteRenderer sRenderer;

	public void Spawn(Sprite sprite)
	{
		gameObject.SetActive(true);
		animator.enabled = false;
		sRenderer.sprite = sprite;
	}

	protected override void Start()
	{
		base.Start();
		SetReferences();
	}

	void SetReferences()
	{
		animator = GetComponent<Animator>();
		sRenderer = GetComponent<SpriteRenderer>();
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if(collision.gameObject.tag == Global.PUCK) 
		{
			Break();
		}
	}

	void Break()
	{
		EventControler.Event(EventList.BRICK_BROKEN);
		animator.enabled = true;
	}
}
