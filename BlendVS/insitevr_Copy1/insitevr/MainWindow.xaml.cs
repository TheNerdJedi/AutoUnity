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
using wyDay.Controls;

namespace insitevr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		AutomaticUpdaterBackend updater;
		
        public MainWindow()
        {
			System.Threading.Thread.Sleep(3000);
			
            InitializeComponent();
			
			updater = new AutomaticUpdaterBackend()
			{
			
			
			
			}
        }
    }
}
