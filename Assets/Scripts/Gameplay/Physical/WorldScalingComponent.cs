using UnityEngine;

public class WorldScalingComponent : MonoBehaviourExtended
{
	[SerializeField]
	PaddlePosition position;

	public void Shift(float distance)
	{
		if(position == PaddlePosition.Right)
		{
			distance *= -1;
		}
		Vector3 objectPosition = transform.position;
		objectPosition.x += distance;
		transform.position = objectPosition;
	}
}
