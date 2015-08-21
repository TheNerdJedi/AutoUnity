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


    IEnumerator WaitForRequest(WWW www)
    {
        //It only goes beyond this when there is an error connecting or the script has reseved data from the online script successfully
        yield return www;

        // check for errors
        if (www.error == null)
        {
            //if there are no errors then pring WWW Ok!
            print("WWW Ok!: " + www.text);

            //check what the online script gives back
            if (www.text == "up-to-date")
            {
                //if it says up to date then print up to date
                print("Game Version is up to date");
                
                //if the game is uptodate and there is something writen in UPTODATESCENE then switch to the 'UPTODATESCENE' scene
                if (UPTODATESCENE != null && UPTODATESCENE != "")
                    Application.LoadLevel(UPTODATESCENE);
            }
            else if (www.text != "")
            {
                //Only if the online script says updates required start updating
                StartUpdate();
            }
        } 
        else
        {
            //if there is a error print it
            errorMessage = "WWW Error: " + www.error;
        }    
    }

    private void StartUpdate()
    {
        //This setts the update to true
        update = true;

        //this starts the download of the file
        wb.DownloadFileAsync(new Uri(urlUpdate), tempDirectory + "updates" + fileFormat);

        //this checks the changes in the download and sends it to the function wb_DownloadProgressChanged
        wb.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wb_DownloadProgressChanged);

        //This is only run when the downloads are completed
        wb.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(wb_DownloadFileCompleted);
    }

    private void wb_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        //when the downloads are completed clean the webclinet called wb
        wb.Dispose();
        //run the function launchAndQuit()
        launchAndQuit();
    }

    private void launchAndQuit()
    {
        //Start the update/downloaded file
        System.Diagnostics.Process.Start(tempDirectory + "updates" + fileFormat);

        //Quit the game
        Application.Quit();
    }

    private void wb_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        //when ever the progress is changed then 
        downloadedPercent = e.ProgressPercentage;
    }

    private void OnGUI()//this repeates 2 times every frame 
    {
        //set the skin to my skin
        GUI.skin = myskin;
        
        //show any error messages
        GUILayout.Label(errorMessage);

        //if autocorrect is enabled then set the varible DownloadBarPos to the center
        if (autoCenterDownloadBar)
            DownloadBarPos = new Rect(Screen.width / 2 - 200, Screen.height / 2 - 25, 400, 50);

        //show the download bar
        GUI.Box(new Rect(DownloadBarPos.x, DownloadBarPos.y, downloadedPercent * 4, DownloadBarPos.height), downloadedPercent.ToString() + "%");

        //if the cancel button is enabled then show the cancel button
        if (showCancelButton)
            if (GUI.Button(CancelButton, "Cancel Update"))
            {
                //if cancel button is clicked then
                //stop download and clean the wb
                wb.CancelAsync();
                wb.Dispose();
                //print canceled button is pressed
                print("download Canceled");

                //switch to the cancel button pressed scene
                if (CencelScene != null && CencelScene != "")
                    Application.LoadLevel(CencelScene);
            }
    }
}
