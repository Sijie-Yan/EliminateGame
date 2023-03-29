using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Table Data/QuestTable", fileName = "New Table")]
public class QuestTable_SO : ScriptableObject
{
    [Header("Data Info")]
    public int QuestFloor;
    public int QuestStartSpriteNum;
    public int QuestCombatSpriteNum;
    public int NextQuestChooseNum;
    public List<QuestRandomNextRatio_SO> NextRatioArray;
    public int GridLine;
    public int GridColumn;
    public int QuestStartEmptyGridNum;
    public List<LoopElementTable_SO> QuestElementLoops;
}
