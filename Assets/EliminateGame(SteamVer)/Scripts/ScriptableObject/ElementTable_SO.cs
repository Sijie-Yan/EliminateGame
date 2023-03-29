using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Table Data/ElementTable", fileName = "New Table")]
public class ElementTable_SO : ScriptableObject
{
    public string elementName;
    public RuntimeAnimatorController elementAnimator;
    
    [Header("循环下标")]
    public int loopNum;
    [Header("循环内部下标")]
    public int loopIndex;
}
