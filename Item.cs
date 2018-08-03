using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string[] keyStrings;
    [TextArea]
    public string location;
    [TextArea]
    public string desc;
    [HideInInspector] public bool isObtained = false;
    public ScriptableObject node = null;
}
