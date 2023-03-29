using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ElementHandler
{
    private ElementHandler() { }

    private static ElementHandler _inst;

    private List<LoopElementTable_SO> _elementLoopsTables;
    private List<ElementTable_SO> _elementsTables;

    private List<ElementStat> _elementsOnBoard;
    private List<ElementStat> _elementsOnTape;

    public static ElementHandler inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = new ElementHandler();
            }
            return _inst;
        }
    }

    // 所有元素循环表数组
    public List<LoopElementTable_SO> elementLoopsTables
    {
        get
        {
            if (this._elementLoopsTables == null)
            {
                this._elementLoopsTables = new List<LoopElementTable_SO>();
                LoopElementTable_SO[] loopArray = Resources.LoadAll<LoopElementTable_SO>("GameTables/ElementTables/LoopElementTables");
                for (int i = 0; i < loopArray.Length; i++)
                {
                    this._elementLoopsTables.Add(loopArray[i]);
                }
            }
            return this._elementLoopsTables;
        }
    }

    // 棋盘上的元素数组
    public List<ElementStat> elementsOnBoard
    {
        get
        {
            if (this._elementsOnBoard == null)
            {
                this._elementsOnBoard = new List<ElementStat>();
            }
            return this._elementsOnBoard;
        }
    }

    // 传送带上的元素数组
    public List<ElementStat> elementsOnTape
    {
        get
        {
            if (this._elementsOnTape == null)
            {
                this._elementsOnTape = new List<ElementStat>();
            }
            return this._elementsOnTape;
        }
    }
}