public static class PlayerUtil {

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

	public static PlayerID GetPlayerFromGoalTag (string goalTag) {
		switch (goalTag) {
		case Global.LEFT_PLAYER_GOAL:
			return PlayerID.Left;
		case Global.RIGHT_PLAYER_GOAL:
			return PlayerID.Right;
		default:
			return PlayerID.None;
		}

	}

	public static PlayerID GetOpponent (PlayerID id) {
		switch (id) {
		case PlayerID.Left:
			return PlayerID.Right;
		case PlayerID.Right:
			return PlayerID.Left;
		default:
			return PlayerID.None;
		}
	}
}
