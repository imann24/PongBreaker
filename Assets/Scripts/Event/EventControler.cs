public static class EventController {
	public delegate void NamedEventAction (string eventName);
	public static event NamedEventAction OnNamedEvent;

	public delegate void AudioEventAction(AudioActionType actionType, AudioType audioType);
	public static event AudioEventAction OnAudioEvent;

	public static void Event(string eventName) 
	{
		if(OnNamedEvent != null)
		{
			OnNamedEvent(eventName);
		}
	}

	public static void Event(AudioActionType actionType, AudioType audioType)
	{
		if(OnAudioEvent != null) 
		{
			OnAudioEvent(actionType, audioType);
		}
	}
}
