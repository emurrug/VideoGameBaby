//FILE README (Emma's notes)

//05.14.2020 this file is divided into 2 parts: 
//1. connection to the MySQL database and 
//2. instances (listed as variables) that will trigger data collection in the script

//this file contains code for interfacing directly with the web-hosted server database
//in short, I have included the essentials: opening, accessing, inserting into, and closing the database
//I decided to omit other basic code like updating and deleting rows within the database --
//This was because I figured it would be best to download the data in longform (to obtain multiple timestamps)
// and not overwrite entries based on a single variable.

//the server is currently being hosted (for free) on AWS. Below are all the credentials needed to access 
//the MySQL database from a product like 'MySQL Workbench'. This is where I am currently working to 
//select, modify, and delete data if I need to (such as for test runs). 
//It is also essential to have 'MySQL Connector' in order to link the C# script in Unity to a SQL database. 
//I did it this way so each game instance connects directly with the dedicated server (instead of having a localhost server).

//05.15.2020 the only way to run this is by adding the .DLL reference "MySql.Data"
//directly to the 'Assets' library. This can be downloaded online, but it MUST be vers. 6.9.8.0 
//to be compatible with the .NET connector

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System.Dynamic;
using System;

public class DBConnect : MonoBehaviour
{
    //public DataLogging datalogging;


    private MySqlConnection connection;
    private string server;
    private string database;
    private string uid;
    private string password;

    public string timenow; //time in ms since beginning of game
    public string id;//subject ID
    public string subcondition; //subject condition (currently no script exists for alternate conditions)
    public string blockname; //references AdvanceScenes script to get block info 
    public string trial; // the number of car loops (including "hits" and "misses")
    public string carspeed; //the current speed of the car
    public string babylocation; //whether the baby is on blanket, grass, or road
    public string responsename; //references player actions
    public string eventname; //references game actions

    //initialize values
    public DBConnect()
    {
        server = "virtualbaby-simulator.cpoxqggcx2nm.us-east-2.rds.amazonaws.com";
        database = "unityaccess";
        uid = "admin";
        password = "virtualbaby";

        string connectionString;
        connectionString = "SERVER=" + server + ";" + "DATABASE=" +
        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        connection = new MySqlConnection(connectionString);
    }

    //open connection to database
    private bool OpenConnection()
    {
        try
        {
            connection.Open();
            return true;
        }
        catch (MySqlException ex)
        {
            //When handling errors, you can your application's response based on the error no. 
            //These are just the most common 2.

            switch (ex.Number)
            {
                case 0:
                    Debug.Log("Cannot connect to server.  Contact administrator");
                    break;

                case 1045:
                    Debug.Log("Invalid username/password, please try again");
                    break;
            }
            return false;
        }
    }

    //Close connection
    private bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            Debug.Log("Whoops! Something went wrong. Can't close the connection.");
            return false;
        }
    }


    
    //Makes sure the relevant variables are always updated
    void Update()
    {
        timenow = CombinedData.TIMENOW;
        id = CombinedData.ID;
        subcondition = CombinedData.CONDITION;
        blockname = CombinedData.BLOCKNAME;
        trial = CombinedData.TRIAL;
        carspeed = CombinedData.CARSPEED;
        babylocation = CombinedData.BABYLOCATION;
        eventname = CombinedData.EVENTNAME;
        responsename = CombinedData.RESPONSENAME;
    }

    //Insert statement
    public void Insert()
    {
        string query = "INSERT INTO subjects (timenow, id, subcondition, block, trial, carspeed, babylocation, givenevent, response) VALUES ('" +
            timenow + "', '" +
            id + "', '" + 
            subcondition + "', '" + 
            blockname + "', '" + 
            trial + "', '" + 
            carspeed + "', '" +
            babylocation + "', '" +
            eventname + "', '" + 
            responsename + "')";

        if (this.OpenConnection() == true)
        {
            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Execute command then close connection
            cmd.ExecuteNonQuery();
            this.CloseConnection();
        }
    }


    //Select statement (for referencing stable variables)
    public List<string>[] Select()
    {
        string query = "SELECT * FROM unityaccess";

        //Create a list to store the result
        List<string>[] list = new List<string>[3];
        list[0] = new List<string>();
        list[1] = new List<string>();
        list[2] = new List<string>();
        list[3] = new List<string>();
        list[4] = new List<string>();
        list[5] = new List<string>();


        //  Open connection
        if (this.OpenConnection() == true)
        {
            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                list[0].Add(dataReader["rowid"] + "");
                list[1].Add(dataReader["id"] + "");
                list[2].Add(dataReader["subcondition"] + "");
                list[3].Add(dataReader["block"] + "");
                list[4].Add(dataReader["givenevent"] + "");
                list[5].Add(dataReader["response"] + "");
            }

            //close Data Reader
            dataReader.Close();

            //close Connection
            this.CloseConnection();

            //return list to be displayed
            return list;
        }
        else
        {
            return list;
        }
    }
}

