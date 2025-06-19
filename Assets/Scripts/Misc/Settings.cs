using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
	public const float pixelsPerUnit = 16f;
	public const float tileSizePixels = 16f;

    //地牢房间重建次数
    public const int maxDungeonRebuildAttemptsForRoomGraph = 1000;
    //尝试构建次数
    public const int maxDungeonBuildAttempts = 10;


	#region ROOM SETTINGS
	public const float fadeInTime = 0.5f;
    //一个房间节点最多能连接3个走廊节点
    public const int maxChildCorridors = 3;

    #endregion

    #region  动画设置
    public static int aimUp = Animator.StringToHash("aimUp");
	public static int aimDown = Animator.StringToHash("aimDown");
	public static int aimUpRight = Animator.StringToHash("aimUpRight");
	public static int aimUpLeft = Animator.StringToHash("aimUpLeft");
	public static int aimRight = Animator.StringToHash("aimRight");
	public static int aimLeft = Animator.StringToHash("aimLeft");
	public static int isIdle = Animator.StringToHash("isIdle");
	public static int isMoving = Animator.StringToHash("isMoving");
	public static int rollUp = Animator.StringToHash("rollUp");
	public static int rollRight = Animator.StringToHash("rollRight");
	public static int rollLeft = Animator.StringToHash("rollLeft");
	public static int rollDown = Animator.StringToHash("rollDown");
	public static float baseSpeedForPlayerAnimations = 8f;

	public static int open = Animator.StringToHash("open");
	#endregion

	public const string playerTag = "Player";
	public const string playerWeapon = "playerWeapon";

	//使用瞄准角度距离
	public const float useAimAngleDistance = 3.5f;
}
