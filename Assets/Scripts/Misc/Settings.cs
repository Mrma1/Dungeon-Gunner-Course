using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    //地牢房间重建次数
    public const int maxDungeonRebuildAttemptsForRoomGraph = 1000;
    //尝试构建次数
    public const int maxDungeonBuildAttempts = 10;
    
    
    #region ROOM SETTINGS

    //一个房间节点最多能连接3个走廊节点
    public const int maxChildCorridors = 3;

    #endregion
}
