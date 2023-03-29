using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragableItemCon : MonoBehaviour
{

    // 当前关卡数据
    private QuestStat curQuestStat;
    // 元素数据
    private ElementStat _elementStat;

    // 缩放比例
    public float BigImageScale;
    // 格子检测半径
    public float gridCheckRaius;
    // 提示阴影透明度
    public float shadowAlpha;
    // 阴影精灵
    public GameObject shadowSprite;
    // 叉叉精灵
    public GameObject redCrossSprite;
    // 是否在格子检测半径内
    private bool isInRange = false;
    // 临时阴影元素
    private GameObject tempShadowSprite;
    // 初始缩放比例
    private Vector2 StartScale;
    // 初始位置
    private Vector2 StartPos;
    // 棋盘
    private BoardCon TheBoard;
    // 是否正在被拖拽
    private bool isDraging;
    // 和每个格子的距离
    private float[,] distanceToGrids;
    // 当前在范围内的格子数据
    private GridStat currentInRangeGridStat;
    // 当前存在的红色叉叉
    private List<GameObject> curRedCrosses;

    private void Start()
    {
        this.Init();
    }

    private void Init()
    {
        this.curQuestStat = QuestHandler.inst.QuestStat;
        this.StartScale = this.transform.localScale;
        this.StartPos = this.transform.position;
        this.TheBoard = GameObject.Find("Grids").GetComponent<BoardCon>();
        this.isDraging = false;
        this.distanceToGrids = new float[this.TheBoard.line, this.TheBoard.column];
        this.curRedCrosses = new List<GameObject>();
        this.GetComponent<Animator>().runtimeAnimatorController = this._elementStat.elementAnimator;
    }

    private void Update()
    {
        //Check the relative position with the board
        if (this.isDraging)
        {
            // TODO: Update中双循环遍历
            for (int i = 0; i < this.TheBoard.BoardStat.GetLength(0); i++)
            {
                for (int j = 0; j < this.TheBoard.BoardStat.GetLength(1); j++)
                {
                    float xMinus = transform.position.x - this.TheBoard.BoardStat[i, j].relativeWorldPos.x;
                    float yMinus = transform.position.y - this.TheBoard.BoardStat[i, j].relativeWorldPos.y;
                    this.distanceToGrids[i, j] = Mathf.Sqrt(xMinus * xMinus + yMinus * yMinus);
                    if (this.distanceToGrids[i, j] <= this.gridCheckRaius * this.TheBoard.transform.localScale.x &&
                        !this.TheBoard.BoardStat[i, j].isFilledWithSprite)
                    {
                        this.isInRange = true;
                        if (this.currentInRangeGridStat == null)
                        {
                            this.currentInRangeGridStat = new GridStat(this.TheBoard.BoardStat[i, j].relativeWorldPos, i, j);
                        }
                        if (!this.TheBoard.BoardStat[i, j].isShadowed)
                        {
                            // Debug.Log("Grid" + i + j + "in range");
                            GameObject shadowSprite = Instantiate(this.shadowSprite);
                            shadowSprite.GetComponent<Animator>().runtimeAnimatorController = this.GetComponent<Animator>().runtimeAnimatorController;
                            this.tempShadowSprite = shadowSprite;
                            shadowSprite.transform.localScale = this.StartScale;
                            shadowSprite.transform.position = this.TheBoard.BoardStat[i, j].relativeWorldPos;
                            shadowSprite.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, this.shadowAlpha);
                            this.TheBoard.BoardStat[i, j].isShadowed = true;

                            // 生成红色叉叉
                            this.generateRedCross(this.TheBoard.BoardStat[i, j]);
                        }
                    }
                    else
                    {
                        if (this.tempShadowSprite != null)
                        {
                            if (this.TheBoard.BoardStat[i, j].isShadowed)
                            {
                                this.destroyShadowSprite();
                                this.TheBoard.BoardStat[i, j].isShadowed = false;
                                this.isInRange = false;
                                this.currentInRangeGridStat = null;

                                // 销毁红色叉叉
                                this.destroyCurRedCrosses();
                            }
                        }
                    }
                }
            }
        }
    }

    // 销毁图标在格子里的提示影子
    private void destroyShadowSprite()
    {
        // TODO: Bug
        Destroy(this.tempShadowSprite.gameObject);
        this.tempShadowSprite = null;
    }

    // 销毁当前红色叉叉
    private void destroyCurRedCrosses()
    {
        for (int i = 0; i < this.curRedCrosses.Count; i++)
        {
            Destroy(this.curRedCrosses[i]);
        }
        this.curRedCrosses.RemoveRange(0, curRedCrosses.Count);
    }

    private void OnMouseOver()
    {
        if (!this._elementStat.isOnBoard && !EventSystem.current.IsPointerOverGameObject())
        {
            this.transform.localScale = this.BigImageScale * this.StartScale;
        }
    }

    private void OnMouseExit()
    {
        if (!this._elementStat.isOnBoard)
        {
            this.transform.localScale = this.StartScale;
        }
    }

    private void OnMouseDrag()
    {
        if (!this._elementStat.isOnBoard && !EventSystem.current.IsPointerOverGameObject())
        {
            this.isDraging = true;
            Vector2 tempVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = tempVec;
        }
    }

    private void OnMouseUp()
    {
        if (!this._elementStat.isOnBoard)
        {
            this.isDraging = false;
            if (this.isInRange && !this.TheBoard.BoardStat[this.currentInRangeGridStat.line, this.currentInRangeGridStat.column].isFilledWithSprite)
            {
                GridStat curGrid = this.TheBoard.BoardStat[this.currentInRangeGridStat.line, this.currentInRangeGridStat.column];
                this.destroyShadowSprite();
                this.destroyCurRedCrosses();
                this._elementStat.isOnBoard = true;
                transform.position = this.currentInRangeGridStat.relativeWorldPos;
                transform.localScale = this.StartScale;
                curGrid.isFilledWithSprite = true;
                curGrid.isShadowed = false;
                curGrid.spriteOnTheGrid = this.gameObject;
                curGrid.elementOnGrid = this._elementStat;
                this.eliminateSpritesAround(curGrid);
                this.currentInRangeGridStat = null;

                // 发送元素被放上棋盘事件
                GameEventsHandler.current.spritePutOnTheBoard(this.gameObject);
                GameEventsHandler.current.checkWinOrLose();
            }
            else
            {
                transform.position = this.StartPos;
            }
        }
    }

    // 设置为在棋盘上的状态
    public void setOnBoard()
    {
        this.isDraging = false;
        this._elementStat.isOnBoard = true;
    }

    // 设置在传送带上的状态
    public void setCanBeChosen()
    {
        this.isDraging = false;
        this._elementStat.isOnBoard = false;
    }

    // 检查周围克制元素并返回
    private List<List<int>> checkDominateSpritesAround(GridStat midGrid)
    {
        List<List<int>> coordinateIndex = new List<List<int>>();
        for (int i = -1; i < 2; i++)
        {
            int curLine = midGrid.line + i;
            if (curLine < 0 || curLine >= this.TheBoard.BoardStat.GetLength(0)) continue;
            for (int j = -1; j < 2; j++)
            {
                int curColumn = midGrid.column + j;
                if (i == 0 && j == 0) continue;
                if (curColumn < 0 || curColumn >= this.TheBoard.BoardStat.GetLength(1)) continue;
                if (this.TheBoard.BoardStat[curLine, curColumn].isFilledWithSprite)
                {
                    if (this._elementStat.loopNum == this.TheBoard.BoardStat[curLine, curColumn].elementOnGrid.loopNum)
                    {
                        if (this._elementStat.loopIndex == 0)
                        {
                            if (this.TheBoard.BoardStat[curLine, curColumn].elementOnGrid.loopIndex == this.curQuestStat.elementLoops[this._elementStat.loopNum].elementLoop.Count - 1)
                            {
                                List<int> coordinate = new List<int>();
                                coordinate.Add(curLine); coordinate.Add(curColumn);
                                coordinateIndex.Add(coordinate);
                            }
                        }
                        else
                        {
                            if (this.TheBoard.BoardStat[curLine, curColumn].elementOnGrid.loopIndex == this._elementStat.loopIndex - 1)
                            {
                                List<int> coordinate = new List<int>();
                                coordinate.Add(curLine); coordinate.Add(curColumn);
                                coordinateIndex.Add(coordinate);
                            }
                        }
                    }
                }
            }
        }

        return coordinateIndex;
    }

    // 消除周围8格克制符号
    private void eliminateSpritesAround(GridStat midGrid)
    {
        List<List<int>> coordinateIndex = this.checkDominateSpritesAround(midGrid);

        for (int i = 0; i < coordinateIndex.Count; i++)
        {
            int curLine = coordinateIndex[i][0];
            int curColumn = coordinateIndex[i][1];
            this.TheBoard.BoardStat[curLine, curColumn].isFilledWithSprite = false;
            this.TheBoard.BoardStat[curLine, curColumn].elementOnGrid = null;
            LeanTween.scale(this.TheBoard.BoardStat[curLine, curColumn].spriteOnTheGrid, new Vector3(0f, 0f, 0f), 0.5f).setOnComplete
                            (new System.Action(() =>
                            {
                                Destroy(this.TheBoard.BoardStat[curLine, curColumn].spriteOnTheGrid);
                                this.TheBoard.BoardStat[curLine, curColumn].spriteOnTheGrid = null;
                            }));
        }

    }

    // 给周边可消除的元素画红色叉叉
    private void generateRedCross(GridStat midGrid)
    {
        List<List<int>> coordinateIndex = this.checkDominateSpritesAround(midGrid);

        for (int i = 0; i < coordinateIndex.Count; i++)
        {
            int curLine = coordinateIndex[i][0];
            int curColumn = coordinateIndex[i][1];

            GameObject redCross = GameObject.Instantiate(this.redCrossSprite);
            redCross.transform.position = this.TheBoard.BoardStat[curLine, curColumn].relativeWorldPos;
            this.curRedCrosses.Add(redCross);
        }
        // Debug.Log(coordinateIndex.Count);
    }

    public Vector2 startPos
    {
        get { return this.StartPos; }
        set { this.StartPos = value; }
    }

    public ElementStat elementStat
    {
        get
        {
            if (this._elementStat == null)
            {
                Debug.Log("脚本中elementStat未初始化");
            }
            return this._elementStat;
        }
        set
        {
            this._elementStat = value;
        }
    }

    private void OnDestroy()
    {

    }

}