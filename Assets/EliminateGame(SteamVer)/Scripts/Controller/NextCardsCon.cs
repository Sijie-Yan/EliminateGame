using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCardsCon : MonoBehaviour
{

    // 当前关卡数据
    private QuestStat curQuestStat;

    // 克隆体
    public GameObject PrefabSprite;
    // 泡泡
    public GameObject Bubble;
    // 传送带上的元素数组
    private List<GameObject> NextSpritesList;
    // 传送带上元素个数
    public int SpritesCanBeChosen;
    // 传送带高度
    private float height;

    void Awake()
    {
        this.NextSpritesList = new List<GameObject>();
    }

    void Start()
    {
        // 注册事件：元素被放上棋盘
        GameEventsHandler.current.onSpritePutOnTheBoard += this.addNewSpriteFromTheBubble;

        this.curQuestStat = QuestHandler.inst.QuestStat;
        this.height = this.GetComponent<Renderer>().bounds.size.y;
        this.generateRandomSpritesStart();
    }

    // 游戏开始时生成随机元素在传送带上
    private void generateRandomSpritesStart()
    {
        for (int i = 0; i < this.SpritesCanBeChosen; i++)
        {
            GameObject tempSprite = Instantiate(this.PrefabSprite);
            this.setNextSpritesPos(tempSprite, i);

            int randomLoop = Random.Range(0, this.curQuestStat.elementLoops.Count);
            int randomNum = Random.Range(0, this.curQuestStat.elementLoops[randomLoop].elementLoop.Count);
            tempSprite.GetComponent<DragableItemCon>().elementStat = new ElementStat(this.curQuestStat.elementLoops[randomLoop].elementLoop[randomNum]);
            ElementHandler.inst.elementsOnTape.Add(tempSprite.GetComponent<DragableItemCon>().elementStat);

            this.NextSpritesList.Add(tempSprite);
        }
    }

    // 放一个元素到棋盘上后调整传送带上元素位置并且把泡泡中的元素添加进来
    private void addNewSpriteFromTheBubble(GameObject chosenSprite)
    {
        this.NextSpritesList.Remove(chosenSprite);
        GameObject currentSpriteInBubble = this.Bubble.GetComponent<BubbleCon>().CurrentSpriteInBubble;
        if (currentSpriteInBubble != null)
        {
            this.NextSpritesList.Add(currentSpriteInBubble);
        }

        for (int i = 0; i < this.NextSpritesList.Count; i++)
        {
            this.setNextSpritesPos(this.NextSpritesList[i], i);
        }

        GameEventsHandler.current.generateNewSpriteInBubble();
    }

    // 设置传送带上元素坐标
    private void setNextSpritesPos(GameObject spritePosToBeSet, int i)
    {
        LeanTween.move(spritePosToBeSet, new Vector2(transform.position.x, transform.position.y + this.height / 2 - (this.height / (SpritesCanBeChosen + 1)) * (i + 1)), 0.3f).
        setOnComplete(new System.Action(() =>
        {
            spritePosToBeSet.GetComponent<DragableItemCon>().startPos = spritePosToBeSet.transform.position;
        }));
    }

    private void OnDestroy()
    {
        GameEventsHandler.current.onSpritePutOnTheBoard -= this.addNewSpriteFromTheBubble;
    }
}
