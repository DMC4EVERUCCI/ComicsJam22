using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CanvasController : MonoBehaviour
{
    GameObject TimeManager;
    GameObject LevelManager;

    SpriteRenderer Mappa;
    SpriteRenderer BGRPG1;
    SpriteRenderer BGRPG2;

    GameObject DiagContainer;
    GameObject ButtonContainer;

    [SerializeField] SpriteRenderer EscIcon;
    [SerializeField] SpriteRenderer PG1;
    [SerializeField] SpriteRenderer PG2;
    [SerializeField] SpriteRenderer MapIcon1;
    [SerializeField] SpriteRenderer MapIcon2;

    TextMeshProUGUI NameBox;
    TextMeshProUGUI Dialogue;
    TextMeshProUGUI Choice1;
    TextMeshProUGUI Choice2;


    // la fase del giorno
    string Phase;


    bool firstDiag;



    // You have to press space to advance through dialogue in these
    public enum MultiTextBox
    {
        DialogueBox
    }
    // All text boxes in the level, for convenient choosing
    public enum SingleTextBox
    {
        Name,
        DialogueBox,
        Choice1,
        Choice2,
        Choice3,
        Choice4
    }

    // Start is called before the first frame update
    void Start()
    {
        NameBox = GameObject.Find("NameBox").GetComponent<TextMeshProUGUI>();
        Dialogue = GameObject.Find("DiagText").GetComponent<TextMeshProUGUI>();
        Choice1 = GameObject.Find("Choice1").GetComponentInChildren<TextMeshProUGUI>();
        Choice2 = GameObject.Find("Choice2").GetComponentInChildren<TextMeshProUGUI>();

        TimeManager = GameObject.Find("Time Manager");
        LevelManager = GameObject.Find("LevelManager");
        DiagContainer = GameObject.Find("DiagContainer");
        ButtonContainer = GameObject.Find("Buttons");
        DiagContainer.SetActive(false);
        ButtonContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        

        if (TimeManager.GetComponent<TimeScript>().mattina == true)
        {
            Phase = "MATTINA";
        }
        else if (TimeManager.GetComponent<TimeScript>().pome == true)
        {
            Phase = "POME";
        }
        else if (TimeManager.GetComponent<TimeScript>().sera == true)
		{
            Phase = "SERA";
		}
        else if (TimeManager.GetComponent<TimeScript>().notte == true)
        {
            Phase = "NOTTE";
        }


        Mappa = GameObject.Find("MAPPA"+Phase).GetComponent<SpriteRenderer>();
        BGRPG1 = GameObject.Find("BGRPG1").GetComponent<SpriteRenderer>();
        BGRPG2 = GameObject.Find("BGRPG2").GetComponent<SpriteRenderer>();

    }


    public void HideAll()
	{
        Mappa.enabled = false;
        BGRPG1.enabled = false;
        BGRPG2.enabled = false;
        PG1.enabled = false;
        PG2.enabled = false;
        MapIcon1.enabled = false;
        MapIcon2.enabled = false;
        EscIcon.enabled = false;
    }

    public void ShowMap()
    {
        firstDiag = true;
        Mappa.enabled = true;
        BGRPG1.enabled = false;
        BGRPG2.enabled = false;
        PG1.enabled = false;
        PG2.enabled = false;
        MapIcon1.enabled = true;
        MapIcon2.enabled = true;
        EscIcon.enabled = false;
    }

    public void ShowPG1()
    {
        Mappa.enabled = false;
        BGRPG1.enabled = true;
        BGRPG2.enabled = false;
        PG1.enabled = true;
        PG2.enabled = false;
        MapIcon1.enabled = false;
        MapIcon2.enabled = false;
        EscIcon.enabled = true;
    }

    public void ShowPG2()
    {
        Mappa.enabled = false;
        BGRPG1.enabled = false;
        BGRPG2.enabled = true;
        PG1.enabled = false;
        PG2.enabled = true;
        MapIcon1.enabled = false;
        MapIcon2.enabled = false;
        EscIcon.enabled = true;
    }

    public void ShowChoices()
	{
        ButtonContainer.SetActive(true);
	}

    public void HideChoices()
    {
        ButtonContainer.SetActive(false);
    }

    public void StartDialogue()
	{
        //DiagContainer.SetActive(true);
        LevelManager.GetComponent<LevelManager>().Dialogue();
        if (firstDiag == true)
		{
            TimeManager.GetComponent<TimeScript>().AdvanceTime(1);
            firstDiag = false;
        }
        

	}

    public void EndDialogue()
	{
        LevelManager.GetComponent<LevelManager>().StopAllCoroutines();
        DiagContainer.SetActive(false);
    }

    // Function to be called to print text to the battle log or to the enemy dialogue (choose via the MultuiTextBox enum)
    // This is an IEnumerator to allow a coroutine in LevelManager to wait for the completion of the dialogue to advance
    public IEnumerator printDialogue(List<string> text, MultiTextBox box)
    {
        yield return null;
        if (box == MultiTextBox.DialogueBox) DiagContainer.SetActive(true);
        foreach (string l in text)
        {
            switch (box)
            {
                case MultiTextBox.DialogueBox:
                    Dialogue.text = "\"" + l + "\"";
                    break;
            }
            // Wait until space press, then wait a frame to make the new text appear
            while (!Input.GetKeyDown(KeyCode.Space)) yield return null;
            yield return null;
        }
        yield return null;
    }

    // Print a single message to a single text box. The player won't need to press spacebar to advance through
    public void printToTextBox(string text, SingleTextBox box)
    {
        switch (box)
        {
            case SingleTextBox.Name:
                NameBox.text = text;
                break;
            case SingleTextBox.Choice1:
                Choice1.text = text;
                break;
            case SingleTextBox.Choice2:
                Choice2.text = text;
                break;
            case SingleTextBox.DialogueBox:
                Dialogue.text = "\"" + text + "\"";
                break;
        }
    }

}
