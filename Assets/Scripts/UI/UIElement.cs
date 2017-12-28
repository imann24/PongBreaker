/*
 * Author: Isaiah Mann
 * Description: Basic UI element
 */

using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviourExtended
{
	public bool HasImage
	{
		get
		{
			return image;
		}
	}

	public bool HasText
	{
		get
		{
			return text;
		}
	}

	[SerializeField]
	Image imageOverride;
	[SerializeField]
	Text textOverride;

	Image image;
	Text text;

	protected override void Awake()
	{
		base.Awake();
		setImage();
		setText();
	}

	void setImage()
	{
		if(imageOverride)
		{
			image = imageOverride;
		}
		else
		{
			image = GetComponentInChildren<Image>();
		}
	}


	void setText()
	{
		if(textOverride)
		{
			text = textOverride;
		}
		else
		{
			text = GetComponentInChildren<Text>();
		}
	}

	public void SetImage(Sprite sprite)
	{
		if(HasImage)
		{
			image.sprite = sprite;
		}
	}

	public void SetText(string text)
	{
		if(HasText)
		{
			this.text.text = text;
		}
	}
}
