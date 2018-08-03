using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour {

    public Text displayText;
    public InputField inputField;
    public GameObject scrollBar;
    [HideInInspector] public Room currentRoom;
    private bool isWaiting = false;
    private bool temp = false;
    [HideInInspector] public bool skip = false;

    // Use this for initialization
    void Start() {
        GetComponent<FoodMeter>().isWaiting = true;
        temp = false;
        currentRoom = GetComponent<UserInput>().currentRoom;
        PrintNameDesc(currentRoom);
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && !inputField.isActiveAndEnabled)
            skip = true;
    }

    public void PrintNameDesc(Room room)
    {
        currentRoom = room;
        temp = false;
        GetComponent<FoodMeter>().isWaiting = true;
        string dashes = "";

        for (int i = 0; i < currentRoom.roomName.Length+1; i++)
        {
            dashes += "-";
        }

        StartCoroutine(TypeSentence("\n" + dashes + "\n" + currentRoom.roomName + "\n" + dashes + "\n\n" + currentRoom.desc + " "));
        StartCoroutine(PrintItems());
    }

    public void PrintDesc(string userInput)
    {
        temp = false;
        StartCoroutine(TypeSentence("\n" + userInput + "\n\n" + currentRoom.desc + " "));
        StartCoroutine(PrintItems());
    }

    IEnumerator TypeSentence(string sentence)
    {
        isWaiting = false;
        inputField.enabled = false;
        inputField.placeholder.GetComponent<Text>().text = "...";
        scrollBar.GetComponent<Scrollbar>().value = 0;

        // Loops through each character & displays it
        foreach (char letter in sentence.ToCharArray())
        {
            // Appends each letter to the string
            displayText.text += letter;
            if (!skip)
                yield return new WaitForSeconds(0.001f);
        }

        isWaiting = true;
        inputField.enabled = true;
        GetComponent<FoodMeter>().isWaiting = false;
        inputField.ActivateInputField();
        if (temp)
        {
            inputField.placeholder.GetComponent<Text>().text = "Enter Text...";
            skip = false;
        }
        yield return null;
    }

    IEnumerator PrintItems()
    {
        yield return new WaitWhile(() => !isWaiting);

        temp = true;
        GetComponent<FoodMeter>().isWaiting = true;
        string output = ""; 

        for (int i = 0; i < currentRoom.nodes.Count; i++)
            output += currentRoom.nodes[i].location + " ";

        for (int i = 0; i < currentRoom.items.Count; i++)
        {
            output += currentRoom.items[i].location;
            if (currentRoom.items[i].location != "")
                output += " ";
        }

        output += currentRoom.descAfter;
        output += "\n";
        StartCoroutine(TypeSentence(output));
    }
}
