using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour {

    public InputField inputField;
    public Text displayText;
    public GameObject scrollBar;
    public GameObject controller;
    [HideInInspector] public Room currentRoom;

	void Update () {
        currentRoom = controller.GetComponent<UserInput>().currentRoom;
	}
}
