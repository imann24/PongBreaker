/*
 * Author: Isaiah Mann
 * Description: UI Button
 */

using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIButton : UIElement
{
	Button button;
	Action onClick;

	protected override void Awake()
	{
		base.Awake();
		button = GetComponent<Button>();
		button.onClick.AddListener(callOnClick);
	}

	public void AddAction(Action action)
	{
		onClick += action;
	}

	void callOnClick()
	{
		if(onClick != null)
		{
			onClick();
		}
	}
}
