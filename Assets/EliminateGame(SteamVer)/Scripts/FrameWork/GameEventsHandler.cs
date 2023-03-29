using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameEventsHandler
{
    private GameEventsHandler() { }
    private static GameEventsHandler _current;
    public static GameEventsHandler current
    {
        get
        {
            if (_current == null)
            {
                _current = new GameEventsHandler();
            }
            return _current;
        }
    }

    // 传送带上的元素被放到棋盘上
    public event Action<GameObject> onSpritePutOnTheBoard;
    public void spritePutOnTheBoard(GameObject chosenSprite)
    {
        if (onSpritePutOnTheBoard != null)
        {
            onSpritePutOnTheBoard(chosenSprite);
        }
    }

    // 泡泡里生成新元素
    public event Action onGenerateNewSpriteInBubble;
    public void generateNewSpriteInBubble()
    {
        if (onGenerateNewSpriteInBubble != null)
        {
            onGenerateNewSpriteInBubble();
        }
    }

    // 改变剩余元素数量
    public event Action onChangeLeftSpriteNum;
    public void changeLeftSpriteNum()
    {
        if (onChangeLeftSpriteNum != null)
        {
            onChangeLeftSpriteNum();
        }
    }

    // 初始格子加载完毕
    public event Action onGridLoaded;
    public void gridLoaded()
    {
        if (onGridLoaded != null)
        {
            onGridLoaded();
        }
    }

    // 检查胜利或失败条件
    public event Action onCheckWinOrLose;
    public void checkWinOrLose()
    {
        if (onCheckWinOrLose != null)
        {
            onCheckWinOrLose();
        }
    }

    // 胜利
    public event Action onWin;
    public void win()
    {
        if (onWin != null)
        {
            onWin();
        }
    }

    // 失败
    public event Action onLose;
    public void lose()
    {
        if (onLose != null)
        {
            onLose();
        }
    }
}
