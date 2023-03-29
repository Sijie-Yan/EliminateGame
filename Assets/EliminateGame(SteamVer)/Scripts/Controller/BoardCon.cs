using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardCon : MonoBehaviour
{
    // 当前关卡数据
    private QuestStat curQuestStat;

    // 每一个格子上的数据
    public GridStat[,] BoardStat;
    // 生成元素预制体
    public GameObject SpritePrefab;
    // 格子预制体
    public GameObject GridPrefab;

    internal int line;
    internal int column;
    internal int emptyNum;

    void Awake()
    {
        this.curQuestStat = QuestHandler.inst.QuestStat;
        this.line = QuestHandler.inst.QuestStat.curQuestTable.GridLine;
        this.column = QuestHandler.inst.QuestStat.curQuestTable.GridColumn;
        this.emptyNum = QuestHandler.inst.QuestStat.curQuestTable.QuestStartEmptyGridNum;
    }

    void Start()
    {
        // 订阅检查胜利或失败条件事件
        GameEventsHandler.current.onCheckWinOrLose += this.checkWinOrLose;
        // 格子加载完毕事件订阅
        GameEventsHandler.current.onGridLoaded += this.gridLoaded;

        this.BoardStat = new GridStat[this.line, this.column];

        this.loadGrids();
    }

    // 判断是否胜利
    private void checkWinOrLose()
    {
        int num = 0;
        for (int i = 0; i < this.BoardStat.GetLength(0); i++)
        {
            for (int j = 0; j < this.BoardStat.GetLength(1); j++)
            {
                if (this.BoardStat[i, j].isFilledWithSprite)
                    num++;
            }
        }
        if (num == 1)
        {
            this.win();
        }
        if (num == 9)
        {
            this.lose();
        }
    }

    private void win()
    {
        GameEventsHandler.current.win();
    }

    private void lose()
    {
        GameEventsHandler.current.lose();
    }

    // 加载格子
    private void loadGrids()
    {
        for (int i = 0; i < this.BoardStat.GetLength(0); i++)
        {
            for (int j = 0; j < this.BoardStat.GetLength(1); j++)
            {
                GameObject grid = Instantiate(this.GridPrefab, transform);
                float sizeX = grid.GetComponent<SpriteRenderer>().size.x;
                float sizeY = grid.GetComponent<SpriteRenderer>().size.y;
                grid.transform.localPosition = new Vector2((j - ((float)this.BoardStat.GetLength(1) / 2.0f) + 0.5f) * sizeX,
                                                      (((float)this.BoardStat.GetLength(0) / 2.0f) - i - 0.5f) * sizeY);
                this.BoardStat[i, j] = new GridStat(grid.transform.position, i, j);
            }
        }
        GameEventsHandler.current.gridLoaded();
    }

    // 格子加载完毕
    private void gridLoaded()
    {
        this.generateRandomSpriteOnBoard();
    }

    // 开始游戏时在棋盘上随机生成图标
    private void generateRandomSpriteOnBoard()
    {
        int emptyCount = 0;
        List<int> uniqueRandomNums = new List<int>();

        while (emptyCount < this.emptyNum)
        {
            int randomNum = Random.Range(0, this.BoardStat.GetLength(0) * this.BoardStat.GetLength(1));
            if (!uniqueRandomNums.Contains(randomNum))
            {
                uniqueRandomNums.Add(randomNum);
                emptyCount++;
            }
        }

        for (int i = 0; i < this.BoardStat.GetLength(0); i++)
        {
            for (int j = 0; j < this.BoardStat.GetLength(1); j++)
            {
                if (!uniqueRandomNums.Contains(i * this.BoardStat.GetLength(0) + j))
                {
                    int randomLoop = Random.Range(0, this.curQuestStat.elementLoops.Count);
                    int randomNum = Random.Range(0, this.curQuestStat.elementLoops[randomLoop].elementLoop.Count);
                    ElementStat newElement = new ElementStat(this.curQuestStat.elementLoops[randomLoop].elementLoop[randomNum]);
                    ElementHandler.inst.elementsOnBoard.Add(newElement);
                    this.BoardStat[i, j].elementOnGrid = newElement;

                    GameObject spritePrefab = Instantiate(this.SpritePrefab);
                    spritePrefab.GetComponent<DragableItemCon>().elementStat = newElement;
                    spritePrefab.GetComponent<Animator>().runtimeAnimatorController = newElement.elementAnimator;
                    spritePrefab.transform.position = this.BoardStat[i, j].relativeWorldPos;
                    spritePrefab.GetComponent<DragableItemCon>().setOnBoard();
                    this.BoardStat[i, j].isFilledWithSprite = true;
                    this.BoardStat[i, j].isShadowed = false;
                    this.BoardStat[i, j].spriteOnTheGrid = spritePrefab;
                }
            }
        }
    }

    private void OnDestroy()
    {
        GameEventsHandler.current.onCheckWinOrLose -= this.checkWinOrLose;
        GameEventsHandler.current.onGridLoaded -= this.gridLoaded;
    }
}