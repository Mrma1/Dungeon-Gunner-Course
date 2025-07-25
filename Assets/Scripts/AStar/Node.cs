using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IComparable<Node>
{
    public Vector2Int gridPosition;

    //起点的距离
    public int gCost = 0;

    //终点的距离
    public int hCost = 0;
    public Node parentNode;

    public Node(Vector2Int gridPosition)
    {
        this.gridPosition = gridPosition;
        parentNode = null;
    }

    public int FCost
    {
        get { return gCost + hCost; }
    }

    public int CompareTo(Node nodeToCompare)
    {
        //判断两个节点的F成本，如果F成本相同则判断H成本
        int compare = FCost.CompareTo(nodeToCompare);

        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return compare;
    }
}