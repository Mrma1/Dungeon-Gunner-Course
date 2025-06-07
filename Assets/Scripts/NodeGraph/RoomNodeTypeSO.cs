using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeType_", menuName = "Scriptable Objects/Dungeon/Room Node Type")]
public class RoomNodeTypeSO : ScriptableObject
{
    public string roomNodeTypeName;
    
    [Header("是否在房间编辑器中显示")]
    public bool displayInNodeGraphEditor = true;
    [Header("是否为走廊")]
    public bool isCorridor;
    [Header("是否为南北走廊")]
    public bool isCorridorNS;
    [Header("是否为东西走廊")]
    public bool isCorridorEW;
    [Header("是否为入口")]
    public bool isEntrance;
    [Header("是否为Boss房间")]
    public bool isBossRoom;
    [Header("是否为空节点")]
    public bool isNone;
    
    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomNodeTypeName), roomNodeTypeName);
    }
#endif
    #endregion
}
