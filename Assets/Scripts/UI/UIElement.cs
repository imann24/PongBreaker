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

	[SerializeField]
	Image imageOverride;

	Image image;

	protected override void Awake()
	{
		base.Awake();
		setImage();
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

	public void SetImage(Sprite sprite)
	{
		if(HasImage)
		{
			image.sprite = sprite;
		}
	}
}
