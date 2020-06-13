using System;
using System.Collections.Generic;
using System.IO;
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

namespace pwśg_wpf_lab2
{
    /// <summary>
    /// Interaction logic for UserControlStart.xaml
    /// </summary>
    public partial class UserControlStart : UserControl
    {
        MainWindow window;
        public UserControlStart(MainWindow mainWindow)
        {
            InitializeComponent();
            window = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataClass data;
            if(File.Exists("Passwords.bin"))
            {
                if( Serialization.Deserialize(passwordBox.Password,out object obj) )
                {
                    data = (DataClass)obj;

                    window.contentControl.Content = new UserControlMain(window,data, passwordBox.Password);
                }
                else
                {
                    MessageBox.Show("Password failed");
                }
            }
            else
            {
                window.contentControl.Content = new UserControlMain(window, new DataClass(), passwordBox.Password);
            }

            
        }
    }
}
