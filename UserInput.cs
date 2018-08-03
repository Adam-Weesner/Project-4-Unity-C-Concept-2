using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UserInput : MonoBehaviour {

    public InputField inputField;
    public Text displayText;
    public GameObject scrollBar;
    public Room currentRoom;
    [HideInInspector] public string[] userInputPublic = null;
    [HideInInspector] public string userInputAll = null;
    private bool notFound;
    [HideInInspector] public bool isWaiting = false;
    [HideInInspector] public bool puzzleActive = false;

    private List<string> go;
    string[] goAr = new string[] { "go", "move" };

    public List<string> check;
    string[] checkAr = new string[] { "check", "examine", "look" };

    private List<string> take;
    string[] takeAr = new string[] { "take" };

    private List<string> use;
    string[] useAr = new string[] { "use" };

    private List<string> eat;
    string[] eatAr = new string[] { "eat" };

    void Start()
    {
        go = new List<string>();
        AddNouns(go, goAr);

        check = new List<string>();
        AddNouns(check, checkAr);

        take = new List<string>();
        AddNouns(take, takeAr);

        use = new List<string>();
        AddNouns(use, useAr);

        eat = new List<string>();
        AddNouns(eat, eatAr);

        inputField.ActivateInputField();
    }

    void AcceptStringInput(string userInput)
    {
        if (inputField.isActiveAndEnabled && !puzzleActive)
        {
            notFound = true;

            userInput = userInput.ToLower();
            char[] delimterCharacters = { ' ' };
            string[] seperatedInputWords = userInput.Split(delimterCharacters);

            userInputPublic = seperatedInputWords;
            userInputAll = userInput;

            if (seperatedInputWords.Length > 1)
            {
                // GO COMMAND
                if (go.Contains(seperatedInputWords[0]))
                {
                    for (int i = 0; i < currentRoom.exits.Count; i++)
                    {
                        for (int j = 0; j < currentRoom.exits[i].keyStrings.Count; j++)
                        {
                            if (seperatedInputWords[1] == currentRoom.exits[i].keyStrings[j].ToLower())
                            {
                                isWaiting = false;
                                notFound = false;
                                currentRoom = currentRoom.exits[i].valueRoom;
                                StartCoroutine(Wait(">>" + userInput));
                                break;
                            }
                        }
                        if (!isWaiting && !notFound)
                            break;
                    }
                    if (notFound)
                    {
                        StartCoroutine(TypeSentence(">>" + userInput + "\n\nThere is no exit named " + seperatedInputWords[1] + "."));
                    }
                }

                // CHECK COMMAND
                else if (check.Contains(seperatedInputWords[0]))
                {
                    // Check inventory
                    if (seperatedInputWords[1] == "inventory")
                    {
                        if (GetComponent<Inventory>().inventory.Count > 0)
                        {
                            string itemsStr = "";
                            for (int i = 0; i < GetComponent<Inventory>().inventory.Count; i++)
                            {
                                itemsStr += GetComponent<Inventory>().inventory[i].itemName;
                                if (i + 1 < GetComponent<Inventory>().inventory.Count)
                                    itemsStr += "\n";
                            }
                            StartCoroutine(TypeSentence(">>" + userInput + "\n\nYou have these items:\n" + itemsStr));
                        }
                        else
                        {
                            StartCoroutine(TypeSentence(">>" + userInput + "\n\nYou have nothing in your inventory."));
                        }
                    // Check items and nodes
                    } else
                    {
                        // Check items
                        for (int i = 0; i < currentRoom.items.Count; i++)
                        {
                            for (int j = 0; j < currentRoom.items[i].keyStrings.Length; j++)
                            {
                                if (seperatedInputWords[1] == currentRoom.items[i].keyStrings[j].ToLower())
                                {
                                    StartCoroutine(TypeSentence(">>" + userInput + "\n\n" + currentRoom.items[i].desc));
                                    notFound = false;
                                }
                            }
                        }

                        // Check nodes
                        for (int i = 0; i < currentRoom.nodes.Count; i++)
                        {
                            for (int j = 0; j < currentRoom.nodes[i].keyStrings.Count; j++)
                            {
                                if (seperatedInputWords[1] == currentRoom.nodes[i].keyStrings[j].ToLower())
                                {
                                    if (currentRoom.nodes[i].desc != "")
                                    {
                                        StartCoroutine(TypeSentence(">>" + userInput + "\n\n" + currentRoom.nodes[i].desc));
                                    } else
                                    {
                                        puzzleActive = true;
                                        StartCoroutine(TypeSentence(">>" + userInput));
                                    }
                                    notFound = false;
                                }
                            }
                        }

                        // Check inventory
                        for (int i = 0; i < GetComponent<Inventory>().inventory.Count; i++)
                        {
                            if (seperatedInputWords[1] == GetComponent<Inventory>().inventory[i].itemName.ToLower())
                            {
                                StartCoroutine(TypeSentence(">>" + userInput + "\n\n" + GetComponent<Inventory>().inventory[i].desc));
                                notFound = false;
                            }
                        }

                        // Check room name
                        for (int i = 0; i < currentRoom.references.Count; i++)
                        {
                            if (seperatedInputWords[1] == currentRoom.references[i].ToLower())
                            {
                                GetComponent<Description>().PrintDesc(">>" + userInput);
                                notFound = false;
                            }
                        }

                        if (notFound)
                        {
                            StartCoroutine(TypeSentence(">>" + userInput + "\n\nYou cannot find the " + seperatedInputWords[1] + "."));
                        }
                    }
                }

                // TAKE COMMAND
                else if (take.Contains(seperatedInputWords[0]))
                {
                    for (int i = 0; i < currentRoom.items.Count; i++)
                    {
                        if (seperatedInputWords[1] == currentRoom.items[i].itemName.ToLower())
                        {
                            StartCoroutine(TypeSentence(">>" + userInput + "\n\nYou take the " + currentRoom.items[i].itemName + "."));
                            GetComponent<Inventory>().inventory.Add(currentRoom.items[i]);

                            if (currentRoom.items[i].node != null)
                            {
                                currentRoom.nodes.Remove((Node)currentRoom.items[i].node);
                            }

                            currentRoom.items[i].isObtained = true;
                            currentRoom.items.Remove(currentRoom.items[i]);
                            notFound = false;
                        }
                    }
                    if (notFound)
                    {
                        StartCoroutine(TypeSentence(">>" + userInput + "\n\nYou cannot take the " + seperatedInputWords[1] + "."));
                    }
                }

                // USE COMMAND
                else if (use.Contains(seperatedInputWords[0]))
                {
                    if (seperatedInputWords.Length == 4 && (seperatedInputWords[2] == "on" || seperatedInputWords[2] == "with"))
                    {
                        for (int i = 0; i < GetComponent<Inventory>().inventory.Count; i++)
                        {
                            if (seperatedInputWords[1] == GetComponent<Inventory>().inventory[i].itemName.ToLower())
                            {
                                notFound = false;
                                bool notFound2 = true;

                                for (int j = 0; j < currentRoom.nodes.Count; j++)
                                {
                                    for (int b = 0; b < currentRoom.nodes[j].keyStrings.Count; b++)
                                    {
                                        if (seperatedInputWords[3] == currentRoom.nodes[j].keyStrings[b].ToLower())
                                        {
                                            notFound2 = false;
                                            bool notFound3 = true;
                                            string strTemp = ">>" + userInput + "\n\nYou used the " + seperatedInputWords[1] + " on the " + seperatedInputWords[3] + ".\n\n";

                                            for (int z = 0; z < currentRoom.nodes[j].usableItems.Length; z++)
                                            {
                                                if (GetComponent<Inventory>().inventory[i] == currentRoom.nodes[j].usableItems[z])
                                                {
                                                    notFound3 = false;
                                                    StartCoroutine(TypeSentence(">>" + userInput + "\n\n" + currentRoom.nodes[j].usedItemDesc));
                                                    currentRoom.nodes[j].activated = true;
                                                }
                                            }
                                            if (notFound3)
                                                StartCoroutine(TypeSentence(">>" + userInput + "\n\nYou cannot use the " + seperatedInputWords[1] + " on the " + seperatedInputWords[3] + "."));
                                        }
                                    }
                                    
                                }
                                if (notFound2)
                                    StartCoroutine(TypeSentence(">>" + userInput + "\n\nThe " + seperatedInputWords[3] + " doesn't exist."));
                            }
                        }
                        if (notFound)
                            StartCoroutine(TypeSentence(">>" + userInput + "\n\nYou don't have the " + seperatedInputWords[1] + "."));
                    }
                    else
                    {
                        StartCoroutine(TypeSentence(">>" + userInput + "\n\nYou must use an item on something. (ex. \"use key on door\")"));
                    }
                }

                // EAT COMMAND
                else if (eat.Contains(seperatedInputWords[0]))
                {
                    for (int i = 0; i < GetComponent<Inventory>().inventory.Count; i++)
                    {
                        if (seperatedInputWords[1] == GetComponent<Inventory>().inventory[i].itemName.ToLower())
                        {
                            notFound = false;
                            if (seperatedInputWords[1] == "banana")
                            {
                                GetComponent<FoodMeter>().slider.GetComponent<Slider>().value += 10;
                                GetComponent<Inventory>().inventory.Remove(GetComponent<Inventory>().inventory[i]);
                                StartCoroutine(TypeSentence(">>" + userInput + "\n\nYou eat the banana. ... Tasty."));
                            }
                            else if (seperatedInputWords[1] == "apple")
                            {
                                GetComponent<FoodMeter>().slider.GetComponent<Slider>().value += 15;
                                GetComponent<Inventory>().inventory.Remove(GetComponent<Inventory>().inventory[i]);
                                StartCoroutine(TypeSentence(">>" + userInput + "\n\nYou eat the apple. Nice and ripe."));
                            }
                            else if (seperatedInputWords[1] == "coconut")
                            {
                                GetComponent<FoodMeter>().slider.GetComponent<Slider>().value += 20;
                                GetComponent<Inventory>().inventory.Remove(GetComponent<Inventory>().inventory[i]);
                                StartCoroutine(TypeSentence(">>" + userInput + "\n\nYou eat the coconut. You feel replenished."));
                            }
                            else
                            {
                                StartCoroutine(TypeSentence(">>" + userInput + "\n\nYou attempt to eat the " + seperatedInputWords[1] + ", but something compels you not to."));
                            }
                        }
                    }
                    if (notFound)
                    {
                        StartCoroutine(TypeSentence(">>" + userInput + "\n\nYou don't have the " + seperatedInputWords[1] + " in your inventory."));
                    }
                }

                // UNRECOGNIZED COMMAND
                else
                {
                    StartCoroutine(TypeSentence(">>" + userInput + "\n\nThat command is unrecognized."));
                }
            }

            // TUTORIAL
            else if (seperatedInputWords[0] == "tutorial")
            {
                StartCoroutine(TypeSentence(">>" + userInput + "\n\nTUTORIAL:\nAction verbs:\n     go = \"go <place>\", \"move <place>\"\n     check = \"check <object>\", \"examine <object>\"\n     take = \"take <item>\"\n     use = \"use <item> on <object>\"\nThere is a hunger meter at the bottom. Once it depletes, you will die.\nYou can check your inventory via \"check inventory\".\nPress space to skip dialog."));
            }
            else
            {
                if (userInput != "")
                {
                    StartCoroutine(TypeSentence(">>" + userInput + "\n\nType a command followed by a noun."));
                }
            }

            inputField.text = null;
            inputField.ActivateInputField();
        }
    }

    public IEnumerator TypeSentence(string sentence)
    {
        inputField.enabled = false;
        GetComponent<FoodMeter>().isWaiting = true;
        inputField.placeholder.GetComponent<Text>().text = "...";
        scrollBar.GetComponent<Scrollbar>().value = 0;

        displayText.text += "\n";

        // Loops through each character & displays it
        foreach (char letter in sentence.ToCharArray())
        {
            // Appends each letter to the string
            displayText.text += letter;
            if (!GetComponent<Description>().skip)
                yield return new WaitForSeconds(0.0001f);
        }

        displayText.text += "\n";
        
        inputField.enabled = true;
        GetComponent<FoodMeter>().isWaiting = false;
        inputField.ActivateInputField();
        if (!puzzleActive)
            inputField.placeholder.GetComponent<Text>().text = "Enter Text...";
        isWaiting = true;
        GetComponent<Description>().skip = false;
        yield return null;
    }

    IEnumerator Wait(string sentence)
    {
        inputField.enabled = false;
        GetComponent<FoodMeter>().isWaiting = true;
        inputField.placeholder.GetComponent<Text>().text = "...";
        scrollBar.GetComponent<Scrollbar>().value = 0;

        displayText.text += "\n";

        // Loops through each character & displays it
        foreach (char letter in sentence.ToCharArray())
        {
            // Appends each letter to the string
            displayText.text += letter;
            if (!GetComponent<Description>().skip)
                yield return new WaitForSeconds(0.0001f);
        }

        displayText.text += "\n";
        isWaiting = true;
        GetComponent<FoodMeter>().isWaiting = false;

        yield return new WaitUntil(() => isWaiting);
        GetComponent<Description>().PrintNameDesc(currentRoom);
        yield return null;
    }

    void Awake()
    {
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    void AddNouns(List<string> list, string[] str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            list.Add(str[i]);
        }
    }
}
