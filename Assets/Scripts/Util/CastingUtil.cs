public static class CastingUtil {

	public static string IDToString (PlayerID id) {
		switch (id) {
		case PlayerID.Left:
			return Global.LEFT_PLAYER;
		case PlayerID.Right:
			return Global.RIGHT_PLAYER;
		default:
			return null;
		}
	}
}
