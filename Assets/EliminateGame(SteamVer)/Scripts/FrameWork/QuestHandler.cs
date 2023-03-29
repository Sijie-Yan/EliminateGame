using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class QuestHandler
{
    private QuestHandler() { }

    private static QuestHandler _inst;
    // 所有关卡数据
    private QuestTable_SO[] _QuestTable;
    // 当前关卡数据类
    private QuestStat _CurQuestStat;

    public static QuestHandler inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = new QuestHandler();
            }
            return _inst;
        }
    }

    // 创建新的当前关卡数据
    public void creatNewQuestStat(int index)
    {
        if (this._CurQuestStat != null) { return; }
        else
        {
            this._CurQuestStat = new QuestStat(index);
        }
    }

    // 重置关卡数据
    public void resetQuestStat(int index)
    {
        this._CurQuestStat = null;
        this._CurQuestStat = new QuestStat(index);
    }

    public QuestTable_SO[] QuestTable
    {
        get
        {
            if (this._QuestTable == null)
            {
                Object[] resource = Resources.LoadAll("GameTables/QuestTables/QuestTable");
                this._QuestTable = new QuestTable_SO[resource.Length];
                for (int i = 0; i < resource.Length; i++)
                {
                    this._QuestTable[i] = (QuestTable_SO)resource[i];
                }
            }
            return this._QuestTable;
        }
    }

    public QuestStat QuestStat
    {
        get
        {
            return this._CurQuestStat;
        }
        set
        {
            this._CurQuestStat = value;
        }
    }
}