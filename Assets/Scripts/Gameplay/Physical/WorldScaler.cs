using UnityEngine;

public class WorldScaler : MonoBehaviourExtended 
{
	[SerializeField]
	Vector2 targetAspectRatio = new Vector2(3, 2);

	[SerializeField]
	float scaleFactor = 5;

	protected override void Awake()
	{
		base.Awake();
		scaleWorld();
	}

	float getTargetAspect()
	{
		return targetAspectRatio.x / targetAspectRatio.y;
	}

	void scaleWorld()
	{
		float aspectDifference = getTargetAspect() - Camera.main.aspect;
		aspectDifference *= scaleFactor;
		foreach(WorldScalingComponent component in  GetComponentsInChildren<WorldScalingComponent>())
		{
			scaleComponent(component, aspectDifference);
		}
	}

	void scaleComponent(WorldScalingComponent component, float distance)
	{
		component.Shift(distance );
	}
}
