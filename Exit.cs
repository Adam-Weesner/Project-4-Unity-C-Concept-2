using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Exit {

    public Room valueRoom;
    public List<string> keyStrings = new List<string>();

    public Exit(Room value, List<string> keys)
    {
        valueRoom = value;
        keyStrings = keys;
    }
}
