//the script advances the subjects through the informed consent
//giving them the option to opt out or continue
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InformedConsent : MonoBehaviour
{
    public static int Advances = 0;

    public Button DoConsent;
    public Button DoNotConsent;
    public Button Next;

    public GameObject InformedConsentP0;
    public GameObject InformedConsentP1;
    public GameObject InformedConsentP2;
    public GameObject InformedConsentP3;
    public GameObject InformedConsentP4;

    void Start()
    {   
        //adds triggers to all the button objects
        Button repeat = Next.GetComponent<Button>();
        repeat.onClick.AddListener(NextPage);

        Button moveon = DoConsent.GetComponent<Button>();
        moveon.onClick.AddListener(GoToRegistration);

        Button submitmph = DoNotConsent.GetComponent<Button>();
        submitmph.onClick.AddListener(EndTheGame);

        //makes sure that only the correct pages are showing at the start
        InformedConsentP1.gameObject.SetActive(false);
        InformedConsentP2.gameObject.SetActive(false);
        InformedConsentP3.gameObject.SetActive(false);
        InformedConsentP4.gameObject.SetActive(false);
        DoConsent.gameObject.SetActive(false);
        DoNotConsent.gameObject.SetActive(false);
    }

    void NextPage()
    {
        Advances += 1;
    }

    void GoToRegistration()
    {
        SceneManager.LoadScene(2);
    }

    void EndTheGame()
    {
        Application.Quit();
    }


    void Update()
    {
        if (Advances == 1)
        {
            InformedConsentP0.gameObject.SetActive(false);
            InformedConsentP1.gameObject.SetActive(true);
        }
        else if (Advances == 2)
        {
            InformedConsentP1.gameObject.SetActive(false);
            InformedConsentP2.gameObject.SetActive(true);
        }
        else if (Advances == 3)
        {
            InformedConsentP2.gameObject.SetActive(false);
            InformedConsentP3.gameObject.SetActive(true);
        }
        else if (Advances == 4)
        {
            InformedConsentP3.gameObject.SetActive(false);
            InformedConsentP4.gameObject.SetActive(true);
            Next.gameObject.SetActive(false);
            DoConsent.gameObject.SetActive(true);
            DoNotConsent.gameObject.SetActive(true);
        }
    }
}
