using UnityEngine;
using System.Collections.Generic;

public class PhysicalObjectController : MonoBehaviourExtended
{
	protected Rigidbody2D rigibody;
	protected SpriteRenderer sprite;

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

	void setReferences()
	{
		rigibody = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
	}
}
