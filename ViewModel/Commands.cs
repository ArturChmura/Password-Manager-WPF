using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace pwśg_wpf_lab2
{
    
    public class AddDirectoryCommand : ICommand
    {
        [field: NonSerialized()]
        public event EventHandler CanExecuteChanged;

       
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is ObservableCollection<ITreeElement> collection)
                collection.Add(new Directory() { Name = "New Directory", DeleteCommand = new DeleteCommand(collection) });
        }
    }
    public class AddImageCommand : ICommand
    {
        [field: NonSerialized()]
        public event EventHandler CanExecuteChanged;

     
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ObservableCollection<ITreeElement> collection)
            {
                MyImage image = new MyImage() { Name = "New Image", DeleteCommand = new DeleteCommand(collection) };

                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "pictures (*.jpg)|*.jpg|(*.png)|*.png",
                    RestoreDirectory = true
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    image.ImageSource = openFileDialog.FileName;
                    collection.Add(image);
                }
            }
          
        }
    }
    public class AddPasswordsCommand : ICommand
    {
        [field: NonSerialized()]
        public event EventHandler CanExecuteChanged;

    
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ObservableCollection<ITreeElement> collection)
            {
                collection.Add(new Passwords() { Name = "New Password", DeleteCommand = new DeleteCommand(collection) });
            }

        }
    }

    [Serializable]
    public class RenameCommand : ICommand
    {
        [field: NonSerialized()]
        public event EventHandler CanExecuteChanged;

        
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ((ITreeElement)parameter).IsBeeingRename = true;
        }
    }

    [Serializable]
    public class DeleteCommand : ICommand
    {
        [field: NonSerialized()]
        public event EventHandler CanExecuteChanged;

        readonly ObservableCollection<ITreeElement> treeElements;
        public DeleteCommand(ObservableCollection<ITreeElement> treeElements)
        {
            this.treeElements = treeElements;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            treeElements.Remove((ITreeElement)parameter);
        }
    }

    public class SwitchViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            (parameter as ITreeElement).IsBeeingRename = false;
        }
    }
}
