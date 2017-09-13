using UnityEngine;
using UnityEngine.UI;

public class DebugPaddleUIDisplay : MonoBehaviourExtended
{
	[SerializeField]
	GameObject player;
	PlayerPaddleController playerPaddle;

	[SerializeField]
	Text touchesDisplay;
	[SerializeField]
	Text kinematicDisplay;
	[SerializeField]
	Text velocityDisplay;

	[SerializeField]
	string touchesFormat = "<b>Touches</b>: {0}";
	[SerializeField]
	string kinematicFormat = "<b>IsKinematic</b>: {0}";
	[SerializeField]
	string velocityFormat = "<b>Velocity</b>: {0}";

	protected override void Start()
	{
		base.Start();
		playerPaddle = player.GetComponent<PlayerPaddleController>();
		if(!playerPaddle)
		{
			Destroy(gameObject);
		}
	}

	void Update()
	{
		updateStateDisplay();
	}

	void updateStateDisplay()
	{
		touchesDisplay.text = string.Format(touchesFormat, playerPaddle.TouchCount);
		kinematicDisplay.text = string.Format(kinematicFormat, playerPaddle.IsKinematic);
		velocityDisplay.text = string.Format(velocityFormat, playerPaddle.Velocity);
	}
}
