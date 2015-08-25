// all the libraries that we are using 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

using wyDay.Controls; // adding wyBuild


namespace testWPF // we can change this to InsiteVR later  
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AutomaticUpdaterBackend updater; //this is what we are adding

        public MainWindow() // remember to change this to UpdateWindow if above changed
        {
            System.Threading.Thread.Sleep(3000); //We are telling program to pause here 

            InitializeComponent(); // now lets start

            updater = new AutomaticUpdaterBackend() // we still have to properly define AutoUpBackend
            {
                GUID = "06AF8C3C-BA10-4938-9D4C-5CAC43A006A7", // have to define this thing as well :/
                UpdateType = UpdateType.CheckAndDownload, // What type of update is you doing?
            }; // fucking semicolon

            // we define the updater things

            updater.CheckingFailed += Updater_CheckingFailed;
            
            updater.UpdateAvailable += OnUpdateAvailable;
            updater.DownloadingFailed += OnDownloadingFailed;
            updater.ExtractingFailed += OnExtractingFailed;
            //alot of things fail
            updater.ReadyToBeInstalled += OnReadyToBeInstalled;
            updater.UpdateSuccessful += OnUpdateSuccessful;
            updater.UpdateFailed += OnFailed;
            updater.UpToDate += OnUpToDate;

            // telling the updater we are good to go
            updater.Initialize();
            updater.AppLoaded();

            updater.ForceCheckForUpdate(); // the most controversial command

            // Console.WriteLine("legggo b0$$");

        }

        private void Updater_CheckingFailed(object sender, FailArgs e)
        {
            Console.WriteLine("*****I FAILED******* " + e.ErrorMessage); // since we kept on getting an error message we added e.ErrorMessage to find out why
            UpdateCheckErrorBox();
        }

      

        void OnUpdateAvailable(object sender, EventArgs e)
        {
            Console.WriteLine("update available");

        }

        void OnDownloadingFailed(object sender, FailArgs e)
        {
            Console.WriteLine("download fail");
            UpdateDownloadErrorBox();
        }

        void OnExtractingFailed(object sender, FailArgs e)
        {
            Console.WriteLine("extract fail");
            UpdateExtractErrorBox();
        }

        void OnReadyToBeInstalled(object sender, EventArgs e) // we are ready to install now what?
        {
            Console.WriteLine("ready install");
            try
            {
                string str = @"wyUpdate.exe";
                Process process = new Process();
                process.StartInfo.FileName = str;
                process.Start();
            }
            catch (Exception)
            {

                throw;
            }
        }


        void OnUpToDate(object sender, EventArgs e)
        {
            Console.WriteLine("update successful");
        }

        void OnUpdateSuccessful(object sender, SuccessArgs e)
        {
            Console.WriteLine("success");

        }

        void OnFailed(object sender, FailArgs e) //OnUpdateFailed
        {
            Console.WriteLine("fail");
            UpdateErrorBox();
        }

        void UpdateErrorBox()
        {
            MessageBoxResult result = MessageBox.Show("ERROR: Update failed.");
            Application.Current.Shutdown();
        }

        void UpdateCheckErrorBox()
        {
            MessageBoxResult result = MessageBox.Show("ERROR: Checking Failed");
            Application.Current.Shutdown();
        }

        void UpdateDownloadErrorBox()
        {
            MessageBoxResult result = MessageBox.Show("ERROR: Download Failed");
            Application.Current.Shutdown();
        }

        void UpdateExtractErrorBox()
        {
            MessageBoxResult result = MessageBox.Show("ERROR: Extract Failed");
            Application.Current.Shutdown();
        }

        void LoadMainWindow()
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                MainWindow main = new MainWindow();
                App.Current.MainWindow = main;
                main.Show();
                this.Close();
            });
            
        }

       
       

        private void button_1_Click(object sender, RoutedEventArgs e)
        {
            // string str = @"C:\windows\system32\notepad.exe";

            // string str  = @"C:\windows\system32\winamp.exe";

            // this opens a link System.Diagnostics.Process.Start("http://www.google.com");
            
            string str = @"location of .exe";
            Process process = new Process();
            process.StartInfo.FileName = str;
            process.Start();
            
        }

        // closing was aborted and then re-run this file     
    }
}


    
 

