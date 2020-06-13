using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for PasswordEditor.xaml
    /// </summary>
    public partial class PasswordEditor : UserControl
    {
        Passwords passwords;
       
       
        public PasswordEditor(Passwords passwords)
        {
            InitializeComponent();
            this.passwords = passwords;
            this.DataContext = this.passwords;
          
        }

        private void AddNewAccountButton_Click(object sender, RoutedEventArgs e)
        {
            AccountInfo accountInfo =  new AccountInfo() { Name = "New Account" };
            passwords.Accounts.Add(accountInfo);
            rightControl.Content = new EditAccount(accountInfo, rightControl,passwords.AccountCollection);

        }

      

        private void passwordsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                rightControl.Content = new ViewAccount((AccountInfo)e.AddedItems[0], rightControl,passwords.AccountCollection);
            }
            else
            {
                rightControl.Content = null;
            }
        }
        
     
       
       
    }
}
