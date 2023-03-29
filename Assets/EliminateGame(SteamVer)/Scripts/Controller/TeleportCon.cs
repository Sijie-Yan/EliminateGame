using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCon : MonoBehaviour
{
    // 十位
    public GameObject FirstNum;
    // 个位
    public GameObject SecondNum;
    private void Start()
    {
        GameEventsHandler.current.onChangeLeftSpriteNum += this.changeNumPics;
        this.changeNumPics();
    }

    // 换数字
    private void changeNumPics()
    {
        if (QuestHandler.inst.QuestStat.curLeftSpriteNum <= 9)
        {
            this.SecondNum.GetComponent<SpriteRenderer>().sprite = null;
            this.FirstNum.GetComponent<SpriteRenderer>().sprite = ImageResources.numberPics[QuestHandler.inst.QuestStat.curLeftSpriteNum];
        }
        else
        {
            this.FirstNum.GetComponent<SpriteRenderer>().sprite = ImageResources.numberPics[QuestHandler.inst.QuestStat.curLeftSpriteNum / 10];
            this.SecondNum.GetComponent<SpriteRenderer>().sprite = ImageResources.numberPics[QuestHandler.inst.QuestStat.curLeftSpriteNum % 10];
        }
    }

    private void OnDestroy()
    {
        GameEventsHandler.current.onChangeLeftSpriteNum -= this.changeNumPics;
    }
}