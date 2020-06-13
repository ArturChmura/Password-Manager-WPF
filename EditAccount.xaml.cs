using Microsoft.Win32;
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
    /// Interaction logic for EditAccount.xaml
    /// </summary>
    public partial class EditAccount : UserControl
    {
        AccountInfo originalAccountInfo;
        AccountInfo editableAccountInfo;
        ContentControl rightControl;
        ICollectionView collectionView;

        public EditAccount(AccountInfo accountInfo, ContentControl rightControl, ICollectionView collectionView)
        {
            InitializeComponent();
            this.originalAccountInfo = accountInfo;
            this.editableAccountInfo = (AccountInfo)accountInfo.Clone();
            this.rightControl = rightControl;
            this.collectionView = collectionView;
            DataContext = this.editableAccountInfo;
        }
        private void AddIconButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "pictures (*.jpg)|*.jpg",
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                editableAccountInfo.IconSource = openFileDialog.FileName;
            }
        }

        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!editableAccountInfo.Equals(originalAccountInfo))
            {
                originalAccountInfo.AssignNewValues(editableAccountInfo);
            }
            collectionView.Refresh();
            ChangeToView();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeToView();
        }

        private void ChangeToView()
        {
            this.rightControl.Content = new ViewAccount(originalAccountInfo, rightControl, collectionView);
        }
    }
}