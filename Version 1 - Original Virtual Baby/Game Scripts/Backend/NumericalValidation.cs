//this script is to make sure that subjects aren't putting in innappropriate mph entries
//they must be numerical and above 0

//the numerical validation is set by inhibiting non-numeric key presses
//this is done in the inputfield investigation panel where "Content Type" = Integer

using UnityEngine;
using System;
using UnityEngine.UI;

public class NumericalValidation : MonoBehaviour
{
    public InputField mphInputField;
    public GameObject ValidationAlert;


    public void Start()
    {
        mphInputField.text = "0"; //this is done to prevent errors
        ValidationAlert.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Convert.ToInt32(mphInputField.text) > 0 && Convert.ToInt32(mphInputField.text) < 200)
        {
            ValidationAlert.gameObject.SetActive(false);
        }
        else
        {
            ValidationAlert.gameObject.SetActive(true);
        }
    }
}
