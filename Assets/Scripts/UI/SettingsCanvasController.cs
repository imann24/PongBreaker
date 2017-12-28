/*
 * Author: Isaiah Mann
 * Description: Settings screen
 */

using UnityEngine;

public class SettingsCanvasController : UIController
{
	const string ON = "On";
	const string OFF = "Off";
	const string MUSIC = "Music";
	const string SFX = "Sound Effects";

	[SerializeField]
	UIButton musicButton;
	[SerializeField]
	UIButton sfxButton;

	protected override void Start()
	{
		base.Start();
		musicButton.AddAction(toggleMusic);
		musicButton.AddAction(setButtons);
		sfxButton.AddAction(toggleSFX);
		sfxButton.AddAction(setButtons);
		setButtons();
	}

	void setButtons()
	{
		musicButton.SetText(getMusicButtonText());
		sfxButton.SetText(getSFXButtonText());
	}

	string getMusicButtonText()
	{
		return string.Format("{0}: <i>{1}</i>", MUSIC, SettingsUtil.MusicMuted ? OFF : ON);
	}

	string getSFXButtonText()
	{
		return string.Format("{0}: <i>{1}</i>", SFX, SettingsUtil.SFXMuted ? OFF : ON);
	}
		
	void toggleMusic()
	{
		SettingsUtil.ToggleMusicMuted();
	}

	void toggleSFX()
	{
		SettingsUtil.ToggleSFXMuted();	
	}
}
