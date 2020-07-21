//This script programs how the car move down the path during the NeutralScene and the BabyScene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CarCycle : MonoBehaviour
{
    //beginning and end of car path
    //there are collider objects at these points that the car moves towards/returns to
    public Transform theOrigin;
    public Transform theDestination;
    public bool ReachedDestination = false;
    public bool CarAtOrigin = true;

    public int speed; //how fast the car is moving
    public List<int> speedslist1 = new List<int>() { 30, 40, 50, 60, 70, 30, 40, 50, 60, 70 };
    public List<int> speedslist2 = new List<int>() { }; //empty list to move all of speedslist1 into for 2nd block

    public static int scoreValue = 0; //how many times the player correctly hit SPACE to wave down cars
    public bool AlreadyScored = false; //prevents subs from spamming SPACE more than once per trial
    public int trial = 1; //increases each time the car loops back over (includes both "misses" and "hits")

    public int randomdelay; //to make car onset unpredictable to viewers
    public string carlocation; //logs beginning and end of car transit to track timing

    //these elements are also in the "Dialogue" script, and control the speed entry box
    public GameObject SpeedBox;
    public Button enterMPH;
    public InputField mphInputField;

    //outside scripts that get referenced
    public AdvanceScenes advancescenes;
    public NeutralScene neutralscene;
    public BabyScene babyscene;

    //the hand visual when the car is flagged down
    public GameObject theHand;

    void Awake()
    {
        //chooses a random wait time for the first trial
        StartCoroutine("RandomWaitDelay");
    }

    void Start()
    {
        //chooses the first speed to start
        speed = speedslist1[Random.Range(0, (speedslist1.Count - 1))];

        //turns off speed entry box
        SpeedBox.gameObject.SetActive(false);

        //turns on trigger when sub enters their speed estimation
        Button submitmph = enterMPH.GetComponent<Button>();
        submitmph.onClick.AddListener(StartLoopOver);

        //makes sure the hand object is turned off until needed
        theHand.gameObject.SetActive(false);

    }

    void Update()
    {
        //moves car towards destination
        if (ReachedDestination == false && CarAtOrigin == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                theDestination.position, Time.deltaTime * speed);
            
        }

        //adds a point to score if they press SPACE while the car is moving
        if (Input.GetKeyDown(KeyCode.Space) && ReachedDestination == false &&
            CarAtOrigin == false && AlreadyScored == false)
        {
            scoreValue += 1;
            AlreadyScored = true; //prevents re-scoring

            //turns on the waving hand object
            StartCoroutine("WaveTheHand");

        }

        //when the car reaches destination, stop and answer mph question
        if (transform.position == theDestination.position)
        {
            ReachedDestination = true;
            SpeedBox.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //if the car reaches the end of the track, it gets updated and logged
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "CarDestination")
        {
            carlocation = "Car Ends";
            if (advancescenes.block == "4.NeutralScene")
            {
                neutralscene.GetCarLocation();
            }
            else if (advancescenes.block == "6.BabyScene")
            {
                babyscene.GetCarLocation();
            }

        }
    }

    //When the car resets, it has to wait for a random amount of time (currently between 5-10 seconds)
    IEnumerator RandomWaitDelay()
    {
        CarAtOrigin = true;
        randomdelay = Random.Range(5, 10);
        yield return new WaitForSeconds(randomdelay);

        CarAtOrigin = false;
        mphInputField.text = "0";
        carlocation = "Car Starts";
        if (advancescenes.block == "4.NeutralScene")
        {
            neutralscene.GetCarLocation();
        }
        else if (advancescenes.block == "6.BabyScene")
        {
            babyscene.GetCarLocation();
        }
    }


    //the loop starts over after each trial
    public void StartLoopOver()
    {
        //this stuff happens everytime...
        trial += 1;
        StartCoroutine("RandomWaitDelay");
        speed = speedslist1[Random.Range(0, (speedslist1.Count - 1))];
        SpeedBox.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ReachedDestination = false;
        transform.position = theOrigin.transform.position;

        //if the sub has scored 10 times, they have to move on to the next set of instructions
        if (scoreValue == 10)
        { advancescenes.GoToBabyInstructions(); }

        //otherwise here is how the speeds are randomly selected without replacement
        //if they do not score, that speed remains on the list to ensure that they see it
        else if (scoreValue < 20 && scoreValue != 10)
        {
            //randomly determines speed of the car from "speedslist"
            int speedslist1index = Random.Range(1, speedslist1.Count);
            int temp1 = speedslist1[speedslist1index];
            //and moves it to the second list only if they waved the car down
            if (AlreadyScored == true)
            {
                speedslist2.Add(temp1);
                speedslist1.RemoveAt(speedslist1index);
            }
        }
        else if (scoreValue == 20)
        {
            { advancescenes.GoToEndGame(); }
        }
        AlreadyScored = false;
    }

    IEnumerator WaveTheHand()
    {
        theHand.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        theHand.gameObject.SetActive(false);
    }
}
