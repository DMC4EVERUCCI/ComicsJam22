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
    public TextAsset levelSource3;
    public TextAsset levelSource4;
    public TextAsset levelSource5;
    private TomlTable levelTable;

    private CanvasController canvas;
    private TimeScript TimeScript;

    public bool actionInProgress = false;
    public int pressedButton = 0;

    // bools firsttime
    bool firstTime1 = true;
    bool firstTime2 = true;
    bool firstTime3 = true;
    bool firstTime4 = true;
    bool firstTime5 = true;
    //bools win/lose
    //PG1
    public bool PG1win;
    public bool PG1lost;
    bool PG1prova1;
    bool PG1prova2;
    //PG2
    public bool PG2win;
    public bool PG2lost;
    //PG3
    public bool PG3win;
    public bool PG3lost;

    // Menus
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
    bool BSTMenu3C;
    bool BSTMenu4;
    bool BSTMenu4B;
    bool ItemMenu;
    bool ItemMenu3;
    bool ItemMenu4;

    //Spigadone
    bool PG1SpigaDONE;
    bool PG2SpigaDONE;
    bool PG3SpigaDONE;
    bool PG4SpigaDONE;
    bool PG5SpigaDONE;

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

    // SpigaCheck PG2
    bool SpigaTimeFestaPG2; //neg
    bool SpigaTimeBST1P1PG2; //neg
    bool SpigaTimeBST1P2PG2; //pos
    bool SpigaTimeBST2P1PG2; //neg
    bool SpigaTimeBST4P1PG2; //neg
    bool SpigaTimeBST1P3APG2; //neg
    bool SpigaTimeBST1P3BPG2; //neg
    bool SpigaTimeBST1P3CPG2; //pos
    bool SpigaTimeBST1P4APG2; //neg
    bool SpigaTimeBST1P4BPG2; //pos
    bool SpigaTimeBST1P4CPG2; //neg
    bool SpigaTimeBST2P3APG2; //pos
    bool SpigaTimeBST2P3BPG2; //neg
    bool SpigaTimeBST2P2APG2; //pos
    bool SpigaTimeBST2P2BPG2; //neg
    
    // SpigaCheck PG3
    bool SpigaTimeFestaPG3; //neg
    bool SpigaTimeBST1P2APG3; //neg
    bool SpigaTimeBST1P2BPG3; //pos
    bool SpigaTimeBST2P1APG3; //pos
    bool SpigaTimeBST2P1BPG3; //neg
    bool SpigaTimeBST2P2APG3; //pos
    bool SpigaTimeBST2P2BPG3; //neg
    bool SpigaTimeBST3P2BPG3; //neg
    bool SpigaTimeBST3P3APG3; //neg
    bool SpigaTimeBST3P3BPG3; //neg
    bool SpigaTimeBST3P3CPG3; //pos


    string PGUnavail;
    string Phase;

    //ints per gli oggetti raccolti & dialoghi cambiati
    // PG1
    string letterInt = "";
    string choice2int = "";
    string choice1int = "";
    // PG2
    string seedInt = "";
    string PG2choice2int = "";
    string PG2choice1int = "";
    // PG2
    string toothInt = "";
    string PG3choice2int = "";
    string PG3choice1int = "";
    bool PG3bad;


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
    // PG2
    int BST1Phase2;
    int BST2Phase2;
    int BST3Phase2;
    int BST4Phase2;
    // also bools per impedire il farm
    bool PG2BST1P1done;
    bool PG2BST1P2done;
    bool PG2BST1P3done;
    bool PG2BST2P1done;
    bool PG2BST2P2done;
    bool PG2BST3P1done;
    bool PG2BST4P1done;
    // PG3
    int BST1Phase3;
    int BST2Phase3;
    int BST3Phase3;
    int BST4Phase3;
    // also bools per impedire il farm
    bool PG3BST1P1done;
    bool PG3BST1P2done;
    bool PG3BST1P3done;
    bool PG3BST2P1done;
    bool PG3BST2P2done;
    bool PG3BST3P1done;
    bool PG3BST3P2done;
    bool PG3BST4P1done;

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
    // PG2
    int PG2currentDay1P1;
    int PG2currentPhase1P1;
    int PG2currentDay1P2;
    int PG2currentPhase1P2;
    int PG2currentDay1P3;
    int PG2currentPhase1P3;
    int PG2currentDay2P1;
    int PG2currentPhase2P1;
    int PG2currentDay2P2;
    int PG2currentPhase2P2;
    int PG2currentDay3P1;
    int PG2currentPhase3P1;
    int PG2currentDay4P1;
    int PG2currentPhase4P1;
    // PG3
    int PG3currentDay1P1;
    int PG3currentPhase1P1;
    int PG3currentDay1P2;
    int PG3currentPhase1P2;
    int PG3currentDay1P3;
    int PG3currentPhase1P3;
    int PG3currentDay2P1;
    int PG3currentPhase2P1;
    int PG3currentDay2P2;
    int PG3currentPhase2P2;
    int PG3currentDay3P1;
    int PG3currentPhase3P1;
    int PG3currentDay3P2;
    int PG3currentPhase3P2;
    int PG3currentDay4P1;
    int PG3currentPhase4P1;

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

        BST1Phase2 = 1;
        BST2Phase2 = 1;
        BST3Phase2 = 1;
        BST4Phase2 = 1;

        BST1Phase3 = 1;
        BST2Phase3 = 1;
        BST3Phase3 = 1;
        BST4Phase3 = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Phase = canvas.Phase;


        if (BST1P1done == true || BST2P1done == true || BST3P1done == true || BST4P1done == true)
        {
            choice2int = "new";
        }

        if (PG2BST1P1done == true || PG2BST2P1done == true || PG2BST3P1done == true || PG2BST4P1done == true)
		{
            PG2choice2int = "new";
        }

        if (PG3BST1P1done == true || PG3BST2P1done == true || PG3BST3P1done == true || PG3BST4P1done == true)
        {
            PG3choice2int = "new";
        }

        if (Input.GetKeyDown(KeyCode.E))
		{
            ItemHandler(1);
            ItemHandler(2);
            ItemHandler(3);
        }

    }

    public void ItemHandler(int i)
	{
		switch (i)
		{
            case 1:
                // mett in inventario
                letterInt = "yes";
                break;
            case 2:
                // mett in inventario
                seedInt = "yes";
                break;
            case 3:
                // mett in inventario
                toothInt = "yes";
                break;
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
            //current spiga find
            //chiama funzione nello script di quella spiga per alzare o abbassare
            //se in quella spiga si passa un threshold interno, attiva un bool qua che triggera una riuscita della choice1
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

                PGUnavail = "MATTINA";

                StartCoroutine(dialogueP2Routine(PGUnavail));
                break;
            case 3:
                // File reading works correctly
                fileContents = levelSource3.ToString();
                //Debug.Log(fileContents);

                // TOML table translation
                levelTable = TOML.Parse(new StringReader(fileContents));

                PGUnavail = "NOTTE";

                StartCoroutine(dialogueP3Routine(PGUnavail));
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

        if (PG1lost)
		{
            List<string> introTextLOSE = getStringList(levelTable["introUNAVAIL"]["introtextlose"].AsArray);
            yield return StartCoroutine(canvas.printDialogue(introTextLOSE, MultiBoxes.DialogueBox));

            actionInProgress = false;
            canvas.EndDialogue();
            yield return null;
        }
		if (PG1win)
		{
            List<string> introTextWIN = getStringList(levelTable["introUNAVAIL"]["introtextwin"].AsArray);
            yield return StartCoroutine(canvas.printDialogue(introTextWIN, MultiBoxes.DialogueBox));

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
        canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
        canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

        while (true)
        {
            if (mainmenu == true) // MAIN MENU
			{
                if (pressedButton == 1)  // INVITO FESTA
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
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2) // BST
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

                if (pressedButton == 3) //ITEM OP
				{
                    canvas.HideChoices();

                    if (letterInt == "yes")
					{
                        canvas.HideChoices();

                        List<string> choice3diag = getStringList(levelTable["choice3"]["choicediag" + choice1int].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(choice3diag, MultiBoxes.DialogueBox));

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["ITEM1"]["choicetext"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["ITEM2"]["choicetext"].ToString(), Boxes.Choice2);
                        canvas.printToTextBox(levelTable["ITEM3"]["choicetext"].ToString(), Boxes.Choice3);
                        canvas.printToTextBox(levelTable["ITEM4"]["choicetext"].ToString(), Boxes.Choice4);

                        ItemMenu = true;
                        mainmenu = false;
                        canvas.AllChoices();
                        pressedButton = 0;
                    }
					else
					{
                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                        canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
                        canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                        mainmenu = true;
                        canvas.AllChoices();
                        pressedButton = 0;
                    }
                    
                }

                if (pressedButton == 4) // ESC
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
                        canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
                        canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                        mainmenu = true;
                        canvas.AllChoices();
                        pressedButton = 0;
					}

					if (BST1Phase == 2 && (PG1currentPhase1P1 != TimeScript.phaseNum || PG1currentDay1P1 != TimeScript.day))
					{
						canvas.HideChoices();

                        // PG1 FELICE
                        canvas.PG1Emoji(2);

                        List<string> BST1P2diag = getStringList(levelTable["BST1P2"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST1P2diag, MultiBoxes.DialogueBox));

                        PG1currentPhase1P2 = TimeScript.phaseNum;
                        PG1currentDay1P2 = TimeScript.day;

                        if (!BST1P2done)
                        {
                            BST1Phase1++;
                            BST1P2done = true;
                        }

                        // PG1 NORMALE
                        canvas.PG1Emoji(1);

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
                        canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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

                        // PG1 preoccupata
                        canvas.PG1Emoji(3);

                        if (SpigaTimeBST4P2PG1 == false)
						{
                            SpigaGauge(1);
                            SpigaTimeBST4P2PG1 = true;
                        }
                        List<string> BST4P2diag = getStringList(levelTable["BST4P2"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST4P2diag, MultiBoxes.DialogueBox));

                        // PG1 normale
                        canvas.PG1Emoji(1);

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
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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

                    // PG1 felice
                    canvas.PG1Emoji(2);

                    if (SpigaTimeBST1P3APG1 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST1P3APG1 = true;
                    }
                    List<string> BST1P3diag2A = getStringList(levelTable["BST1P3"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P3diag2A, MultiBoxes.DialogueBox));

                    // PG1 normale
                    canvas.PG1Emoji(1);

                    choice1int = "new";

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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

                    // PG1 felice
                    canvas.PG1Emoji(2);

                    if (SpigaTimeBST1P3CPG1 == false)
                    {
                        SpigaGauge(1);
                        SpigaTimeBST1P3CPG1 = true;
                    }
                    List<string> BST1P3diag2C = getStringList(levelTable["BST1P3"]["choicediag2C"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P3diag2C, MultiBoxes.DialogueBox));

                    // PG1 normale
                    canvas.PG1Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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

                    // PG1 felice
                    canvas.PG1Emoji(2);

                    if (SpigaTimeBST1P4APG1 == false)
					{
                        SpigaGauge(-1);
                        SpigaTimeBST1P4APG1 = true;
                    }

                    List<string> BST1P4diag2A = getStringList(levelTable["BST1P4"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P4diag2A, MultiBoxes.DialogueBox));


                    // PG1 normale
                    canvas.PG1Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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

                    // PG1 felice
                    canvas.PG1Emoji(2);

                    if (SpigaTimeBST1P4BPG1 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST1P4BPG1 = true;
                    }

                    List<string> BST1P4diag2B = getStringList(levelTable["BST1P4"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P4diag2B, MultiBoxes.DialogueBox));

                    // PG1 normale
                    canvas.PG1Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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

                    // PG1 felice
                    canvas.PG1Emoji(2);

                    if (SpigaTimeBST1P4CPG1 == false)
                    {
                        SpigaGauge(-3);
                        SpigaTimeBST1P4CPG1 = true;
                    }

                    List<string> BST1P4diag2C = getStringList(levelTable["BST1P4"]["choicediag2C"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P4diag2C, MultiBoxes.DialogueBox));

                    // PG1 normale
                    canvas.PG1Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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

                    // PG1 preoccupata
                    canvas.PG1Emoji(3);

                    List<string> BST2P3diag2A = getStringList(levelTable["BST2P3"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST2P3diag2A, MultiBoxes.DialogueBox));

                    // PG1 normale
                    canvas.PG1Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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

                    // PG1 preoccupata
                    canvas.PG1Emoji(3);

                    List<string> BST2P3diag2B = getStringList(levelTable["BST2P3"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST2P3diag2B, MultiBoxes.DialogueBox));

                    // PG1 normale
                    canvas.PG1Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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

                    // PG1 preoccupata
                    canvas.PG1Emoji(3);

                    List<string> BST3P2diag = getStringList(levelTable["BST3P2"]["choicediag2"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST3P2diag, MultiBoxes.DialogueBox));

                    // PG1 normale
                    canvas.PG1Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
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

                    // PG1 felice
                    canvas.PG1Emoji(2);

                    List<string> BST4P2diag = getStringList(levelTable["BST4P2"]["choicediag2"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST4P2diag, MultiBoxes.DialogueBox));

                    // PG1 normale
                    canvas.PG1Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu4 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }

            if (ItemMenu == true)
			{
                if (pressedButton == 1) // ESC
				{
                    canvas.HideChoices();

                    List<string> item1diag = getStringList(levelTable["ITEM1"]["choicediag"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(item1diag, MultiBoxes.DialogueBox));

                    pressedButton = 0;

                    mainmenu = true;
                    ItemMenu = false;
                    canvas.AllChoices();
                    actionInProgress = false;
                    canvas.EndDialogue();
                }

                if (pressedButton == 2)
				{
                    canvas.HideChoices();

                    // PG1 felice
                    canvas.PG1Emoji(2);

                    List<string> item2diag = getStringList(levelTable["ITEM2"]["choicediag"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(item2diag, MultiBoxes.DialogueBox));

                    // PG1 normale
                    canvas.PG1Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["ITEM1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["ITEM2"]["choicetext"].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["ITEM3"]["choicetext"].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["ITEM4"]["choicetext"].ToString(), Boxes.Choice4);

                    //prova1
                    PG1prova1 = true;

                    mainmenu = false;
                    ItemMenu = true;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 3)
                {
                    canvas.HideChoices();

                    // PG1 preoccupata
                    canvas.PG1Emoji(3);

                    List<string> item3diag = getStringList(levelTable["ITEM3"]["choicediag"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(item3diag, MultiBoxes.DialogueBox));


                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["ITEM3"]["choicetext2"].ToString(), Boxes.Choice1);

                    //prova2
                    PG1prova2 = true;

                    mainmenu = false;
                    ItemMenu = false;
                    ItemMenu3 = true;
                    canvas.Choices1();
                    pressedButton = 0;
                }
                if (pressedButton == 4)
                {
                    canvas.HideChoices();

                    if (PG1prova1 == true && PG1prova2 == true) // WIN
					{
                        // PG1 preoccupata
                        canvas.PG1Emoji(3);

                        List<string> item4diagB = getStringList(levelTable["ITEM4"]["choicediagB"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(item4diagB, MultiBoxes.DialogueBox));

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["ITEM4"]["choicetextB2"].ToString(), Boxes.Choice1);

                        mainmenu = false;
                        ItemMenu = false;
                        ItemMenu3 = false;
                        ItemMenu4 = true;
                        canvas.Choices1();
                        pressedButton = 0;
                    }
					else // LOSE
                    {
                        // PG1 preoccupata
                        canvas.PG1Emoji(3);

                        List<string> item4diagA = getStringList(levelTable["ITEM4"]["choicediagA"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(item4diagA, MultiBoxes.DialogueBox));

                        // PG1 normale
                        canvas.PG1Emoji(1);

                        PG1lost = true;

                        pressedButton = 0;

                        mainmenu = true;
                        ItemMenu = false;
                        canvas.AllChoices();
                        actionInProgress = false;
                        canvas.EndDialogue();
                    }

                }
            }

            if (ItemMenu3 == true)
			{
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    List<string> item3diag2 = getStringList(levelTable["ITEM3"]["choicediag2"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(item3diag2, MultiBoxes.DialogueBox));

                    // PG1 normale
                    canvas.PG1Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + letterInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    ItemMenu = false;
                    ItemMenu3 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }

            if (ItemMenu4 == true)
			{
                if (pressedButton == 1)
				{
                    canvas.HideChoices();

                    List<string> item4diagB2 = getStringList(levelTable["ITEM4"]["choicediagB2"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(item4diagB2, MultiBoxes.DialogueBox));

                    // PG1 normale
                    canvas.PG1Emoji(1);

                    PG1win = true;

                    mainmenu = true;
                    ItemMenu = false;
                    ItemMenu3 = false;
                    ItemMenu4 = false;
                    canvas.AllChoices();
                    actionInProgress = false;
                    canvas.EndDialogue();
                    pressedButton = 0;
                }
            }

            yield return null;
        }


    }


    public IEnumerator dialogueP2Routine(string PGUnavail)
    {

        // Wait a frame to allow for initialization of other classes
        yield return null;

        actionInProgress = true;

        if (progressHind2 == false)
        {
            BST1Phase = BST1Phase2;
            BST2Phase = BST2Phase2;
            BST3Phase = BST3Phase2;
            BST4Phase = BST4Phase2;
            progressHind2 = true;
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

        if (PG2lost)
        {
            List<string> introTextLOSE = getStringList(levelTable["introUNAVAIL"]["introtextlose"].AsArray);
            yield return StartCoroutine(canvas.printDialogue(introTextLOSE, MultiBoxes.DialogueBox));

            actionInProgress = false;
            canvas.EndDialogue();
            yield return null;
        }
        if (PG2win)
        {
            List<string> introTextWIN = getStringList(levelTable["introUNAVAIL"]["introtextwin"].AsArray);
            yield return StartCoroutine(canvas.printDialogue(introTextWIN, MultiBoxes.DialogueBox));

            actionInProgress = false;
            canvas.EndDialogue();
            yield return null;
        }


        // Generate and print intro text
        if (firstTime2) // se è la prima volta
        {
            firstTime2 = false;
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
        canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
        canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
        canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

        while (true)
        {
            if (mainmenu == true) // MAIN MENU
            {
                if (pressedButton == 1)  // INVITO FESTA
                {
                    canvas.HideChoices();

                    if (PG2SpigaDONE == true)
					{
                        PG2choice1int = "new";
                        PG2win = true; 
                        
                        SpigaGauge(5);

                        // PG2 arrabbiata
                        canvas.PG2Emoji(3);

                        List<string> choice1diagnew = getStringList(levelTable["choice1"]["choicediag" + PG2choice1int].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(choice1diagnew, MultiBoxes.DialogueBox));

                        // PG2 normale
                        canvas.PG2Emoji(1);

                        pressedButton = 0;

                        mainmenu = true;
                        canvas.AllChoices();
                        actionInProgress = false;
                        canvas.EndDialogue();
                    }
					else
					{
                        PG2choice1int = "";

                        if (SpigaTimeFestaPG2 == false)
                        {
                            SpigaGauge(-1);
                            SpigaTimeFestaPG2 = true;
                        }
                    }

                    List<string> choice1diag = getStringList(levelTable["choice1"]["choicediag" + PG2choice1int].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(choice1diag, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2) // BST
                {
                    canvas.HideChoices();

                    List<string> choice2diag = getStringList(levelTable["choice2"]["choicediag" + PG2choice2int].AsArray);
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

                if (pressedButton == 3) //ITEM OP
                {
                    canvas.HideChoices();

                    if (seedInt == "yes")
                    {
                        canvas.HideChoices();

                        List<string> choice3diag = getStringList(levelTable["choice3"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(choice3diag, MultiBoxes.DialogueBox));

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["ITEM1"]["choicetext"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["ITEM3"]["choicetext"].ToString(), Boxes.Choice2);

                        ItemMenu = true;
                        mainmenu = false;
                        canvas.Choices2();
                        pressedButton = 0;
                    }
                    else
                    {
                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                        canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
                        canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                        mainmenu = true;
                        canvas.AllChoices();
                        pressedButton = 0;
                    }

                }

                if (pressedButton == 4) // ESC
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

                        PG2currentPhase1P1 = TimeScript.phaseNum;
                        PG2currentDay1P1 = TimeScript.day;
                        PG2choice2int = "new";

                        if (!PG2BST1P1done)
                        {
                            BST1Phase2++;
                            PG2BST1P1done = true;
                        }

                        if (SpigaTimeBST1P1PG2 == false)
                        {
                            SpigaGauge(-1);
                            SpigaTimeBST1P1PG2 = true;
                        }

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                        canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
                        canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                        mainmenu = true;
                        canvas.AllChoices();
                        pressedButton = 0;
                    }

                    if (BST1Phase == 2 && (PG2currentPhase1P1 != TimeScript.phaseNum || PG2currentDay1P1 != TimeScript.day))
                    {
                        canvas.HideChoices();

                        List<string> BST1P2diag = getStringList(levelTable["BST1P2"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST1P2diag, MultiBoxes.DialogueBox));

                        PG2currentPhase1P2 = TimeScript.phaseNum;
                        PG2currentDay1P2 = TimeScript.day;

                        if (!PG2BST1P2done)
                        {
                            BST1Phase2++;
                            PG2BST1P2done = true;
                        }

                        if (SpigaTimeBST1P2PG2 == false)
                        {
                            SpigaGauge(1);
                            SpigaTimeBST1P2PG2 = true;
                        }

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST1P2"]["choicetext2A"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["BST1P2"]["choicetext2B"].ToString(), Boxes.Choice2);
                        canvas.printToTextBox(levelTable["BST1P2"]["choicetext2C"].ToString(), Boxes.Choice3);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu1 = true;
                        canvas.Choices3();
                        pressedButton = 0;
                    }

                    if (BST1Phase == 3 && (PG2currentPhase1P2 != TimeScript.phaseNum || PG2currentDay1P2 != TimeScript.day))
                    {
                        canvas.HideChoices();

                        List<string> BST1P3diag = getStringList(levelTable["BST1P3"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST1P3diag, MultiBoxes.DialogueBox));

                        PG2currentPhase1P3 = TimeScript.phaseNum;
                        PG2currentDay1P3 = TimeScript.day;


                        if (!PG2BST1P3done)
                        {
                            BST1Phase2++;
                            PG2BST1P3done = true;
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

                    if (BST1Phase == 4 && (PG2currentPhase1P3 != TimeScript.phaseNum || PG2currentDay1P3 != TimeScript.day))
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
                        BSTMenu1D = true;
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

                        PG2currentPhase2P1 = TimeScript.phaseNum;
                        PG2currentDay2P1 = TimeScript.day;
                        PG2choice2int = "new";

                        if (!PG2BST2P1done)
                        {
                            BST2Phase2++;
                            PG2BST2P1done = true;
                        }

                        if (SpigaTimeBST2P1PG2 == false)
                        {
                            SpigaGauge(-1);
                            SpigaTimeBST2P1PG2 = true;
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

                    if (BST2Phase == 2 && (PG2currentPhase2P1 != TimeScript.phaseNum || PG2currentDay2P1 != TimeScript.day))
                    {
                        canvas.HideChoices();

                        List<string> BST2P2diag = getStringList(levelTable["BST2P2"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST2P2diag, MultiBoxes.DialogueBox));

                        PG2currentPhase2P2 = TimeScript.phaseNum;
                        PG2currentDay2P2 = TimeScript.day;

                        if (!PG2BST2P2done)
                        {
                            BST2Phase2++;
                            PG2BST2P2done = true;
                        }

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                        canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
                        canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                        mainmenu = true;
                        BSTMenu = false;
                        BSTMenu2 = false;
                        canvas.AllChoices();
                        pressedButton = 0;
                    }

                    if (BST2Phase == 3 && (PG2currentPhase2P2 != TimeScript.phaseNum || PG2currentDay2P2 != TimeScript.day))
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

                        PG2currentPhase3P1 = TimeScript.phaseNum;
                        PG2currentDay3P1 = TimeScript.day;
                        PG2choice2int = "new";

                        if (!PG2BST3P1done)
                        {
                            BST3Phase2++;
                            PG2BST3P1done = true;
                        }

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                        canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
                        canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                        mainmenu = true;
                        BSTMenu = false;
                        BSTMenu3 = false;
                        canvas.AllChoices();
                        pressedButton = 0;
                    }

                    if (BST3Phase == 2 && (PG2currentPhase3P1 != TimeScript.phaseNum || PG2currentDay3P1 != TimeScript.day))
                    {
                        canvas.HideChoices();

                        List<string> BST3P2diag = getStringList(levelTable["BST3P2"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST3P2diag, MultiBoxes.DialogueBox));

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST3P2"]["choicetext2A"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["BST3P2"]["choicetext2B"].ToString(), Boxes.Choice2);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu3 = false;
                        BSTMenu3B = true;
                        canvas.Choices2();
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

                        PG2currentPhase4P1 = TimeScript.phaseNum;
                        PG2currentDay4P1 = TimeScript.day;
                        PG2choice2int = "new";

                        if (!PG2BST4P1done)
                        {
                            BST4Phase2++;
                            PG2BST4P1done = true;
                        }

                        if (SpigaTimeBST4P1PG2 == false)
                        {
                            SpigaGauge(-1);
                            SpigaTimeBST4P1PG2 = true;
                        }

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST4P1"]["choicetext2A"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["BST4P1"]["choicetext2B"].ToString(), Boxes.Choice2);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu4 = true;
                        canvas.Choices2();
                        pressedButton = 0;
                    }

                    if (BST4Phase == 2 && (PG2currentPhase4P1 != TimeScript.phaseNum || PG2currentDay4P1 != TimeScript.day))
                    {
                        canvas.HideChoices();

                        // PG2 arrabbiata
                        canvas.PG2Emoji(3);

                        List<string> BST4P2diag = getStringList(levelTable["BST4P2"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST4P2diag, MultiBoxes.DialogueBox));

                        // PG2 normale
                        canvas.PG2Emoji(1);

                        PG2lost = true;

                        pressedButton = 0;

                        mainmenu = true;
                        BSTMenu = false;
                        BSTMenu4 = false;
                        canvas.AllChoices();
                        actionInProgress = false;
                        canvas.EndDialogue();
                    }
                }

            }

            if (BSTMenu1 == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    // PG2 arrabbiata
                    canvas.PG2Emoji(3);

                    List<string> BST1P2diag2A = getStringList(levelTable["BST1P2"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P2diag2A, MultiBoxes.DialogueBox));

                    // PG2 normale
                    canvas.PG2Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu1 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    // PG2 incuriosita
                    canvas.PG2Emoji(2);

                    List<string> BST1P2diag2B = getStringList(levelTable["BST1P2"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P2diag2B, MultiBoxes.DialogueBox));

                    // PG2 normale
                    canvas.PG2Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu1 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 3)
                {
                    canvas.HideChoices();

                    List<string> BST1P2diag2C = getStringList(levelTable["BST1P2"]["choicediag2C"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P2diag2C, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
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

                    // PG2 incuriosita
                    canvas.PG2Emoji(2);

                    if (SpigaTimeBST1P3APG2 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST1P3APG2 = true;
                    }

                    List<string> BST1P3diag2A = getStringList(levelTable["BST1P3"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P3diag2A, MultiBoxes.DialogueBox));

                    // PG2 normale
                    canvas.PG2Emoji(1);

                    choice1int = "new";

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
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

                    // PG2 incuriosita
                    canvas.PG2Emoji(2);

                    if (SpigaTimeBST1P3BPG2 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST1P3BPG2 = true;
                    }

                    List<string> BST1P3diag2B = getStringList(levelTable["BST1P3"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P3diag2B, MultiBoxes.DialogueBox));

                    // PG2 normale
                    canvas.PG2Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
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

                    // PG2 incuriosita
                    canvas.PG2Emoji(2);

                    if (SpigaTimeBST1P3CPG2 == false)
                    {
                        SpigaGauge(1);
                        SpigaTimeBST1P3CPG2 = true;
                    }

                    List<string> BST1P3diag2C = getStringList(levelTable["BST1P3"]["choicediag2C"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P3diag2C, MultiBoxes.DialogueBox));

                    // PG2 normale
                    canvas.PG2Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu1 = false;
                    BSTMenu1B = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }

            if (BSTMenu1D == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    // PG2 incuriosita
                    canvas.PG2Emoji(2);

                    if (SpigaTimeBST1P4APG2 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST1P4APG2 = true;
                    }

                    List<string> BST1P4diag2A = getStringList(levelTable["BST1P4"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P4diag2A, MultiBoxes.DialogueBox));


                    // PG2 normale
                    canvas.PG2Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
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

                    if (SpigaTimeBST1P4BPG2 == false)
                    {
                        SpigaGauge(1);
                        SpigaTimeBST1P4BPG2 = true;
                    }

                    List<string> BST1P4diag2B = getStringList(levelTable["BST1P4"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P4diag2B, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
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

                    // PG2 arrabbiata
                    canvas.PG2Emoji(3);

                    if (SpigaTimeBST1P4CPG2 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST1P4CPG2 = true;
                    }

                    List<string> BST1P4diag2C = getStringList(levelTable["BST1P4"]["choicediag2C"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P4diag2C, MultiBoxes.DialogueBox));

                    // PG2 normale
                    canvas.PG2Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
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
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
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

                    if (SpigaTimeBST2P3APG2 == false)
                    {
                        SpigaGauge(2);
                        SpigaTimeBST2P3APG2 = true;
                    }

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
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

                    // PG2 arrabbiata
                    canvas.PG2Emoji(3);

                    List<string> BST2P3diag2B = getStringList(levelTable["BST2P3"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST2P3diag2B, MultiBoxes.DialogueBox));

                    if (SpigaTimeBST2P3BPG2 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST2P3BPG2 = true;
                    }

                    // PG2 normale
                    canvas.PG2Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu2 = false;
                    BSTMenu2B = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }


            if (BSTMenu3B == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    // PG2 arrabbiata
                    canvas.PG2Emoji(3);

                    List<string> BST3P2diag2A = getStringList(levelTable["BST3P2"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST3P2diag2A, MultiBoxes.DialogueBox));

                    if (SpigaTimeBST2P2APG2 == false)
                    {
                        SpigaGauge(1);
                        SpigaTimeBST2P2APG2 = true;
                    }

                    // PG2 normale
                    canvas.PG2Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu3 = false;
                    BSTMenu3B = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    List<string> BST3P2diag2B = getStringList(levelTable["BST3P2"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST3P2diag2B, MultiBoxes.DialogueBox));

                    if (SpigaTimeBST2P2BPG2 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST2P2BPG2 = true;
                    }


                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
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

                    List<string> BST4P1diag2A = getStringList(levelTable["BST4P1"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST4P1diag2A, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu4 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    List<string> BST4P1diag2B = getStringList(levelTable["BST4P1"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST4P1diag2B, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG2choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + seedInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu4 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }


            if (ItemMenu == true)
            {
                if (pressedButton == 1) // ESC
                {
                    canvas.HideChoices();

                    List<string> item1diag = getStringList(levelTable["ITEM1"]["choicediag"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(item1diag, MultiBoxes.DialogueBox));

                    pressedButton = 0;

                    mainmenu = true;
                    ItemMenu = false;
                    canvas.AllChoices();
                    actionInProgress = false;
                    canvas.EndDialogue();
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    List<string> item3diag = getStringList(levelTable["ITEM3"]["choicediag"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(item3diag, MultiBoxes.DialogueBox));


                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["ITEM3"]["choicetext2"].ToString(), Boxes.Choice1);


                    mainmenu = false;
                    ItemMenu = false;
                    ItemMenu3 = true;
                    canvas.Choices1();
                    pressedButton = 0;
                }

            }

            if (ItemMenu3 == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    // PG2 incuriosita
                    canvas.PG2Emoji(2);

                    List<string> item3diag2 = getStringList(levelTable["ITEM3"]["choicediag2"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(item3diag2, MultiBoxes.DialogueBox));

                    // PG2 normale
                    canvas.PG2Emoji(1);

                    PG2win = true;

                    mainmenu = true;
                    ItemMenu = false;
                    ItemMenu3 = false;
                    ItemMenu4 = false;
                    canvas.AllChoices();
                    actionInProgress = false;
                    canvas.EndDialogue();
                    pressedButton = 0;
                }
            }

            yield return null;
        }


    }

    public IEnumerator dialogueP3Routine(string PGUnavail)
    {

        // Wait a frame to allow for initialization of other classes
        yield return null;

        actionInProgress = true;

        if (progressHind3 == false)
        {
            BST1Phase = BST1Phase3;
            BST2Phase = BST2Phase3;
            BST3Phase = BST3Phase3;
            BST4Phase = BST4Phase3;
            progressHind2 = true;
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

        if (PG3lost)
        {
            List<string> introTextLOSE = getStringList(levelTable["introUNAVAIL"]["introtextlose"].AsArray);
            yield return StartCoroutine(canvas.printDialogue(introTextLOSE, MultiBoxes.DialogueBox));

            actionInProgress = false;
            canvas.EndDialogue();
            yield return null;
        }
        if (PG3win)
        {
            List<string> introTextWIN = getStringList(levelTable["introUNAVAIL"]["introtextwin"].AsArray);
            yield return StartCoroutine(canvas.printDialogue(introTextWIN, MultiBoxes.DialogueBox));

            actionInProgress = false;
            canvas.EndDialogue();
            yield return null;
        }


        // Generate and print intro text
        if (firstTime3) // se è la prima volta
        {
            firstTime3 = false;
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
        canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
        canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
        canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

        while (true)
        {
            if (mainmenu == true) // MAIN MENU
            {
                if (pressedButton == 1)  // INVITO FESTA
                {
                    canvas.HideChoices();

                    if (PG3SpigaDONE == true)
                    {
                        PG3choice1int = "new";
                        PG3win = true;

                        SpigaGauge(5);

                        // PG3 FN
                        canvas.PG3Emoji(2);

                        List<string> choice1diagnew = getStringList(levelTable["choice1"]["choicediag" + PG3choice1int].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(choice1diagnew, MultiBoxes.DialogueBox));

                        // PG3 N
                        canvas.PG3Emoji(1);

                        pressedButton = 0;

                        mainmenu = true;
                        canvas.AllChoices();
                        actionInProgress = false;
                        canvas.EndDialogue();
                    }
                    else
                    {
                        PG3choice1int = "";

                        if (SpigaTimeFestaPG3 == false)
                        {
                            SpigaGauge(-1);
                            SpigaTimeFestaPG3 = true;
                        }
                    }

                    List<string> choice1diag = getStringList(levelTable["choice1"]["choicediag" + PG3choice1int].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(choice1diag, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2) // BST
                {
                    canvas.HideChoices();

                    List<string> choice2diag = getStringList(levelTable["choice2"]["choicediag" + PG3choice2int].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(choice2diag, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortextBST"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["BST1P1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["BST2P" + BST2Phase]["choicetext"].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["BST3P" + BST3Phase]["choicetext"].ToString(), Boxes.Choice3);

                    BSTMenu = true;
                    mainmenu = false;
                    canvas.Choices3();
                    pressedButton = 0;
                }

                if (pressedButton == 3) //ITEM OP
                {
                    canvas.HideChoices();


                    if (PG3bad)
                    {
                        // PG3 AF
                        canvas.PG3Emoji(4);

                        List<string> item3diagBad = getStringList(levelTable["ITEM3"]["choicediagbad"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(item3diagBad, MultiBoxes.DialogueBox));

                        // PG3 N
                        canvas.PG3Emoji(1);

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                        canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
                        canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                        mainmenu = true;
                        canvas.AllChoices();
                        pressedButton = 0;
                    }

                    if (toothInt == "yes" && !PG3bad)
                    {

                        // PG3 FN
                        canvas.PG3Emoji(2);

                        List<string> choice3diag = getStringList(levelTable["choice3"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(choice3diag, MultiBoxes.DialogueBox));

                        // PG3 N
                        canvas.PG3Emoji(1);

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["ITEM1"]["choicetext"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["ITEM2"]["choicetext"].ToString(), Boxes.Choice2);
                        canvas.printToTextBox(levelTable["ITEM3"]["choicetext"].ToString(), Boxes.Choice3);

                        ItemMenu = true;
                        mainmenu = false;
                        canvas.Choices3();
                        pressedButton = 0;
                    }
                    else
                    {
                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                        canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
                        canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                        mainmenu = true;
                        canvas.AllChoices();
                        pressedButton = 0;
                    }

                }

                if (pressedButton == 4) // ESC
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

                    canvas.HideChoices();

                    // PG3 FA
                    canvas.PG3Emoji(3);

                    List<string> BST1P1diag = getStringList(levelTable["BST1P1"]["choicediag"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P1diag, MultiBoxes.DialogueBox));

                    // PG3 N
                    canvas.PG3Emoji(1);

                    PG3currentPhase1P1 = TimeScript.phaseNum;
                    PG3currentDay1P1 = TimeScript.day;
                    PG3choice2int = "new";

                    if (!PG3BST1P1done)
                    {
                        BST1Phase3++;
                        PG3BST1P1done = true;
                    }

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["BST1P1"]["choicetext2A"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["BST1P1"]["choicetext2B"].ToString(), Boxes.Choice2);

                    mainmenu = false;
                    BSTMenu = false;
                    BSTMenu1 = true;
                    canvas.Choices2();
                    pressedButton = 0;

                }

                if (pressedButton == 2)
                {
                    if (BST2Phase == 1)
                    {
                        canvas.HideChoices();

                        List<string> BST2P1diag = getStringList(levelTable["BST2P1"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST2P1diag, MultiBoxes.DialogueBox));

                        PG3currentPhase2P1 = TimeScript.phaseNum;
                        PG3currentDay2P1 = TimeScript.day;
                        PG3choice2int = "new";

                        if (!PG3BST2P1done)
                        {
                            BST2Phase3++;
                            PG3BST2P1done = true;
                        }

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST2P1"]["choicetext2A"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["BST2P1"]["choicetext2B"].ToString(), Boxes.Choice2);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu1 = false;
                        BSTMenu2 = true;
                        canvas.Choices2();
                        pressedButton = 0;
                    }

                    if (BST2Phase == 2 && (PG3currentPhase2P1 != TimeScript.phaseNum || PG3currentDay2P1 != TimeScript.day))
                    {
                        canvas.HideChoices();

                        List<string> BST2P2diag = getStringList(levelTable["BST2P2"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST2P2diag, MultiBoxes.DialogueBox));

                        PG3currentPhase2P2 = TimeScript.phaseNum;
                        PG3currentDay2P2 = TimeScript.day;

                        if (!PG3BST2P2done)
                        {
                            BST2Phase3++;
                            PG3BST2P2done = true;
                        }


                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST2P2"]["choicetext2A"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["BST2P2"]["choicetext2B"].ToString(), Boxes.Choice2);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu1 = false;
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

                        PG3currentPhase3P1 = TimeScript.phaseNum;
                        PG3currentDay3P1 = TimeScript.day;
                        PG3choice2int = "new";

                        if (!PG3BST3P1done)
                        {
                            BST3Phase3++;
                            PG3BST3P1done = true;
                        }

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST3P1"]["choicetext2A"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["BST3P1"]["choicetext2B"].ToString(), Boxes.Choice2);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu3 = true;
                        canvas.Choices2();
                        pressedButton = 0;
                    }

                    if (BST3Phase == 2 && (PG3currentPhase3P1 != TimeScript.phaseNum || PG3currentDay3P1 != TimeScript.day))
                    {
                        canvas.HideChoices();

                        List<string> BST3P2diag = getStringList(levelTable["BST3P2"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST3P2diag, MultiBoxes.DialogueBox));

                        if (!PG3BST3P2done)
                        {
                            BST3Phase3++;
                            PG3BST3P2done = true;
                        }

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST3P2"]["choicetext2A"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["BST3P2"]["choicetext2B"].ToString(), Boxes.Choice2);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu3 = false;
                        BSTMenu3B = true;
                        canvas.Choices2();
                        pressedButton = 0;
                    }

                    if (BST3Phase == 3 && (PG3currentPhase3P2 != TimeScript.phaseNum || PG3currentDay3P2 != TimeScript.day))
                    {
                        canvas.HideChoices();

                        List<string> BST3P2diag = getStringList(levelTable["BST3P2"]["choicediag"].AsArray);
                        yield return StartCoroutine(canvas.printDialogue(BST3P2diag, MultiBoxes.DialogueBox));

                        canvas.ShowChoices();
                        canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                        canvas.printToTextBox(levelTable["BST3P3"]["choicetext2A"].ToString(), Boxes.Choice1);
                        canvas.printToTextBox(levelTable["BST3P3"]["choicetext2B"].ToString(), Boxes.Choice2);
                        canvas.printToTextBox(levelTable["BST3P3"]["choicetext2C"].ToString(), Boxes.Choice2);

                        mainmenu = false;
                        BSTMenu = false;
                        BSTMenu3 = false;
                        BSTMenu3B = false;
                        BSTMenu3C = true;
                        canvas.Choices3();
                        pressedButton = 0;
                    }
                }

            }

            if (BSTMenu1 == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    // PG3 FA
                    canvas.PG3Emoji(3);

                    List<string> BST1P1diag2A = getStringList(levelTable["BST1P1"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P1diag2A, MultiBoxes.DialogueBox));

                    if (SpigaTimeBST1P2APG3 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST1P2APG3 = true;
                    }

                    // PG3 N
                    canvas.PG3Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu1 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    // PG3 NF
                    canvas.PG3Emoji(2);

                    List<string> BST1P1diag2B = getStringList(levelTable["BST1P1"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST1P1diag2B, MultiBoxes.DialogueBox));

                    if (SpigaTimeBST1P2BPG3 == false)
                    {
                        SpigaGauge(1);
                        SpigaTimeBST1P2BPG3 = true;
                    }

                    // PG3 N
                    canvas.PG3Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu1 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }


            }


            if (BSTMenu2 == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    List<string> BST2P1diag2A = getStringList(levelTable["BST2P1"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST2P1diag2A, MultiBoxes.DialogueBox));

                    if (SpigaTimeBST2P1APG3 == false)
                    {
                        SpigaGauge(1);
                        SpigaTimeBST2P1APG3 = true;
                    }

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu2 = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    // PG3 AF
                    canvas.PG3Emoji(4);

                    List<string> BST2P1diag2B = getStringList(levelTable["BST2P1"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST2P1diag2B, MultiBoxes.DialogueBox));

                    if (SpigaTimeBST2P1BPG3 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST2P1BPG3 = true;
                    }

                    // PG3 N
                    canvas.PG3Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
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

                    List<string> BST2P2diag2A = getStringList(levelTable["BST2P2"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST2P2diag2A, MultiBoxes.DialogueBox));

                    if (SpigaTimeBST2P2APG3 == false)
                    {
                        SpigaGauge(1);
                        SpigaTimeBST2P2APG3 = true;
                    }

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
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

                    // PG3 AF
                    canvas.PG3Emoji(4);

                    List<string> BST2P2diag2B = getStringList(levelTable["BST2P2"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST2P2diag2B, MultiBoxes.DialogueBox));

                    if (SpigaTimeBST2P2BPG3 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST2P2BPG3 = true;
                    }

                    // PG3 N
                    canvas.PG3Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
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

                    List<string> BST3P1diag2A = getStringList(levelTable["BST3P1"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST3P1diag2A, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu3 = false;
                    BSTMenu3B = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    List<string> BST3P1diag2B = getStringList(levelTable["BST3P1"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST3P1diag2B, MultiBoxes.DialogueBox));


                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu3 = false;
                    BSTMenu3B = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }

            if (BSTMenu3B == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    List<string> BST3P2diag2A = getStringList(levelTable["BST3P2"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST3P2diag2A, MultiBoxes.DialogueBox));

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu3 = false;
                    BSTMenu3B = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    // PG3 FA
                    canvas.PG3Emoji(3);

                    List<string> BST3P2diag2B = getStringList(levelTable["BST3P2"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST3P2diag2B, MultiBoxes.DialogueBox));


                    if (SpigaTimeBST3P3APG3 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST3P3APG3 = true;
                    }

                    // PG3 N
                    canvas.PG3Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu3 = false;
                    BSTMenu3B = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }

            if (BSTMenu3C == true)
			{
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    // PG3 FA
                    canvas.PG3Emoji(3);

                    List<string> BST3P3diag2A = getStringList(levelTable["BST3P3"]["choicediag2A"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST3P3diag2A, MultiBoxes.DialogueBox));

                    if (SpigaTimeBST3P3BPG3 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST3P3BPG3 = true;
                    }

                    // PG3 N
                    canvas.PG3Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu3 = false;
                    BSTMenu3B = false;
                    BSTMenu3C = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    // PG3 FA
                    canvas.PG3Emoji(3);

                    List<string> BST3P3diag2B = getStringList(levelTable["BST3P3"]["choicediag2B"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST3P3diag2B, MultiBoxes.DialogueBox));


                    if (SpigaTimeBST3P3CPG3 == false)
                    {
                        SpigaGauge(-1);
                        SpigaTimeBST3P3CPG3 = true;
                    }

                    // PG3 N
                    canvas.PG3Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu3 = false;
                    BSTMenu3B = false;
                    BSTMenu3C = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }

                if (pressedButton == 3)
                {
                    canvas.HideChoices();

                    // PG3 AF
                    canvas.PG3Emoji(4);

                    List<string> BST3P3diag2C = getStringList(levelTable["BST3P3"]["choicediag2C"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(BST3P3diag2C, MultiBoxes.DialogueBox));


                    if (SpigaTimeBST3P2BPG3 == false)
                    {
                        SpigaGauge(1);
                        SpigaTimeBST3P2BPG3 = true;
                    }

                    // PG3 N
                    canvas.PG3Emoji(1);

                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortext"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["choice1"]["choicetext"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["choice2"]["choicetext" + PG3choice2int].ToString(), Boxes.Choice2);
                    canvas.printToTextBox(levelTable["choice3"]["choicetext" + toothInt].ToString(), Boxes.Choice3);
                    canvas.printToTextBox(levelTable["choice4"]["choicetext"].ToString(), Boxes.Choice4);

                    mainmenu = true;
                    BSTMenu = false;
                    BSTMenu3 = false;
                    BSTMenu3B = false;
                    BSTMenu3C = false;
                    canvas.AllChoices();
                    pressedButton = 0;
                }
            }


            if (ItemMenu == true)
            {
                if (pressedButton == 1) // ESC
                {
                    canvas.HideChoices();

                    List<string> item1diag = getStringList(levelTable["ITEM1"]["choicediag"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(item1diag, MultiBoxes.DialogueBox));



                    pressedButton = 0;

                    mainmenu = true;
                    ItemMenu = false;
                    canvas.AllChoices();
                    actionInProgress = false;
                    canvas.EndDialogue();
                }

                if (pressedButton == 2) // BAD
                {
                    canvas.HideChoices();

                    List<string> item2diag = getStringList(levelTable["ITEM2"]["choicediag"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(item2diag, MultiBoxes.DialogueBox));

                    PG3bad = true;

                    pressedButton = 0;

                    mainmenu = true;
                    ItemMenu = false;
                    canvas.AllChoices();
                    actionInProgress = false;
                    canvas.EndDialogue();
                }

                if (pressedButton == 3)
                {
                    canvas.HideChoices();

                    List<string> item3diag = getStringList(levelTable["ITEM3"]["choicediag"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(item3diag, MultiBoxes.DialogueBox));


                    canvas.ShowChoices();
                    canvas.printToTextBox(levelTable["intro"]["flavortextEMPTY"].ToString(), Boxes.DialogueBox);
                    canvas.printToTextBox(levelTable["ITEM3"]["choicetextA"].ToString(), Boxes.Choice1);
                    canvas.printToTextBox(levelTable["ITEM3"]["choicetextB"].ToString(), Boxes.Choice2);


                    mainmenu = false;
                    ItemMenu = false;
                    ItemMenu3 = true;
                    canvas.Choices2();
                    pressedButton = 0;
                }

            }

            if (ItemMenu3 == true)
            {
                if (pressedButton == 1)
                {
                    canvas.HideChoices();

                    // PG3 NF
                    canvas.PG3Emoji(2);

                    List<string> item3diagA = getStringList(levelTable["ITEM3"]["choicediagA"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(item3diagA, MultiBoxes.DialogueBox));

                    // PG3 N
                    canvas.PG3Emoji(1);

                    PG3win = true;

                    mainmenu = true;
                    ItemMenu = false;
                    ItemMenu3 = false;
                    canvas.AllChoices();
                    actionInProgress = false;
                    canvas.EndDialogue();
                    pressedButton = 0;
                }

                if (pressedButton == 2)
                {
                    canvas.HideChoices();

                    List<string> item3diagB = getStringList(levelTable["ITEM3"]["choicediagB"].AsArray);
                    yield return StartCoroutine(canvas.printDialogue(item3diagB, MultiBoxes.DialogueBox));

                    PG3lost = true;

                    mainmenu = true;
                    ItemMenu = false;
                    ItemMenu3 = false;
                    canvas.AllChoices();
                    actionInProgress = false;
                    canvas.EndDialogue();
                    pressedButton = 0;
                }
            }

            yield return null;
        }


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
