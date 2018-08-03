using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Node")]
public class Node : ScriptableObject
{
    public string nodeName;
    public List<string> keyStrings = new List<string>();
    [TextArea]
    public string location;
    [TextArea]
    public string desc;
    public Item[] usableItems;
    [TextArea]
    public string usedItemDesc;
    public bool activated = false;
}
