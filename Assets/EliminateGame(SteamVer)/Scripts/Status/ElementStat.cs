using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementStat
{
    // 表数据
    private ElementTable_SO _elementTable;

    // 循环下标
    public int loopNum;
    // 所在循环内部下标（大吃小）
    public int loopIndex;
    // 是否在棋盘上
    public bool isOnBoard;

    public ElementStat(ElementTable_SO elementTable)
    {
        this._elementTable = elementTable;
        this.loopNum = this._elementTable.loopNum;
        this.loopIndex = this._elementTable.loopIndex;
    }

    public RuntimeAnimatorController elementAnimator
    {
        get
        {
            return this._elementTable.elementAnimator;
        }
    }
}