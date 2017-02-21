﻿using UnityEngine;
using System.Collections;

public class CommonTypes {

	public enum eTileType
	{
		TILE_TYPE_NONE,
		TILE_TYPE_PATH,
		TILE_TYPE_PATH_WITH_WP,
		TILE_TYPE_GROUND,
		TILE_TYPE_MAX,
	}
	public enum MinionTeam
	{
		MINION_TEAM_NONE,
		MINION_TEAM_USER,
		MINION_TEAM_ENEMY,
		MINION_TEAM_USER_HERO,
		MINION_TEAM_ENEMY_HERO,
		MINION_TEAM_MAX,
	}

	public enum StatusType
	{
		STATUS_TYPE_NONE,
		STATUS_TYPE_PLAY, // 진행
		STATUS_TYPE_MOVE,
		STATUS_TYPE_PAUSE, // 일시정지 
		STATUS_TYPE_SIGHTMOVE,
		STATUS_TYPE_RANGEMOVE,
		STATUS_TYPE_MAX,
	}


	public enum CollisionType
	{
		COLLISION_TYPE_NONE,
		COLLISION_TYPE_SIGHTCOLLISION,
		COLLISION_TYPE_RANGECOLLISION,
		COLLISION_TYPE_MAX,
	}

	public enum GameStatusType
	{
		GAMESTATUS_TYPE_NONE,
		GAMESTATUS_TYPE_PAUSE,
		GAMESTATUS_TYPE_RESUME,
		GAMESTATUS_TYPE_MAX,
	}
}
