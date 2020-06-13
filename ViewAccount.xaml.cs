using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PasswordViewer.xaml
    /// </summary>
    public partial class ViewAccount : UserControl
    {
        AccountInfo accountInfo;
        ContentControl rightControl;
        ICollectionView collectionView;
        public ViewAccount(AccountInfo accountInfo, ContentControl rightControl, ICollectionView collectionView)
        {
            InitializeComponent();
            this.accountInfo = accountInfo;
            this.rightControl = rightControl;
            this.collectionView = collectionView;
            this.DataContext = accountInfo;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            this.rightControl.Content = new EditAccount(accountInfo, rightControl,collectionView);
            collectionView.Refresh();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            (collectionView.SourceCollection as ObservableCollection<AccountInfo>).Remove(accountInfo);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            e.Handled = true;
        }

        private void Hyperlink_RequestNavigate_1(object sender, RequestNavigateEventArgs e)
        {
            string sendto = "mailto:" + e.Uri.OriginalString + "?subject=SubjectExample&amp;body=BodyExample";
            try
            {
                System.Diagnostics.Process.Start(sendto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            e.Handled = true;
        }

        private void copyEmailButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(accountInfo.Email);
        }

        private void copyLoginButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(accountInfo.Login);
        }

        private void copyPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(accountInfo.Password);
        }
    }
}
