//This script is for the scene called "Narrative1"
//It delivers instructions to the subjects about how to wave down the cars

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Dialogue : MonoBehaviour
{
    public static int Advances = 0;     //how many times the subject has clicked (to read instructions)

    public GameObject DialogueBox;      //the canvas gameobject that has all the instructions elements
    public GameObject SpeedBox;         //the canvas gameobject that has all the elements to input the est. car speed
    public GameObject CarScoreBox;      //the canvas gameobject that presents the current score to subjects

    public GameObject DialogueText;     //the written instructions
    public GameObject ClicktoContinue;  //small text item that lets subjects know how to advance

    public Button enterMPH;             //the button to "submit" their estimated speed in mph
    public InputField mphInputField;    //where subjects write out their estimated mph

    public Button repeatInstructions;   //button to repeat instructions over
    public Button imReady;              //button to advance to next scene after instructions

    void Start()
    {
        //for when subjects need to repeat instructions or move on after the instructions
        Button repeat = repeatInstructions.GetComponent<Button>();
        repeat.onClick.AddListener(startover);

        Button moveon = imReady.GetComponent<Button>();
        moveon.onClick.AddListener(GoToTestScene);

        //when they enter their estimated mph, it resolves the practice trial and moves on
        Button submitmph = enterMPH.GetComponent<Button>();
        submitmph.onClick.AddListener(EndPracticeTrial);

        //make sure only the correct items are showing at start
        SpeedBox.gameObject.SetActive(false);
        CarScoreBox.gameObject.SetActive(false);
        repeatInstructions.gameObject.SetActive(false);
        imReady.gameObject.SetActive(false);
        ClicktoContinue.gameObject.SetActive(true);

        //hides the mouse and the camera is locked to mouse tracking
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }


    void Update()
    {   //when subs click, they advance the instructions dialogue
        if (Input.GetMouseButtonDown(0) && Advances != 7 && Advances != 9)
        {
            Advances += 1;
        }

        //note: the dialogue can be written as a "list" method to make it easier on the eyes, but...I'm just lazy
        if (Advances == 0)
        {
            DialogueText.GetComponent<Text>().text = "You were in the car with your infant, taking the backroad home, when you suddenly realized you were completely out of gas.";
        }
        else if (Advances == 1)
        {
            DialogueText.GetComponent<Text>().text = "Your car would go no further.";
        }
        else if (Advances == 2)
        {
            DialogueText.GetComponent<Text>().text = "So you pulled over and tried to call roadside assistance...";
        }
        else if (Advances == 3)
        {
            DialogueText.GetComponent<Text>().text = "..but unfortunately, you have no cell phone signal out here.";
        }
        else if (Advances == 4)
        {
            DialogueText.GetComponent<Text>().text = "You are going to have to flag down cars to see if anyone is willing to help you out.";
        }
        else if (Advances == 5)
        {
            DialogueText.GetComponent<Text>().text = "Fortunately, you had a blanket and folding chair in the back of your car so you can sit as you wait for cars.";
        }
        else if (Advances == 6)
        {
            DialogueText.GetComponent<Text>().text = "Make sure you flag them down as soon as you see them. Go as fast as possible! They might not see you if you wait too long!";
        }
        else if (Advances == 7) //will not advance until they score (see "TutorialCarCycle" script)
        {
            DialogueText.GetComponent<Text>().text = "Press [SPACE] to wave at oncoming cars."
                + Environment.NewLine + "Try it now!";
            CarScoreBox.gameObject.SetActive(true);
            ClicktoContinue.gameObject.SetActive(false);
        }

        else if (Advances == 8)
        {
            DialogueText.GetComponent<Text>().text = "Great! Flag down 20 cars to win the game and get home!";
            ClicktoContinue.gameObject.SetActive(true);
        }
        else if (Advances == 9) //will not advance without pressing a button
        {
            //ends isntructions and turns on the buttons
            DialogueText.GetComponent<Text>().text = "Do these instructions make sense?";
            repeatInstructions.gameObject.SetActive(true);
            imReady.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            ClicktoContinue.gameObject.SetActive(false);
        }

        //waits until subjects have successfully waved down a car to advance
        if (TutorialCarCycle.scoreValue == 1 && Advances == 7)
        {
            DialogueBox.gameObject.SetActive(false);
            SpeedBox.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }

    // the subject has just submitted their practice mph estimation
    public void EndPracticeTrial()
    {
        Advances += 1;
        DialogueBox.gameObject.SetActive(true);
        SpeedBox.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    //the subject has opted to repeat all the instructions
    void startover()
    {
        Advances = 0;
        mphInputField.text = ""; //refreshes the input field
        TutorialCarCycle.scoreValue = 0;

        repeatInstructions.gameObject.SetActive(false);
        imReady.gameObject.SetActive(false);
    }
    
    //the subject has opted to move on to the next game scene
    void GoToTestScene()
    {
        Advances = 0;
        SceneManager.LoadScene(4);
    }
}
