public static class PlayerUtil {

	public static string IDToString (PaddlePosition id) {
		switch (id) {
		case PaddlePosition.Left:
			return Global.LEFT_PLAYER;
		case PaddlePosition.Right:
			return Global.RIGHT_PLAYER;
		default:
			return null;
		}
	}

	public static PaddlePosition GetPlayerFromGoalTag (string goalTag) {
		switch (goalTag) {
		case Global.LEFT_PLAYER_GOAL:
			return PaddlePosition.Left;
		case Global.RIGHT_PLAYER_GOAL:
			return PaddlePosition.Right;
		default:
			return PaddlePosition.None;
		}

	}

	public static PaddlePosition GetOpponent (PaddlePosition id) {
		switch (id) {
		case PaddlePosition.Left:
			return PaddlePosition.Right;
		case PaddlePosition.Right:
			return PaddlePosition.Left;
		default:
			return PaddlePosition.None;
		}
	}
}
