using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Table Data/QuestRandomNextRatio", fileName = "New Table")]
public class QuestRandomNextRatio_SO : ScriptableObject
{
    public List<Enums.QuestType> QuestType;
    public List<int> QuestTypeRatio;
}