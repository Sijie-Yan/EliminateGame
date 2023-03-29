using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStat
{
    // 当前关卡表数据
    private QuestTable_SO _curQuestTable;
    // 当前剩余元素个数
    private int _curLeftSpriteNum;
    public QuestStat(int curQuestIndex)
    {
        this._curQuestTable = QuestHandler.inst.QuestTable[curQuestIndex];
        this._curLeftSpriteNum = this._curQuestTable.QuestStartSpriteNum;
    }

    public int curLeftSpriteNum
    {
        get
        {
            return this._curLeftSpriteNum;
        }
        set
        {
            this._curLeftSpriteNum = value;
        }
    }

    public QuestTable_SO curQuestTable
    {
        get
        {
            return this._curQuestTable;
        }
    }

    // 当前关卡循环元素数组
    public List<LoopElementTable_SO> elementLoops
    {
        get
        {
            return this._curQuestTable.QuestElementLoops;
        }
    }
}