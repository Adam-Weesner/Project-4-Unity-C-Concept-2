using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    public Text displayText;
    public InputField inputField;
    private bool skip = false;
    private bool done = false;

    // Use this for initialization
    void Start()
    {
        GetComponent<FoodMeter>().isWaiting = true;
        StartCoroutine(TypeSentence("Beginning. Type \"tutorial\" to see the list of action verbs. Press space to skip dialog.\n------\n\nPress any key to start...\n"));
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && !inputField.isActiveAndEnabled)
            skip = true;

        if (Input.anyKeyDown && done)
        {
            GetComponent<StartGame>().enabled = false;
            GetComponent<Description>().enabled = true;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        inputField.enabled = false;
        inputField.placeholder.GetComponent<Text>().text = "...";

        // Loops through each character & displays it
        foreach (char letter in sentence.ToCharArray())
        {
            // Appends each letter to the string
            displayText.text += letter;
            if (!skip)
                yield return new WaitForSeconds(0.001f);
        }
        done = true;
        yield return null;
    }
}
