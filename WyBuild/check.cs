using UnityEngine;
using System.Collections;

using System;
using System.Diagnostics;

public class check : MonoBehaviour {

    // Variables

    public bool showUpdateButton = true;
    public Rect UpdateButton = new Rect(0, 0, 0, 0);

    // the scene that it is redirected to
    public string UpdateScene = "";


    // Other stuff checked in other stuff

    public static bool update = false;
    public static string errorMessage = "You done goofed";




    private void OnGUI() 
    {
    if (showUpdateButton)
        if (GUI.Button(UpdateButton, "Check For Updates"))
        {
            // What happens if Check for Updates is clicked
            
            try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";
                string path = "C:\\Users\\Russell\\Desktop\\testFile.bat";
                myProcess.StartInfo.Arguments = "/c" + path;
                myProcess.EnableRaisingEvents = true;
                myProcess.Start();
                myProcess.WaitForExit();
                int ExitCode = myProcess.ExitCode;
                //print(ExitCode);
            }
            catch (Exception e)
            {
                print(e);
            }



            // Print "Checked for Update"
            print("Checked for Update");
        }   
    }
}

