
using UnityEngine;

public class UIController : MonoBehaviourExtended
{
	protected void toggleCanvasGroup(CanvasGroup canvas, bool enabled)
	{
		canvas.alpha = enabled ? 1 : 0;
		canvas.interactable = enabled;
	}
}
