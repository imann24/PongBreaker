using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicalObjectController : MonoBehaviourExtended
{
	public float ScaleX
	{
		get
		{
			return transform.localScale.x;
		}
		set
		{
			Vector3 current = transform.localScale;
			if(scalingCoroutine != null)
			{
				StopCoroutine(scalingCoroutine);
			}
			StartCoroutine(scalingCoroutine = scaleOverTime(new Vector3(value, current.y, current.z), defaultTimeToScale));
		}
	}

	protected Rigidbody2D rigibody;
	protected SpriteRenderer sprite;


	[SerializeField]
	float defaultTimeToScale = 1.75f;
	IEnumerator scalingCoroutine;

	// Use this for initialization
	protected override void Awake() 
	{
		base.Awake();
		setReferences();
	}

	public float GetYPosition()
	{
		return transform.localPosition.y;
	}

	public float GetXPosition()
	{
		return transform.localPosition.x;
	}

	public PaddlePosition GetDirection()
	{
		float xVelocity = rigibody.velocity.x;
		if(xVelocity > 0)
		{
			return PaddlePosition.Right;
		}
		else if(xVelocity < 0)
		{
			return PaddlePosition.Left;
		}
		else
		{
			return PaddlePosition.None;
		}
	}

	protected float getXDistanceFrom(PhysicalObjectController phyiscalObject)
	{
		return Mathf.Abs(phyiscalObject.GetXPosition() - GetXPosition());
	}

	protected bool tryGetClosestInstance(IEnumerable<PhysicalObjectController> physicalObjects, out PhysicalObjectController match, float maxXDistance = float.MaxValue)
	{
		foreach(PhysicalObjectController physicalObject in physicalObjects)
		{
			if(getXDistanceFrom(physicalObject) < maxXDistance)
			{
				match = physicalObject;
				return true;
			}
		}
		match = null;
		return false;
	}

	IEnumerator scaleOverTime(Vector3 targetScale, float timeToScale)
	{
		float timer = 0;
		Vector3 startingScale = transform.localScale;
		while(timer <= timeToScale)
		{
			transform.localScale = Vector3.Lerp(startingScale, targetScale, timer / timeToScale);
			yield return new WaitForEndOfFrame();
			timer += Time.deltaTime;
		}
		transform.localScale = targetScale;
	}

	void setReferences()
	{
		rigibody = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
	}
}
