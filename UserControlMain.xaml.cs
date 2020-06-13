using Microsoft.Win32;
using pwśg_wpf_lab2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
using WPF_Project;

namespace pwśg_wpf_lab2
{
    /// <summary>
    /// Interaction logic for UserControlMain.xaml
    /// </summary>
   
    
   
    public partial class UserControlMain : UserControl
    {
        readonly MainWindow main;
        readonly DataClass data;
        string password;
       
        public UserControlMain(MainWindow main,DataClass data,string password)
        {
            InitializeComponent();
            this.main = main;
            this.data = data;
            this.password = password;
            this.DataContext = data;
        }


      
        private void Menu_Logout_Click(object sender, RoutedEventArgs e)
        {
            main.contentControl.Content = new UserControlStart(main);
        }

      
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(e.NewValue is MyImage img)
            {
                rightContentControl.Content = new SelectedImage(img);
            }
            else if(e.NewValue is Directory dir)
            {
                rightContentControl.Content = new SelectedDirectory(dir);
            }
            else if (e.NewValue is Passwords pass)
            {
                rightContentControl.Content = new PasswordEditor(pass);
            }
        }

        private void Menu_Save_Click(object sender, RoutedEventArgs e)
        {
            Serialization.Serialize(data, password);
        }

       
    }
}
