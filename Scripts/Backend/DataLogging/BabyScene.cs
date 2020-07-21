//This script is to record data during the block where the baby is freely crawling
//See "CombinedData" script for more details

using System;
using UnityEngine;

public class BabyScene : MonoBehaviour
{
    public string EVENTNAME;
    public string RESPONSENAME;
    public string BLOCKNAME;
    public string CARSPEED;
    public string BABYLOCATION;
    public string TRIAL;

    public DBConnect dbconnect;
    public ScoreKeeper scorekeeper;
    public CarCycle carcycle;
    public PickUpBaby pickupbaby;
    public AdvanceScenes advancescenes;
    public CrossingThreshold crossingthreshold;


    private void Start()
    {
        //registers current scene for data logging
        advancescenes.CurrentBlock();
        BLOCKNAME = advancescenes.block;
        EVENTNAME = "Start New Block";
        dbconnect.Insert();

        //uses button as tricgger to add estimated speed to the data file
        carcycle.enterMPH.onClick.AddListener(GetMPH);
    }

    void Update()
    {   //updates the static variables in CombinedData script
        CombinedData.BLOCKNAME = BLOCKNAME;
        CombinedData.CARSPEED = CARSPEED;
        CombinedData.TRIAL = TRIAL;
        CombinedData.BABYLOCATION = BABYLOCATION;
        CombinedData.EVENTNAME = EVENTNAME;
        CombinedData.RESPONSENAME = RESPONSENAME;

        //updates data if subjects press SPACE to wave down cars
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TRIAL = carcycle.trial.ToString();
            CARSPEED = carcycle.speed.ToString();
            EVENTNAME = "Space Pressed";
            RESPONSENAME = "Score " + (scorekeeper.scoreValue + 1);
            dbconnect.Insert();
        }
        //updates data if subjects press B to pick up the baby
        if (Input.GetKeyDown(KeyCode.B))
        {
            TRIAL = carcycle.trial.ToString();
            CARSPEED = carcycle.speed.ToString();
            EVENTNAME = ("Baby Location: " + pickupbaby.BabyNonNavMesh.transform.position);
            RESPONSENAME = "B Pressed";
            dbconnect.Insert();
        }

        if (crossingthreshold.BlanketCross == true)
        {
            crossingthreshold.BlanketCross = false;
            CARSPEED = carcycle.speed.ToString();
            EVENTNAME = "Baby Crossed Blanket";
            RESPONSENAME = "NA";
            dbconnect.Insert();
        }

        if (crossingthreshold.RoadCross == true)
        {
            crossingthreshold.RoadCross = false;
            CARSPEED = carcycle.speed.ToString();
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

    public void GetCarLocation()
    {   //updates data when the car changes location (starts moving and ends moving)
        TRIAL = carcycle.trial.ToString();
        CARSPEED = carcycle.speed.ToString();
        EVENTNAME = carcycle.carlocation;
        RESPONSENAME = "NA";
        dbconnect.Insert();
    }

    private void GetMPH()
    {   //updates data if subjects submit their estimated mph speed
        TRIAL = (carcycle.trial - 1).ToString();
        CARSPEED = carcycle.speed.ToString();
        EVENTNAME = "Estimated Speed";
        RESPONSENAME = carcycle.mphInputField.text;
        dbconnect.Insert();

    }
}



