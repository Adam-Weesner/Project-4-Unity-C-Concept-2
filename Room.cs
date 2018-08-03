using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/Room")]
public class Room : ScriptableObject {

    public string roomName;
    [TextArea]
    public string desc;
    [TextArea]
    public string descAfter;
    public List<string> references = new List<string>();
    public List<Exit> exits = new List<Exit>();
    public List<Node> nodes = new List<Node>();
    public List<Item> items = new List<Item>();
}
