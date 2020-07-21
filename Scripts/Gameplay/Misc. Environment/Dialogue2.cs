//This script is for the scene called "Narrative2"
//It delivers instructions to the subjects about how to pick up the baby

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Dialogue2 : MonoBehaviour
{
    public static int Advances = 0;     //how many times the subject has clicked (to read instructions)

    public GameObject DialogueBox;      //the canvas gameobject that has all the instructions elements

    public GameObject DialogueText;     //the written instructions
    public GameObject ClicktoContinue;  //small text item that lets subjects know how to advance

    public Button repeatInstructions;   //button to repeat instructions over
    public Button imReady;              //button to advance to next scene after instructions
    public GameObject SpeedBox;


    void Start()
    {
        //for when subjects need to repeat instructions or move on after the instructions
        Button repeat = repeatInstructions.GetComponent<Button>();
        repeat.onClick.AddListener(startover);

        Button moveon = imReady.GetComponent<Button>();
        moveon.onClick.AddListener(GoToBabyScene);

        //make sure only the correct items are showing at start
        SpeedBox.gameObject.SetActive(false);
        repeatInstructions.gameObject.SetActive(false);
        imReady.gameObject.SetActive(false);
        ClicktoContinue.gameObject.SetActive(true);


        //hides the mouse and the camera is locked to mouse tracking
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        //when subs click, they advance the instructions dialogue
        if (Input.GetMouseButtonDown(0) && Advances != 4 && Advances != 5) 
        {
            Advances += 1;
        }
        if (Advances == 0)
            {DialogueText.GetComponent<Text>().text = "You were able to flag down 10 cars before your baby woke up and wanted out of their carseat."; }
        else if (Advances == 1)
            {DialogueText.GetComponent<Text>().text = "You still need to keep an eye on the cars, so you let your baby out to crawl on the blanket with some toys.";}
        else if (Advances == 2)
            {DialogueText.GetComponent<Text>().text = "If your baby gets too close to the road, you can always pick them up and bring them back in."; }
        else if (Advances == 3)
            {DialogueText.GetComponent<Text>().text = "However, if you are picking up your baby, you might not see oncoming cars."; }
        else if (Advances == 4) //will not advance until they pick the baby up (see "PickUpBaby" script)
            {
            DialogueText.GetComponent<Text>().text = "Press [B] to pick up your baby."
                + Environment.NewLine + "Try it now!";
            ClicktoContinue.gameObject.SetActive(false);
        }
        else if (Advances == 5) //will not advance without pressing a button
            {
                DialogueText.GetComponent<Text>().text = "Only 10 more cars to go!" + Environment.NewLine + "Are you Ready?";

                //ends isntructions and turns on the buttons
                repeatInstructions.gameObject.SetActive(true);
                imReady.gameObject.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                ClicktoContinue.gameObject.SetActive(false);
            }

        }

        //the subject has opted to repeat all the instructions
        void startover()
        {
            Advances = 0;
            TutorialCarCycle.scoreValue = 0;

            repeatInstructions.gameObject.SetActive(false);
            imReady.gameObject.SetActive(false);
        ClicktoContinue.gameObject.SetActive(true);
    }

        //the subject has opted to move on to the next game scene
        void GoToBabyScene()
        {
            SceneManager.LoadScene(6);
        }

    }
