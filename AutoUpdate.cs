using UnityEngine;
using System.Collections;

using System.Net;//import all the net (download / upload) function
using System;//Import all the functions like Exception and getting the appdata directory of the user


public class AutoUpdator : MonoBehaviour {


    //Declaring Varibles 
    //public varibles that can be edited in unity 3d
    public GUISkin myskin;
    public string GameVersion = "none", fileFormat = ".exe", securityCode = "";
    public string urlVersion = "" , urlUpdate = "";
    public bool TempDirectoryInsideAppdata = true, updateOnStart = true;
    public string tempDirectory = "";
    public bool autoCenterDownloadBar = true;
    public Rect DownloadBarPos = new Rect(0,0,0,0);
    public bool showCancelButton = true;
    public Rect CancelButton = new Rect(0, 0, 0, 0);


    public string UPTODATESCENE = "" , CencelScene = "";

    //the following varibles can be checked in other scripts!
    public static bool update = false;
    public static string errorMessage = "";

    //some private varibles
    private WebClient wb = new WebClient();//This is used to download and upload files
    private int downloadedPercent = 0;

	void Start ()// This is run at the start
    {
        //if the updateOnStart is true then run CheckUpdates()
        if(updateOnStart)
            CheckUpdates();
    }

    public void CheckUpdates()
    {
        //this checks if the folder is needed to be created in the appdata then it creates the folder where it is required
        if (TempDirectoryInsideAppdata)
            System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" +tempDirectory);
        else
            System.IO.Directory.CreateDirectory(tempDirectory);

        //This is a try incase something fails
        try
        {
            //This gets the varibles ready to be sent to the script stored online
            WWWForm form = new WWWForm();
            form.AddField("version", GameVersion);
            form.AddField("hash", securityCode);
            WWW www = new WWW(urlVersion, form);

            //this tells it to start the process by executing the function [WaitForRequest]
            StartCoroutine(WaitForRequest(www));
        }
        catch (Exception)
        {
            //if there is a error change message to the following text
            errorMessage = "Could not connect to the server!";
        }
    }