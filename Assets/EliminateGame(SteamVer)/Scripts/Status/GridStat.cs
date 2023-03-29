using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStat
{
    // 格子上的元素数据
    public ElementStat elementOnGrid;
    // 世界位置坐标
    public Vector2 relativeWorldPos;
    public bool isShadowed = false;
    public bool isFilledWithSprite = false;
    // 行
    public int line = 0;
    // 列
    public int column = 0;
    // 格子上的符号
    public GameObject spriteOnTheGrid;
    // 格子符号
    public GameObject gridSprite;
    public GridStat()
    {
        this.spriteOnTheGrid = null;
    }
    public GridStat(Vector2 relativeWorldPos, int line, int column)
    {
        this.relativeWorldPos = relativeWorldPos;
        this.line = line;
        this.column = column;
        this.spriteOnTheGrid = null;
    }
}