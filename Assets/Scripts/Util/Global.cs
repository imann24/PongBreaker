﻿public static class Global {
	public const string LEFT_PLAYER = "LeftPlayer";
	public const string RIGHT_PLAYER = "RightPlayer";
	public const string GOAL = "Goal";
	public const string LEFT_PLAYER_GOAL = LEFT_PLAYER + GOAL;
	public const string RIGHT_PLAYER_GOAL = RIGHT_PLAYER + GOAL;
	public const string PUCK = "Puck";
	public const string BRICK = "Brick";

	public const float BASE_PADDLE_SPEED = 15.0f;
	public const float MAX_PUCK_STARTING_SPEED = 15.5f;
	public const float MAX_PUCK_SPEED = 20f;
	public const float MIN_PUCK_SPEED = 7.5f;
	public const int BASE_GOAL_SCORE = 1;
	public const float PUCK_RESPAWN_TIME = 3.0f;
	public const float SPEED_BOOST_MODIFIER = 1.5f;
}
