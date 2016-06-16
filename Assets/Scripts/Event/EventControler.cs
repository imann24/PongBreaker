public static class EventControler {
	public delegate void NamedEventAction (string eventName);
	public static event NamedEventAction OnNamedEvent;

	public static void Event (string eventName) {
		if (OnNamedEvent != null) {
			OnNamedEvent(eventName);
		}
	}

}
