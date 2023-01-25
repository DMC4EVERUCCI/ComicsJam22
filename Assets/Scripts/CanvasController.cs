using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using FMODUnity;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    StudioEventEmitter OST;

    GameObject TimeManager;
    GameObject LevelManager;
    SpigaManager SpigaManager;

    bool firstBoot;

    SpriteRenderer Mappa;
    SpriteRenderer BGRPG1;
    SpriteRenderer BGRPG2;
    SpriteRenderer BGRPG3;
    SpriteRenderer BGRPG4;
    SpriteRenderer BGRPG5;

    GameObject DiagContainer;
    GameObject ButtonContainer;

    [SerializeField] SpriteRenderer EscIcon;
    //PG1
    [Header("PG1")]
    GameObject PG1Container;
    [SerializeField] SpriteRenderer PG1;
    [SerializeField] SpriteRenderer PG1F;
    [SerializeField] SpriteRenderer PG1P;
    [SerializeField] SpriteRenderer PG1absent;
    //PG2
    [Header("PG2")]
    GameObject PG2Container;
    [SerializeField] SpriteRenderer PG2;
    [SerializeField] SpriteRenderer PG2F;
    [SerializeField] SpriteRenderer PG2P;
    [SerializeField] SpriteRenderer PG2absent;
    //PG3
    [Header("PG3")]
    GameObject PG3Container;
    [SerializeField] SpriteRenderer PG3N;
    [SerializeField] SpriteRenderer PG3FN;
    [SerializeField] SpriteRenderer PG3FA;
    [SerializeField] SpriteRenderer PG3AF;
    [SerializeField] SpriteRenderer PG3absent;
    //PG4
    [Header("PG4")]
    GameObject PG4Container;
    [SerializeField] SpriteRenderer PG4;
    [SerializeField] SpriteRenderer PG4P;
    [SerializeField] SpriteRenderer PG4absent;
    //PG5
    [Header("PG5")]
    GameObject PG5Container;
    [SerializeField] SpriteRenderer PG5;
    [SerializeField] SpriteRenderer PG5P;
    [SerializeField] SpriteRenderer PG5absent;

    [SerializeField] GameObject ICONS;
    [SerializeField] SpriteRenderer MapIcon1;
    [SerializeField] SpriteRenderer MapIcon2;
    [SerializeField] SpriteRenderer MapIcon3;
    [SerializeField] SpriteRenderer MapIcon4;
    [SerializeField] SpriteRenderer MapIcon5;

    TextMeshProUGUI NameBox;
    TextMeshProUGUI Dialogue;

    GameObject ButtonChoice1;
    GameObject ButtonChoice2;
    GameObject ButtonChoice3;
    GameObject ButtonChoice4;

    GameObject ConfidoSpiga;
    GameObject Zaino;

    TextMeshProUGUI Choice1;
    TextMeshProUGUI Choice2;
    TextMeshProUGUI Choice3;
    TextMeshProUGUI Choice4;

    public int currentPG;

    // la fase del giorno
    public string Phase;


    bool timeToTime;
    bool firstDiag;


    [SerializeField] GameObject LEADERPG;
    [SerializeField] GameObject LEADERBGR;
    [SerializeField] TextMeshProUGUI DAY;


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
        OST = GetComponent<StudioEventEmitter>();
        OST.Play();

        NameBox = GameObject.Find("NameBox").GetComponent<TextMeshProUGUI>();
        Dialogue = GameObject.Find("DiagText").GetComponent<TextMeshProUGUI>();

        ButtonChoice1 = GameObject.Find("Choice1");
        ButtonChoice2 = GameObject.Find("Choice2");
        ButtonChoice3 = GameObject.Find("Choice3");
        ButtonChoice4 = GameObject.Find("Choice4");
        Choice1 = GameObject.Find("Choice1").GetComponentInChildren<TextMeshProUGUI>();
        Choice2 = GameObject.Find("Choice2").GetComponentInChildren<TextMeshProUGUI>();
        Choice3 = GameObject.Find("Choice3").GetComponentInChildren<TextMeshProUGUI>();
        Choice4 = GameObject.Find("Choice4").GetComponentInChildren<TextMeshProUGUI>();

        PG1Container = GameObject.Find("PG1Container");
        PG2Container = GameObject.Find("PG2Container");
        PG3Container = GameObject.Find("PG3Container");
        PG4Container = GameObject.Find("PG4Container");
        PG5Container = GameObject.Find("PG5Container");

        ConfidoSpiga = GameObject.Find("Confido");
        Zaino = GameObject.Find("ZAINO");

        SpigaManager = GameObject.Find("SpigaManager").GetComponent<SpigaManager>();
        TimeManager = GameObject.Find("Time Manager");
        LevelManager = GameObject.Find("LevelManager");
        DiagContainer = GameObject.Find("DiagContainer");
        ButtonContainer = GameObject.Find("Buttons");
        DiagContainer.SetActive(false);
        ButtonContainer.SetActive(false);

        firstDiag = true;

        Phase = "NOTTE";

        BGRPG1 = GameObject.Find("BGRPG1" + Phase).GetComponent<SpriteRenderer>();
        BGRPG2 = GameObject.Find("BGRPG2" + Phase).GetComponent<SpriteRenderer>();
        BGRPG3 = GameObject.Find("BGRPG3" + Phase).GetComponent<SpriteRenderer>();
        BGRPG4 = GameObject.Find("BGRPG4" + Phase).GetComponent<SpriteRenderer>();
        BGRPG5 = GameObject.Find("BGRPG5" + Phase).GetComponent<SpriteRenderer>();
        Mappa = GameObject.Find("MAPPA" + Phase).GetComponent<SpriteRenderer>();
    }

	void PhaseCheckBGR()
	{

        DAY.text = "Day " + TimeManager.GetComponent<TimeScript>().day;

        if (TimeManager.GetComponent<TimeScript>().day > 3)
		{
            EndingTime(TimeManager.GetComponent<TimeScript>().currentWins);
        }

        BGRPG1 = GameObject.Find("BGRPG1" + Phase).GetComponent<SpriteRenderer>();
        BGRPG2 = GameObject.Find("BGRPG2" + Phase).GetComponent<SpriteRenderer>();
        BGRPG3 = GameObject.Find("BGRPG3" + Phase).GetComponent<SpriteRenderer>();
        BGRPG4 = GameObject.Find("BGRPG4" + Phase).GetComponent<SpriteRenderer>();
        BGRPG5 = GameObject.Find("BGRPG5" + Phase).GetComponent<SpriteRenderer>();
    }



	// Update is called once per frame
	void Update()
    {

        if (firstBoot == false)
        {

            PG1Container.SetActive(false);
            PG2Container.SetActive(false);
            PG3Container.SetActive(false);
            PG4Container.SetActive(false);
            PG5Container.SetActive(false);
            ICONS.SetActive(false);

            firstBoot = true;
        }

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

    }




    public void HideAll()
	{
        Mappa.enabled = false;
        BGRPG1.enabled = false;
        BGRPG2.enabled = false;
        BGRPG3.enabled = false;
        BGRPG4.enabled = false;
        BGRPG5.enabled = false;
        PG1.enabled = false;
        PG2.enabled = false;
        MapIcon1.enabled = false;
        MapIcon2.enabled = false;
        MapIcon3.enabled = false;
        MapIcon4.enabled = false;
        MapIcon5.enabled = false;
        EscIcon.enabled = false;
    }

    public void IntroEnd()
    {
        LEADERPG.SetActive(false);
        LEADERBGR.SetActive(false);
        DAY.enabled = true;

        ICONS.SetActive(true);
    }

    public void ShowMap()
    {
        TimeManager.GetComponent<TimeScript>().WheelUpdate();


        OST.SetParameter("MenuDrums", 100f);

        OST.SetParameter("Leader", 30f);
        OST.SetParameter("LeaderDrums",0f);
        OST.SetParameter("Contadina", 30f);
        OST.SetParameter("ContadinaDrums", 0f);
        OST.SetParameter("Suora", 30f);
        OST.SetParameter("SuoraDrums", 0f);
        OST.SetParameter("Ubriacone", 30f);
        OST.SetParameter("UbriaconeDrums", 0f);
        OST.SetParameter("Fabbro", 30f);
        OST.SetParameter("FabbroDrums", 0f);
        OST.SetParameter("Pellegrini", 30f);
        OST.SetParameter("PellegriniDrums", 0f);

        timeToTime = true;
        Mappa.enabled = true;
        BGRPG1.enabled = false;
        BGRPG2.enabled = false;
        BGRPG3.enabled = false;
        BGRPG4.enabled = false;
        BGRPG5.enabled = false;
        //PGs
        PG1.enabled = false;
		PG2.enabled = false;
        PG3N.enabled = false;
        PG4.enabled = false;
        PG5.enabled = false;
        //PGabs
        PG1absent.enabled = false;
        PG2absent.enabled = false;
        PG3absent.enabled = false;
        PG4absent.enabled = false;
        PG5absent.enabled = false;
        //PGContainers
        PG1Container.SetActive(false);
        PG2Container.SetActive(false);
        PG3Container.SetActive(false);
        PG4Container.SetActive(false);
        PG5Container.SetActive(false);

        MapIcon1.enabled = true;
        MapIcon2.enabled = true;
        MapIcon3.enabled = true;
        MapIcon4.enabled = true;
        MapIcon5.enabled = true;
        EscIcon.enabled = false;
        ConfidoSpiga.SetActive(false);
        Zaino.SetActive(false);
    }

    public void ShowPG1()
    {
        OST.SetParameter("Suora", 60f);
        OST.SetParameter("SuoraDrums", 90f);
        OST.SetParameter("MenuDrums", 0f);

        PG1Container.SetActive(true);
        PG2Container.SetActive(false);
        PG3Container.SetActive(false);
        PG4Container.SetActive(false);
        PG5Container.SetActive(false);

        PhaseCheckBGR();

        Mappa.enabled = false;
        BGRPG1.enabled = true;
        BGRPG2.enabled = false;
        BGRPG3.enabled = false;
        BGRPG4.enabled = false;
        BGRPG5.enabled = false;

        if (Phase == "NOTTE" || LevelManager.GetComponent<LevelManager>().PG1win || LevelManager.GetComponent<LevelManager>().PG1lost)
        {
            PG1.enabled = false;
            PG1absent.enabled = true;
        }
        else
        {
            PG1.enabled = true;
            PG1absent.enabled = false;
            PG1Emoji(1);
        }
        
        MapIcon1.enabled = false;
        MapIcon2.enabled = false;
        MapIcon3.enabled = false;
        MapIcon4.enabled = false;
        MapIcon5.enabled = false;
        EscIcon.enabled = true;
        
        ConfidoSpiga.SetActive(true);
        Zaino.SetActive(true);
    }
    public void ShowPG2()
    {
        OST.SetParameter("Contadina", 60f);
        OST.SetParameter("ContadinaDrums", 90f);
        OST.SetParameter("MenuDrums", 0f);

        PG1Container.SetActive(false);
        PG2Container.SetActive(true);
        PG3Container.SetActive(false);
        PG4Container.SetActive(false);
        PG5Container.SetActive(false);

        PhaseCheckBGR();

        Mappa.enabled = false;
        BGRPG1.enabled = false;
        BGRPG2.enabled = true;
        BGRPG3.enabled = false;
        BGRPG4.enabled = false;
        BGRPG5.enabled = false;

        if (Phase == "MATTINA" || LevelManager.GetComponent<LevelManager>().PG2win || LevelManager.GetComponent<LevelManager>().PG2lost)
        {
            PG2.enabled = false;
            PG2absent.enabled = true;
        }
        else
        {
            PG2.enabled = true;
            PG2absent.enabled = false;
            PG2Emoji(1);
        }

        MapIcon1.enabled = false;
        MapIcon2.enabled = false;
        MapIcon3.enabled = false;
        MapIcon4.enabled = false;
        MapIcon5.enabled = false;
        EscIcon.enabled = true;

        ConfidoSpiga.SetActive(true);
        Zaino.SetActive(true);
    }
    public void ShowPG3()
    {
        OST.SetParameter("Pellegrini", 60f);
        OST.SetParameter("PellegriniDrums", 90f);
        OST.SetParameter("MenuDrums", 0f);

        PG1Container.SetActive(false);
        PG2Container.SetActive(false);
        PG3Container.SetActive(true);
        PG4Container.SetActive(false);
        PG5Container.SetActive(false);

        PhaseCheckBGR();

        Mappa.enabled = false;
        BGRPG1.enabled = false;
        BGRPG2.enabled = false;
        BGRPG3.enabled = true;
        BGRPG4.enabled = false;
        BGRPG5.enabled = false;

        if (Phase == "NOTTE" || LevelManager.GetComponent<LevelManager>().PG3win || LevelManager.GetComponent<LevelManager>().PG3lost)
        {
            PG3N.enabled = false;
            PG3absent.enabled = true;
        }
        else
        {
            PG3N.enabled = true;
            PG3absent.enabled = false;
            PG3Emoji(1);
        }

        MapIcon1.enabled = false;
        MapIcon2.enabled = false;
        MapIcon3.enabled = false;
        MapIcon4.enabled = false;
        MapIcon5.enabled = false;
        EscIcon.enabled = true;

        ConfidoSpiga.SetActive(true);
        Zaino.SetActive(true);
    }

    public void ShowPG4()
    {
        OST.SetParameter("Fabbro", 60f);
        OST.SetParameter("FabbroDrums", 90f);
        OST.SetParameter("MenuDrums", 0f);

        PG1Container.SetActive(false);
        PG2Container.SetActive(false);
        PG3Container.SetActive(false);
        PG4Container.SetActive(true);
        PG5Container.SetActive(false);

        PhaseCheckBGR();

        Mappa.enabled = false;
        BGRPG1.enabled = false;
        BGRPG2.enabled = false;
        BGRPG3.enabled = false;
        BGRPG4.enabled = true;
        BGRPG5.enabled = false;

        if (Phase == "POME" || LevelManager.GetComponent<LevelManager>().PG4win || LevelManager.GetComponent<LevelManager>().PG4lost)
        {
            PG4.enabled = false;
            PG4absent.enabled = true;
        }
        else
        {
            PG4.enabled = true;
            PG4absent.enabled = false;
            PG4Emoji(1);
        }

        MapIcon1.enabled = false;
        MapIcon2.enabled = false;
        MapIcon3.enabled = false;
        MapIcon4.enabled = false;
        MapIcon5.enabled = false;
        EscIcon.enabled = true;

        ConfidoSpiga.SetActive(true);
        Zaino.SetActive(true);
    }

    public void ShowPG5()
    {
        OST.SetParameter("Ubriacone", 60f);
        OST.SetParameter("UbriaconeDrums", 90f);
        OST.SetParameter("MenuDrums", 0f);

        PG1Container.SetActive(false);
        PG2Container.SetActive(false);
        PG3Container.SetActive(false);
        PG4Container.SetActive(false);
        PG5Container.SetActive(true);

        PhaseCheckBGR();

        Mappa.enabled = false;
        BGRPG1.enabled = false;
        BGRPG2.enabled = false;
        BGRPG3.enabled = false;
        BGRPG4.enabled = false;
        BGRPG5.enabled = true;

        if (Phase == "SERA" || LevelManager.GetComponent<LevelManager>().PG5win || LevelManager.GetComponent<LevelManager>().PG5lost)
        {
            PG5.enabled = false;
            PG5absent.enabled = true;
        }
        else
        {
            PG5.enabled = true;
            PG5absent.enabled = false;
            PG5Emoji(1);
        }

        MapIcon1.enabled = false;
        MapIcon2.enabled = false;
        MapIcon3.enabled = false;
        MapIcon4.enabled = false;
        MapIcon5.enabled = false;
        EscIcon.enabled = true;

        ConfidoSpiga.SetActive(true);
        Zaino.SetActive(true);
    }

    public void PG1Emoji(int i)
	{
		switch (i)
		{
            case 1:
                PG1.enabled = true;
                PG1F.enabled = false;
                PG1P.enabled = false;
                break;
            case 2:
                PG1.enabled = false;
                PG1F.enabled = true;
                PG1P.enabled = false;
                break;
            case 3:
                PG1.enabled = false;
                PG1F.enabled = false;
                PG1P.enabled = true;
                break;
        }
    }

    public void PG2Emoji(int i)
    {
        switch (i)
        {
            case 1:
                PG2.enabled = true;
                PG2F.enabled = false;
                PG2P.enabled = false;
                break;
            case 2:
                PG2.enabled = false;
                PG2F.enabled = true;
                PG2P.enabled = false;
                break;
            case 3:
                PG2.enabled = false;
                PG2F.enabled = false;
                PG2P.enabled = true;
                break;
        }
    }
    public void PG3Emoji(int i)
    {
        switch (i)
        {
            case 1: //N
                PG3N.enabled = true;
                PG3FN.enabled = false;
                PG3FA.enabled = false;
                PG3AF.enabled = false;
                break;
            case 2: //FN
                PG3N.enabled = false;
                PG3FN.enabled = true;
                PG3FA.enabled = false;
                PG3AF.enabled = false;
                break;
            case 3: //FA
                PG3N.enabled = false;
                PG3FN.enabled = false;
                PG3FA.enabled = true;
                PG3AF.enabled = false;
                break;
            case 4:  //AF
                PG3N.enabled = false;
                PG3FN.enabled = false;
                PG3FA.enabled = false;
                PG3AF.enabled = true;
                break;
        }
    }
    public void PG4Emoji(int i)
    {
        switch (i)
        {
            case 1:
                PG4.enabled = true;
                PG4P.enabled = false;
                break;
            case 2:
                PG4.enabled = false;
                PG4P.enabled = true;
                break;
        }
    }
    public void PG5Emoji(int i)
    {
        switch (i)
        {
            case 1:
                PG5.enabled = true;
                PG5P.enabled = false;
                break;
            case 2:
                PG5.enabled = false;
                PG5P.enabled = true;
                break;
        }
    }
    public void EndingTime(int i)
    {
        OST.Stop();

        SceneManager.LoadScene(3);
    }

    public void ShowChoices()
	{
        ButtonContainer.SetActive(true);
	}

    public void HideChoices()
    {
        ButtonContainer.SetActive(false);
    }

    public void AllChoices()
	{
        ButtonChoice1.SetActive(true);
        ButtonChoice2.SetActive(true);
        ButtonChoice3.SetActive(true);
        ButtonChoice4.SetActive(true);
    }

    public void Choices3()
	{
        ButtonChoice1.SetActive(true);
        ButtonChoice2.SetActive(true);
        ButtonChoice3.SetActive(true);
        ButtonChoice4.SetActive(false);
    }
    public void Choices2()
    {
        ButtonChoice1.SetActive(true);
        ButtonChoice2.SetActive(true);
        ButtonChoice3.SetActive(false);
        ButtonChoice4.SetActive(false);
    }

    public void Choices1()
	{
        ButtonChoice1.SetActive(true);
        ButtonChoice2.SetActive(false);
        ButtonChoice3.SetActive(false);
        ButtonChoice4.SetActive(false);
    }

    public void StartDialogue(int i)
	{
        //DiagContainer.SetActive(true);
        if (timeToTime == true)
		{
            LevelManager.GetComponent<LevelManager>().progressHindLock(i);
        }

        LevelManager.GetComponent<LevelManager>().DialogueSwitch(i);
        SpigaManager.SpigaFind(i);
        
        if (timeToTime == true)
		{
            TimeManager.GetComponent<TimeScript>().AdvanceTime(1);
            timeToTime = false;
        }

        currentPG = i;

        if (i == 6)
		{
            OST.SetParameter("LeaderDrums", 100f);
		}

	}

    public void EndDialogue()
	{
        SpigaManager.SpigaUpdate();
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
            case SingleTextBox.Choice3:
                Choice3.text = text;
                break;
            case SingleTextBox.Choice4:
                Choice4.text = text;
                break;
            case SingleTextBox.DialogueBox:
                Dialogue.text = "\"" + text + "\"";
                break;
        }
    }

}
