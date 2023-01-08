using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tommy;
using System.IO;
using Boxes = CanvasController.SingleTextBox;
using MultiBoxes = CanvasController.MultiTextBox;

public class LevelManager : MonoBehaviour
{

    // The .toml file which contains the information to populate the level
    // !!! ASSIGN THE CORRESPONDING ONE FOR EACH LEVEL
    public TextAsset levelSource;
    public TextAsset levelSource2;
    private TomlTable levelTable;

    private CanvasController canvas;
    private TimeScript TimeScript;

    public bool actionInProgress = false;
    public int pressedButton = 0;

    // bools firsttime
    bool firstTime1 = true;

    // Menus PG1
    bool mainmenu = true;
    bool BSTMenu;
    bool BSTMenu1;
    bool BSTMenu1B;
    bool BSTMenu1C;
    bool BSTMenu1D;
    bool BSTMenu2;
    bool BSTMenu2B;
    bool BSTMenu3;
    bool BSTMenu3B;
    bool BSTMenu4;
    bool BSTMenu4B;

    // SpigaCheck PG1
    bool SpigaTimeFestaPG1; //neg
    bool SpigaTimeBST1P2BPG1; //neg
    bool SpigaTimeBST1P2CPG1; //pos
    bool SpigaTimeBST1P3APG1; //neg
    bool SpigaTimeBST1P3BPG1; //neg
    bool SpigaTimeBST1P3CPG1; //pos
    bool SpigaTimeBST1P4APG1; //neg
    bool SpigaTimeBST1P4BPG1; //neg
    bool SpigaTimeBST1P4CPG1; //MOLTO neg
    bool SpigaTimeBST2P2PG1; //neg
    bool SpigaTimeBST4P2PG1; //pos


    string PGUnavail;
    string Phase;

    //ints per gli oggetti raccolti
    string letterInt = "";
    string choice2int = "";
    string choice1int = "";


    // bools per i primi dialoghi
    bool PG1first = true;
    bool PG2first = true;
    bool PG3first = true;
    bool PG4first = true;
    bool PG5first = true;

    // ints per tenere traccia del dialogo
    //ALLPGs
    int BST1Phase;
    int BST2Phase;
    int BST3Phase;
    int BST4Phase;

    // PG1
    int BST1Phase1;
    int BST2Phase1;
    int BST3Phase1;
    int BST4Phase1;
    // also bools per impedire il farm
    bool BST1P1done;
    bool BST1P2done;
    bool BST1P3done;
    bool BST2P1done;
    bool BST2P2done;
    bool BST3P1done;
    bool BST4P1done;

    // bool per impedire di avanzare right away
    // PG1
    int PG1currentDay1P1;
    int PG1currentPhase1P1;
    int PG1currentDay1P2;
    int PG1currentPhase1P2;
    int PG1currentDay1P3;
    int PG1currentPhase1P3;
    int PG1currentDay2P1;
    int PG1currentPhase2P1;
    int PG1currentDay2P2;
    int PG1currentPhase2P2;
    int PG1currentDay3P1;
    int PG1currentPhase3P1;
    int PG1currentDay4P1;
    int PG1currentPhase4P1;
    // PGs
    bool progressHind1;
    bool progressHind2;
    bool progressHind3;
    bool progressHind4;
    bool progressHind5;

    // Start is called before the first frame update
    void Start()
    {
        // Set canvas controller
        canvas = GameObject.Find("Canvas Controller").GetComponent<CanvasController>();

        TimeScript = GameObject.Find("Time Manager").GetComponent<TimeScript>();


        BST1Phase1 = 1;
        BST2Phase1 = 1;
        BST3Phase1 = 1;
        BST4Phase1 = 1;

    }

    // Update is called once per frame
    void Update()
    {
        Phase = canvas.Phase;


        if (BST1P1done == true || BST2P1done == true || BST3P1done == true || BST4P1done == true)
        {
            choice2int = "new";
        }

    }

    public void progressHindLock(int i)
	{
		switch (i)
		{
            case 1:
                progressHind1 = false;
                break;
            case 2:
                progressHind2 = false;
                break;
            case 3:
                progressHind3 = false;
                break;
            case 4:
                progressHind4 = false;
                break;
            case 5:
                progressHind5 = false;
                break;
        }
	}

    public void SpigaGauge(int i)
	{
		// per quando la spiga si alza o si abbassa

		if (i > 0)
		{
            print("SPIGA SU");
		}
		else
		{
            print("SPIGA GIU");
		}
	}

    public void DialogueSwitch(int i)
	{
        string PGUnavail;
        switch (i)
        {
            case 1:
                // File reading works correctly
                string fileContents = levelSource.ToString();
                //Debug.Log(fileContents);

                // TOML table translation
                levelTable = TOML.Parse(new StringReader(fileContents));

                PGUnavail = "NOTTE";

                StartCoroutine(dialogueP1Routine(PGUnavail));
                break;
            case 2:
                // File reading works correctly
                fileContents = levelSource2.ToString();
                //Debug.Log(fileContents);

                // TOML table translation
                levelTable = TOML.Parse(new StringReader(fileContents));

                PGUnavail = "POME";

                StartCoroutine(dialogueP2Routine(PGUnavail));
                break;
        }

    }

    // The flow of the level is handled here
    public IEnumerator dialogueP1Routine(string PGUnavail)
    {

        // Wait a frame to allow for initialization of other classes
        yield return null;

        actionInProgress = true;

        if (progressHind1 == false)
		{
            BST1Phase = BST1Phase1;
            BST2Phase = BST2Phase1;
            BST3Phase = BST3Phase1;
            BST4Phase = BST4Phase1;
            progressHind1 = true;
        }


        // Change name of pg
        string name = levelTable["name"].ToString();
        canvas.printToTextBox(name, Boxes.Name);

        // Se la fase corrente è quella in cui non ci sono:
        if (Phase == PGUnavail)
        {
            List<string> introTextUNAVAIL = getStringList(levelTable["introUNAVAIL"]["introtext"].AsArray);
            yield return StartCoroutine(canvas.printDialogue(introTextUNAVAIL, MultiBoxes.DialogueBox));

            actionInProgress = false;
            canvas.EndDialogue();
            yield return null;
        }


        // Generate and print intro text
        if (firstTime1) // se è la prima volta
        {
            firstTime1 = false;
            List<string> introFirstText = getStringList(levelTable["introFirst"]["introtext"].AsArray);
            yield return StartCoroutine(canvas.printDialogue(introFirstText, MultiBoxes.DialogueBox));
		}
		else // tutte le altre volte
		{
            List<string> introText = getStringList(levelTable["intro"]["introtext"].AsArray);
            yield return StartCoroutine(canvas.printDialogue(introText, MultiBoxes.DialogueBox));
        }
        
        // mostra le prime scelte
        canvas.ShowChoices();
        canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
        canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
        canvas.printToTextBox(levelTable["choice2"]["choicetext"+choice2int].ToString(), Boxes.Choice2);
        canvas.printToTextBox(levelTable["choice3"+letterInt]["choicetext"].ToString(), Boxes.Choice3);
        canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

        while (true)
        {
            if (mainmenu == true)
			{
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

					if (SpigaTimeFestaPG1 == false)
					{
                        SpigaGauge(-1);
                        SpigaTimeFestaPG1 = true;
                    }
                    List<string> choice1diag = getStringList(levelTable["choice1"]["choicediag"+choice1int].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(choice1diag, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    List<string> choice2diag = getStringList(levelTable["choice2"]["choicediag"+choice2int].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(choice2diag, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortextBST"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["BST1P" + BST1Phase]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["BST2P" + BST2Phase]["choicetext"].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["BST3P" + BST3Phase]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["BST4P" + BST4Phase]["choicetext"].ToString(), Boxes.Choice4);

                    BSTMenu = true;
                    mainmenu = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 3)
				{
                    canvas.HideChoices();

                    if (letterInt == "yes")
					{
                        print("gotcha");
					}
					else
					{
                        
                    }
                    //temp
                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 4)
				{
                    canvas.HideChoices();

                    List<string> choice4diag = getStringList(levelTable["choice4"]["choicediag"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(choice4diag, MultiBoxes.DialogueBox));

                    pressedButton = 0;

                    mainmenu = true;
                    canvas.AllChoices();
                    actionInProgress = false;
                    canvas.EndDialogue();
                }
            }

            // BSTmenu
            if (BSTMenu == true)
			{
                if (pressedButton == 1)
				{
                    if (BST1Phase == 1)
					{
                        canvas.HideChoices();

                        List<string> BST1P1diag = getStringList(levelTable["BST1P1"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST1P1diag, MultiBoxes.DialogueBox));

                        PG1currentPhase1P1 = TimeScript.phaseNum;
                        PG1currentDay1P1 = TimeScript.day;
                        choice2int = "new";

                        if (!BST1P1done)
						{
                            BST1Phase1++;
                            BST1P1done = true;
                        }

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                        canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                        canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                        mainmenu = true;
                        canvas.AllChoices();
                        pressedButton = 0;
					}

					if (BST1Phase == 2 && (PG1currentPhase1P1 != TimeScript.phaseNum || PG1currentDay1P1 != TimeScript.day))
					{
						canvas.HideChoices();

                        List<string> BST1P2diag = getStringList(levelTable["BST1P2"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST1P2diag, MultiBoxes.DialogueBox));

                        PG1currentPhase1P2 = TimeScript.phaseNum;
                        PG1currentDay1P2 = TimeScript.day;

                        if (!BST1P2done)
                        {
                            BST1Phase1++;
                            BST1P2done = true;
                        }

                        canvas.ShowChoices();
						canvas.printToTextBox(levelTable["intro"]["flavortextBST"].ToString(), Boxes.DialogueBox);
						canvas.printToTextBox(levelTable["BST1P2"]["choicetext2A"].ToString(), Boxes.Choice1);
						canvas.printToTextBox(levelTable["BST1P2"]["choicetext2B"].ToString(), Boxes.Choice2);
						canvas.printToTextBox(levelTable["BST1P2"]["choicetext2C"].ToString(), Boxes.Choice3);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu1 = true;
                        canvas.Choices3();
                        pressedButton = 0;
					}

					if (BST1Phase == 3 && (PG1currentPhase1P2 != TimeScript.phaseNum || PG1currentDay1P2 != TimeScript.day))
					{
                        canvas.HideChoices();

                        List<string> BST1P3diag = getStringList(levelTable["BST1P3"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST1P3diag, MultiBoxes.DialogueBox));

                        PG1currentPhase1P3 = TimeScript.phaseNum;
                        PG1currentDay1P3 = TimeScript.day;


                        if (!BST1P3done)
                        {
                            BST1Phase1++;
                            BST1P3done = true;
                        }

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextBST"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST1P3"]["choicetext2A"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["BST1P3"]["choicetext2B"].ToString(), Boxes.Choice2);
                        canvas.printToTextBox(levelTable["BST1P3"]["choicetext2C"].ToString(), Boxes.Choice3);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu1 = false;
                        BSTMenu1B = true;
                        canvas.Choices3();
                        pressedButton = 0;
                    }

					if (BST1Phase == 4 && (PG1currentPhase1P3 != TimeScript.phaseNum || PG1currentDay1P3 != TimeScript.day))
                    {
                        canvas.HideChoices();

                        List<string> BST1P4diag = getStringList(levelTable["BST1P4"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST1P4diag, MultiBoxes.DialogueBox));

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST1P4"]["choicetext2"].ToString(), Boxes.Choice1);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu1 = false;
                        BSTMenu1B = false;
                        BSTMenu1C = true;
                        canvas.Choices1();
                        pressedButton = 0;
                    }

                }

                if (pressedButton == 2)
                {
                    if (BST2Phase == 1)
                    {
                        canvas.HideChoices();

                        List<string> BST2P1diag = getStringList(levelTable["BST2P1"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST2P1diag, MultiBoxes.DialogueBox));

                        PG1currentPhase2P1 = TimeScript.phaseNum;
                        PG1currentDay2P1 = TimeScript.day;
                        choice2int = "new";

                        if (!BST2P1done)
                        {
                            BST2Phase1++;
                            BST2P1done = true;
                        }

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST2P1"]["choicetext2"].ToString(), Boxes.Choice1);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu2 = true;
                        canvas.Choices1();
                        pressedButton = 0;
                    }

                    if (BST2Phase == 2 && (PG1currentPhase2P1 != TimeScript.phaseNum || PG1currentDay2P1 != TimeScript.day))
                    {   
                        canvas.HideChoices();

                        if(SpigaTimeBST2P2PG1 == false)
						{
                            SpigaGauge(-1);
                            SpigaTimeBST2P2PG1 = true;
                        }
                        
                        List<string> BST2P2diag = getStringList(levelTable["BST2P2"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST2P2diag, MultiBoxes.DialogueBox));

                        PG1currentPhase2P2 = TimeScript.phaseNum;
                        PG1currentDay2P2 = TimeScript.day;

                        if (!BST2P2done)
                        {
                            BST2Phase1++;
                            BST2P2done = true;
                        }

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                        canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                        canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                        mainmenu = true;
                        BSTMenu = false;
                        BSTMenu2 = false;
                        canvas.AllChoices();
                        pressedButton = 0;
                    }

                    if (BST2Phase == 3 && (PG1currentPhase2P2 != TimeScript.phaseNum || PG1currentDay2P2 != TimeScript.day))
                    {
                        canvas.HideChoices();

                        List<string> BST2P3diag = getStringList(levelTable["BST2P3"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST2P3diag, MultiBoxes.DialogueBox));

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST2P3"]["choicetext2A"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["BST2P3"]["choicetext2B"].ToString(), Boxes.Choice2);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu2 = false;
                        BSTMenu2B = true;
                        canvas.Choices2();
                        pressedButton = 0;
                    }

                }

                if (pressedButton == 3)
                {
                    if (BST3Phase == 1)
                    {
                        canvas.HideChoices();

                        List<string> BST3P1diag = getStringList(levelTable["BST3P1"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST3P1diag, MultiBoxes.DialogueBox));

                        PG1currentPhase3P1 = TimeScript.phaseNum;
                        PG1currentDay3P1 = TimeScript.day;
                        choice2int = "new";

                        if (!BST3P1done)
                        {
                            BST3Phase1++;
                            BST3P1done = true;
                        }

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST3P1"]["choicetext2"].ToString(), Boxes.Choice1);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu3 = true;
                        canvas.Choices1();
                        pressedButton = 0;
                    }

                    if (BST3Phase == 2 && (PG1currentPhase3P1 != TimeScript.phaseNum || PG1currentDay3P1 != TimeScript.day))
                    {
                        canvas.HideChoices();

                        List<string> BST3P2diag = getStringList(levelTable["BST3P2"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST3P2diag, MultiBoxes.DialogueBox));

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST3P2"]["choicetext2"].ToString(), Boxes.Choice1);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu3 = false;
                        BSTMenu3B = true;
                        canvas.Choices1();
                        pressedButton = 0;
                    }
                }

                if (pressedButton == 4)
                {
                    if (BST4Phase == 1)
                    {
                        canvas.HideChoices();

                        List<string> BST4P1diag = getStringList(levelTable["BST4P1"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST4P1diag, MultiBoxes.DialogueBox));

                        PG1currentPhase4P1 = TimeScript.phaseNum;
                        PG1currentDay4P1 = TimeScript.day;
                        choice2int = "new";

                        if (!BST4P1done)
                        {
                            BST4Phase1++;
                            BST4P1done = true;
                        }

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST4P1"]["choicetext2"].ToString(), Boxes.Choice1);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu4 = true;
                        canvas.Choices1();
                        pressedButton = 0;
                    }

                    if (BST4Phase == 2 && (PG1currentPhase4P1 != TimeScript.phaseNum || PG1currentDay4P1 != TimeScript.day))
                    {
                        canvas.HideChoices();

                        if (SpigaTimeBST4P2PG1 == false)
						{
                            SpigaGauge(1);
                            SpigaTimeBST4P2PG1 = true;
                        }
                        List<string> BST4P2diag = getStringList(levelTable["BST4P2"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST4P2diag, MultiBoxes.DialogueBox));

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST4P2"]["choicetext2"].ToString(), Boxes.Choice1);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu4 = false;
                        BSTMenu4B = true;
                        canvas.Choices1();
                        pressedButton = 0;
                    }
                }

            }

            if (BSTMenu1 == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    List<string> BST1P2diag2A = getStringList(levelTable["BST1P2"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P2diag2A, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu1 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    if (SpigaTimeBST1P2BPG1 == false)
					{
                        SpigaGauge(-1);
                        SpigaTimeBST1P2BPG1 = true;
					}
                    List<string> BST1P2diag2B = getStringList(levelTable["BST1P2"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P2diag2B, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu1 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 3)
                {
                    canvas.HideChoices();

                    if (SpigaTimeBST1P2CPG1 == false)
                    {
                        SpigaGauge(1);
                        SpigaTimeBST1P2CPG1 = true;
                    }
                    List<string> BST1P2diag2C = getStringList(levelTable["BST1P2"]["choicediag2C"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P2diag2C, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu1 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

            }

            if (BSTMenu1B == true)
			{
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    if (SpigaTimeBST1P3APG1 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST1P3APG1 = true;
                    }
                    List<string> BST1P3diag2A = getStringList(levelTable["BST1P3"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P3diag2A, MultiBoxes.DialogueBox));

                    choice1int = "new";

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu1 = false;
                    BSTMenu1B = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    if (SpigaTimeBST1P3BPG1 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST1P3BPG1 = true;
                    }
                    List<string> BST1P3diag2B = getStringList(levelTable["BST1P3"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P3diag2B, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu1 = false;
                    BSTMenu1B = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 3)
                {
                    canvas.HideChoices();

                    if (SpigaTimeBST1P3CPG1 == false)
                    {
                        SpigaGauge(1);
                        SpigaTimeBST1P3CPG1 = true;
                    }
                    List<string> BST1P3diag2C = getStringList(levelTable["BST1P3"]["choicediag2C"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P3diag2C, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu1 = false;
                    BSTMenu1B = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }

            if (BSTMenu1C == true)
            {
                if (pressedButton == 1)
				{
                    canvas.HideChoices();

                    List<string> BST1P4diag = getStringList(levelTable["BST1P4"]["choicediag2"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P4diag, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortextBST"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["BST1P4"]["choicetext2A"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["BST1P4"]["choicetext2B"].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["BST1P4"]["choicetext2C"].ToString(), Boxes.Choice3);

                    mainmenu = false;
                    BSTMenu = false;
                    BSTMenu1 = false;
                    BSTMenu1B = false;
                    BSTMenu1C = false;
                    BSTMenu1D = true;
                    canvas.Choices3();
                    pressedButton = 0;
                }
            }

            if (BSTMenu1D == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    if (SpigaTimeBST1P4APG1 == false)
					{
                        SpigaGauge(-1);
                        SpigaTimeBST1P4APG1 = true;
                    }

                    List<string> BST1P4diag2A = getStringList(levelTable["BST1P4"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P4diag2A, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu1 = false;
                    BSTMenu1B = false;
                    BSTMenu1C = false;
                    BSTMenu1D = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    if (SpigaTimeBST1P4BPG1 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST1P4BPG1 = true;
                    }

                    List<string> BST1P4diag2B = getStringList(levelTable["BST1P4"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P4diag2B, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu1 = false;
                    BSTMenu1B = false;
                    BSTMenu1C = false;
                    BSTMenu1D = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 3)
                {
                    canvas.HideChoices();

                    if (SpigaTimeBST1P4CPG1 == false)
                    {
                        SpigaGauge(-3);
                        SpigaTimeBST1P4CPG1 = true;
                    }

                    List<string> BST1P4diag2C = getStringList(levelTable["BST1P4"]["choicediag2C"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P4diag2C, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu1 = false;
                    BSTMenu1B = false;
                    BSTMenu1C = false;
                    BSTMenu1D = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }

            if (BSTMenu2 == true)
			{
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    List<string> BST2P1diag = getStringList(levelTable["BST2P1"]["choicediag2"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST2P1diag, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu2 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }

            if (BSTMenu2B == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    List<string> BST2P3diag2A = getStringList(levelTable["BST2P3"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST2P3diag2A, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu2 = false;
                    BSTMenu2B = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    List<string> BST2P3diag2B = getStringList(levelTable["BST2P3"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST2P3diag2B, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu2 = false;
                    BSTMenu2B = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }

            if (BSTMenu3 == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    List<string> BST3P1diag = getStringList(levelTable["BST3P1"]["choicediag2"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST3P1diag, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu3 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }

            if (BSTMenu3B == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    List<string> BST3P2diag = getStringList(levelTable["BST3P2"]["choicediag2"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST3P2diag, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu3 = false;
                    BSTMenu3B = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }

            if (BSTMenu4 == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    List<string> BST4P1diag = getStringList(levelTable["BST4P1"]["choicediag2"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST4P1diag, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu4 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }

            if (BSTMenu4B == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    List<string> BST4P2diag = getStringList(levelTable["BST4P2"]["choicediag2"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST4P2diag, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3" + letterInt]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu4 = false;
                    BSTMenu4B = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }

            // ITEMmenu

            yield return null;
        }


    }


    public IEnumerator dialogueP2Routine(string PGUnavail)
	{
        yield return null;
	}





    public void pressButton(int buttonNumber)
    {
        pressedButton = buttonNumber;
    }

    private List<string> getStringList(TomlArray arr)
    {
        List<string> ans = new List<string>();
        foreach (TomlString s in arr) ans.Add(s.ToString());
        return ans;
    }
    private List<TomlNode> getNodeList(TomlArray arr)
    {
        List<TomlNode> ans = new List<TomlNode>();
        foreach (TomlNode n in arr) ans.Add(n);
        return ans;
    }
}
