//this script is to log data in the scene called "Narrative2" (i.e., tutorial for picking up baby)
using System.Collections;
using UnityEngine;

public class Narrative2 : MonoBehaviour
{
    public string EVENTNAME;
    public string RESPONSENAME;
    public string BLOCKNAME;
    public string CARSPEED;
    public string BABYLOCATION;
    public string TRIAL;

    public DBConnect dbconnect;
    public TutorialCarCycle tutorialcarcycle;
    public AdvanceScenes advancescenes;
    public Dialogue2 dialogue2;
    public PickUpBaby pickupbaby;
    public CrossingThreshold crossingthreshold;


    void Start()
    {
        //registers current scene for data logging
        advancescenes.CurrentBlock();
        BLOCKNAME = advancescenes.block;
        EVENTNAME = "Start New Block";
        dbconnect.Insert();


        //uses button as tricgger to add see if subs need to repeat instructions
        dialogue2.repeatInstructions.onClick.AddListener(RepeatInstructions);
        //uses button as tricgger to add see if subs need to repeat instructions
        dialogue2.imReady.onClick.AddListener(DoNotRepeatInstructions);
    }

    private void Update()
    {   //updates the static variables in CombinedData script
        CombinedData.BLOCKNAME = BLOCKNAME;
        CombinedData.CARSPEED = CARSPEED;
        CombinedData.TRIAL = "Tutorial Instructions 2";
        CombinedData.BABYLOCATION = BABYLOCATION;
        CombinedData.EVENTNAME = EVENTNAME;
        CombinedData.RESPONSENAME = RESPONSENAME;


        //updates data if subjects press B to pick up baby
        if (Input.GetKeyDown(KeyCode.B))
        {
            CARSPEED = tutorialcarcycle.speed.ToString();
            EVENTNAME = ("Baby Location: " + pickupbaby.BabyNonNavMesh.transform.position);
            RESPONSENAME = "B Pressed";
            dbconnect.Insert();
        }

        if (crossingthreshold.BlanketCross == true)
        {
            crossingthreshold.BlanketCross = false;
            CARSPEED = tutorialcarcycle.speed.ToString();
            EVENTNAME = "Baby Crossed Blanket";
            RESPONSENAME = "NA";
            dbconnect.Insert();
        }
        if (crossingthreshold.RoadCross == true)
        {
            crossingthreshold.RoadCross = false;
            CARSPEED = tutorialcarcycle.speed.ToString();
            EVENTNAME = "Baby Crossed Road";
            RESPONSENAME = "NA";
            dbconnect.Insert();
        }

        if (crossingthreshold.theBlanket == true)
        {
            BABYLOCATION = "Blanket";
        }
        if (crossingthreshold.theGrass == true)
        {
            BABYLOCATION = "Grass";
        }
        if (crossingthreshold.theRoad == true)
        {
            BABYLOCATION = "Road";
        }
    }

    void RepeatInstructions()
    {   //logs if player needed to repeat the instructions
        EVENTNAME = "Repeat Instructions";
        RESPONSENAME = "Yes";
        dbconnect.Insert();
    }
    void DoNotRepeatInstructions()
    {   //logs if player is ready to continue
        EVENTNAME = "Repeat Instructions";
        RESPONSENAME = "No";
        dbconnect.Insert();
    }

}