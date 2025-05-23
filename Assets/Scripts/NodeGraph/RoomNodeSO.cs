using UnityEditor;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomNodeSO : ScriptableObject
{
    [HideInInspector] public string id;
    [HideInInspector] public List<string> parentRoomNodeIDList = new List<string>();
    [HideInInspector] public List<string> childRoomNodeIDList = new List<string>();
    [HideInInspector] public RoomNodeGraphSO roomNodeGraph;
    public RoomNodeTypeSO roomNodeType;
    [HideInInspector] public RoomNodeTypeListSO roomNodeTypeList;

    #region Editor Code

#if UNITY_EDITOR
    [HideInInspector] public Rect rect;
    [HideInInspector] public bool isLeftClickDragging = false;
    [HideInInspector] public bool isSelected = false;

    public void Initialise(Rect rect, RoomNodeGraphSO roomNodeGraph, RoomNodeTypeSO roomNodeType)
    {
        this.rect = rect;
        id = Guid.NewGuid().ToString();
        name = "RoomNode";
        this.roomNodeGraph = roomNodeGraph;
        this.roomNodeType = roomNodeType;
        
        roomNodeTypeList = GameResources.Instance.roomNodeTypeList;
    }
    
    public void Draw(GUIStyle nodeStyle)
    {
        GUILayout.BeginArea(rect, nodeStyle);
        EditorGUI.BeginChangeCheck();

        //如果已经被连接或者是入口就显示标签
        if (parentRoomNodeIDList.Count > 0 || roomNodeType.isEntrance)
        {
            EditorGUILayout.LabelField(roomNodeType.roomNodeTypeName);
        }
        else
        {
            //当前房间类型索引
            int selected = roomNodeTypeList.list.FindIndex(x => x == roomNodeType);
            //选中的房间类型索引
            int selection = EditorGUILayout.Popup("", selected, GetRoomNodeTypesToDisplay());
        
            roomNodeType = roomNodeTypeList.list[selection];

            //清除连接
            if (roomNodeTypeList.list[selected].isCorridor && !roomNodeTypeList.list[selection].isCorridor || !roomNodeTypeList.list[selected].isCorridor
                && roomNodeTypeList.list[selection].isCorridor || !roomNodeTypeList.list[selected].isBossRoom && roomNodeTypeList.list[selection].isBossRoom)
            {
                if (childRoomNodeIDList.Count > 0)
                {
                    for (int i = childRoomNodeIDList.Count - 1; i >= 0; i--)
                    {
                        RoomNodeSO childRoomNode = roomNodeGraph.GetRoomNode(childRoomNodeIDList[i]);
                        if (childRoomNode != null)
                        {
                            RemoveChildRoomNodeIDFromRoomNode(childRoomNode.id);
                            childRoomNode.RemoveParentRoomNodeIDFromRoomNode(id);
                        }
                    }
                }
            }
        }
        
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(this);
        }
        
        GUILayout.EndArea();
    }

    /// <summary>
    /// 获取可显示菜单房间
    /// </summary>
    /// <returns></returns>
    private string [] GetRoomNodeTypesToDisplay()
    {
        string[] roomArray = new string[roomNodeTypeList.list.Count];

        for (int i = 0; i < roomNodeTypeList.list.Count; i++)
        {
            if (roomNodeTypeList.list[i].displayInNodeGraphEditor)
            {
                roomArray[i] = roomNodeTypeList.list[i].roomNodeTypeName;
            }
        }
        
        return roomArray;
    }

    /// <summary>
    /// 事件集合
    /// </summary>
    /// <param name="currentEvent"></param>
    public void ProcessEvents(Event currentEvent)
    {
        switch (currentEvent.type)
        {
            case EventType.MouseDown:
                ProcessMouseDownEvent(currentEvent);
                break;
            case EventType.MouseUp:
                ProcessMouseUpEvent(currentEvent);
                break;
            case EventType.MouseDrag:
                ProcessMouseDragEvent(currentEvent);
                break;
        }
    }

    /// <summary>
    /// 鼠标单击按下事件
    /// </summary>
    /// <param name="currentEvent"></param>
    private void ProcessMouseDownEvent(Event currentEvent)
    {
        if (currentEvent.button == 0)
        {
            ProcessLeftClickDownEvent();
        }
        else if (currentEvent.button == 1)
        {
            ProcessRightClickDownEvent(currentEvent);
        }
    }

    /// <summary>
    /// 鼠标左键单击按下事件
    /// </summary>
    private void ProcessLeftClickDownEvent()
    {
        Selection.activeObject = this;
        isSelected = !isSelected;
    }

    /// <summary>
    /// 鼠标右键单击按下事件
    /// </summary>
    /// <param name="currentEvent"></param>
    private void ProcessRightClickDownEvent(Event currentEvent)
    {
        roomNodeGraph.SetNodeToDrawConnectionLineFrom(this, currentEvent.mousePosition);
    }
    
    /// <summary>
    /// 鼠标单击抬起事件
    /// </summary>
    /// <param name="currentEvent"></param>
    private void ProcessMouseUpEvent(Event currentEvent)
    {
        if (currentEvent.button == 0)
        {
            ProcessLeftClickUpEvent();
        }
    }

    /// <summary>
    /// 鼠标左键单击抬起事件
    /// </summary>
    private void ProcessLeftClickUpEvent()
    {
        if (isLeftClickDragging)
        {
            isLeftClickDragging = false;
        }
    }
    
    /// <summary>
    /// 鼠标拖动事件
    /// </summary>
    /// <param name="currentEvent"></param>
    private void ProcessMouseDragEvent(Event currentEvent)
    {
        if (currentEvent.button == 0)
        {
            ProcessLeftMouseDragEvent(currentEvent);
        }
    }

    /// <summary>
    /// 鼠标左键拖动事件
    /// </summary>
    /// <param name="currentEvent"></param>
    private void ProcessLeftMouseDragEvent(Event currentEvent)
    {
        isLeftClickDragging = true;
        DragNode(currentEvent.delta);
        GUI.changed = true;
    }

    public void DragNode(Vector2 delta)
    {
        rect.position += delta;
        EditorUtility.SetDirty(this);
    }

    /// <summary>
    /// 添加子房间
    /// </summary>
    /// <param name="childID"></param>
    /// <returns></returns>
    public bool AddChildRoomNodeIDToRoomNode(string childID)
    {
        if (IsChildRoomValid(childID))
        {
            childRoomNodeIDList.Add(childID);
            return true;
        }

        return false;
    }

    //验证连接是否合法
    public bool IsChildRoomValid(string childID)
    {
        //Boos房间是否连接
        bool isConnectedBossNodeAlready = false;
        foreach (RoomNodeSO node in roomNodeGraph.roomNodelist)
        {
            if (node.roomNodeType.isBossRoom && node.parentRoomNodeIDList.Count > 0)
            {
                isConnectedBossNodeAlready = true;
            }
        }
        
        //重复连接
        if (childRoomNodeIDList.Contains(childID))
            return false;

        //自己连接自己
        if (id == childID)
            return false;
        
        //已经被连接
        if (parentRoomNodeIDList.Contains(childID))
            return false;
        
        RoomNodeSO roomNode = roomNodeGraph.GetRoomNode(childID);
        if (roomNode != null)
        {
            //Boss房间并且已经连接
            if (roomNode.roomNodeType.isBossRoom && isConnectedBossNodeAlready)
                return false;
        
            if (roomNode.roomNodeType.isNode)
                return false;
            //已经被连接
            if (roomNode.parentRoomNodeIDList.Count > 0)
                return false;
            
            //走廊节点不能连接走廊节点
            if (roomNode.roomNodeType.isCorridor && roomNodeType.isCorridor)
                return false;

            //两个节点都不是走廊不能连接，一个房间必须连接一个走廊
            if (!roomNode.roomNodeType.isCorridor && !roomNodeType.isCorridor)
                return false;

            //连接的走廊数量超过最大值
            if (roomNode.roomNodeType.isCorridor && childRoomNodeIDList.Count >= Settings.maxChildCorridors)
                return false;

            //不能连接入口房间
            if (roomNode.roomNodeType.isEntrance)
            {
                return false;
            }

            //已经连接
            if (!roomNode.roomNodeType.isCorridor && childRoomNodeIDList.Count > 0)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// 添加父房间
    /// </summary>
    /// <param name="parentID"></param>
    /// <returns></returns>
    public bool AddParentRoomNodeIDToRoomNode(string parentID)
    {
        parentRoomNodeIDList.Add(parentID);
        return true;
    }

    /// <summary>
    /// 通过childID移除子房间
    /// </summary>
    /// <param name="childID"></param>
    /// <returns></returns>
    public bool RemoveChildRoomNodeIDFromRoomNode(string childID)
    {
        if (childRoomNodeIDList.Contains(childID))
        {
            childRoomNodeIDList.Remove(childID);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 通过parentID移除父房间
    /// </summary>
    /// <param name="parentID"></param>
    /// <returns></returns>
    public bool RemoveParentRoomNodeIDFromRoomNode(string parentID)
    {
        if (parentRoomNodeIDList.Contains(parentID))
        {
            parentRoomNodeIDList.Remove(parentID);
            return true;
        }

        return false;
    }
#endif

    #endregion
}
