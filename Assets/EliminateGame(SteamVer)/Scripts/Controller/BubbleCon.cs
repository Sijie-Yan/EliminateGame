using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BubbleCon : MonoBehaviour
{
    // 当前关卡数据
    private QuestStat curQuestStat;
    // 克隆体
    public GameObject PrefabSprite;
    // 距离中心点的y轴距离（需要乘以缩放）
    public float DistanceToMid;
    // 当前在泡泡里的元素
    private GameObject currentSpriteInBubble;
    // 当前泡泡里是否有元素
    private bool _isHaveSpriteInBubble;

    void Start()
    {
        // 注册事件：元素被放上棋盘
        GameEventsHandler.current.onGenerateNewSpriteInBubble += this.spritePutOnTheBoard;

        this.curQuestStat = QuestHandler.inst.QuestStat;
        this.generateRandomSprite();
    }

    // 生成新的随机元素
    private void generateRandomSprite()
    {
        if (QuestHandler.inst.QuestStat.curLeftSpriteNum <= 0)
        {
            this.currentSpriteInBubble = null;
            return;
        }
        GameObject bubbleSprite = Instantiate(this.PrefabSprite);
        bubbleSprite.transform.position = new Vector2(transform.position.x, transform.position.y + this.DistanceToMid * transform.localScale.y);

        int randomLoop = Random.Range(0, this.curQuestStat.elementLoops.Count);
        int randomNum = Random.Range(0, this.curQuestStat.elementLoops[randomLoop].elementLoop.Count);
        bubbleSprite.GetComponent<DragableItemCon>().elementStat = new ElementStat(this.curQuestStat.elementLoops[randomLoop].elementLoop[randomNum]);
        bubbleSprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

        bubbleSprite.GetComponent<DragableItemCon>().setOnBoard();

        this.currentSpriteInBubble = bubbleSprite;

        LeanTween.alpha(bubbleSprite, 1f, 0.3f);

        QuestHandler.inst.QuestStat.curLeftSpriteNum--;

        GameEventsHandler.current.changeLeftSpriteNum();
    }

    // 元素被放上棋盘
    private void spritePutOnTheBoard()
    {
        if (this.currentSpriteInBubble != null)
            this.currentSpriteInBubble.GetComponent<DragableItemCon>().setCanBeChosen();
        this.generateRandomSprite();
    }

    public GameObject CurrentSpriteInBubble
    {
        get
        {
            return this.currentSpriteInBubble;
        }
    }

    public bool isHaveSpriteInBubble
    {
        get
        {
            return this._isHaveSpriteInBubble;
        }
        set
        {
            this._isHaveSpriteInBubble = value;
        }
    }

    private void OnDestroy()
    {
        GameEventsHandler.current.onGenerateNewSpriteInBubble -= this.spritePutOnTheBoard;
    }
}
