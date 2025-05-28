using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonLevel_", menuName = "Scriptable Objects/Dungeon/Dungeon Level")]
public class DungeonLevelSO : ScriptableObject
{
    public string levelName;
    public List<RoomTemplateSO> roomTemplateList;
    public List<RoomNodeGraphSO> roomNodeGraphList;

    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(levelName), levelName);
        if (HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomTemplateList), roomTemplateList))
            return;
        if (HelperUtilities.ValidateCheckEnumerableValues(this, nameof(roomNodeGraphList), roomNodeGraphList))
            return;
        
        //检查是否有南北走廊、东西走廊和入口
        bool isEWCorridor = false;
        bool isNSCorridor = false;
        bool isEntrance = false;

        foreach (RoomTemplateSO roomTemplateSo in roomTemplateList)
        {
            if (roomTemplateSo == null)
                return;

            if (roomTemplateSo.roomNodeType.isCorridorEW)
                isEWCorridor = true;

            if (roomTemplateSo.roomNodeType.isCorridorNS)
                isNSCorridor = true;

            if (roomTemplateSo.roomNodeType.isEntrance)
                isEntrance = true;
        }

        if (isEWCorridor == false)
        {
            Debug.Log("没有东西走廊房间");
        }
        
        if (isNSCorridor == false)
        {
            Debug.Log("没有南北走廊房间");
        }
        
        if (isEntrance == false)
        {
            Debug.Log("没有入口房间");
        }

        foreach (RoomNodeGraphSO roomNodeGraph in roomNodeGraphList)
        {
            if (roomNodeGraph == null)
                return;

            foreach (RoomNodeSO roomNode in roomNodeGraph.roomNodelist)
            {
                if (roomNode == null)
                    continue;

                if (roomNode.roomNodeType.isEntrance || roomNode.roomNodeType.isCorridorEW || roomNode.roomNodeType.isCorridorNS ||
                    roomNode.roomNodeType.isCorridor || roomNode.roomNodeType.isNode)
                    continue;

                bool isRoomNodeTypeFound = false;

                foreach (RoomTemplateSO roomTemplateSo in roomTemplateList)
                {
                    if (roomTemplateSo == null)
                        continue;
                    
                    if (roomTemplateSo.roomNodeType == roomNode.roomNodeType)
                    {
                        isRoomNodeTypeFound = true;
                        break;
                    }
                }

                if (!isRoomNodeTypeFound)
                {
                    Debug.Log("In " + name.ToString() + " : No room template" + roomNode.roomNodeType.name.ToString() + " found fo node graph"
                        + roomNodeGraph.name.ToString());
                }
            }
        }
    }
#endif

    #endregion
}
