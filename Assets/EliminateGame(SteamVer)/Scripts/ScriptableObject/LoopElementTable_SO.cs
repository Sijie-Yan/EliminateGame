using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Table Data/LoopElementTable", fileName = "New Table")]
public class LoopElementTable_SO : ScriptableObject
{
    public List<ElementTable_SO> elementLoop;
}
