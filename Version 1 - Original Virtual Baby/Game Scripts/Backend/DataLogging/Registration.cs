using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Registration : MonoBehaviour
{
    public InputField IdField; //subject ID
    public InputField ConditionField; //subject condition
    public Button SubmitButton; //submits ID and condition

    public string ID;
    public string CONDITION;
    public string BLOCKNAME;
    public string EVENTNAME;

    public DBConnect dbconnect;
    public AdvanceScenes advancescenes;

    void Start()
    {
        Button submit = SubmitButton.GetComponent<Button>();
        submit.onClick.AddListener(InsertRow);

        advancescenes.CurrentBlock();
        BLOCKNAME = advancescenes.block;
        EVENTNAME = "Start New Block";
        dbconnect.Insert();
    }

    void Update()
    {
        ID = IdField.text;
        CONDITION = ConditionField.text;

        CombinedData.ID = ID;
        CombinedData.CONDITION = CONDITION;
        CombinedData.BLOCKNAME = BLOCKNAME;
        CombinedData.EVENTNAME = EVENTNAME;
    }

    public void InsertRow()
    {
        dbconnect.Insert();
    }
}
