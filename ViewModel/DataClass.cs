using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pwśg_wpf_lab2
{
    [Serializable]
    public class DataClass
    {
        [NonSerialized]
        private ICommand _addDirectoryCommand;
        public ICommand AddDirectoryCommand
        {
            get { return _addDirectoryCommand; }
            set { _addDirectoryCommand = value; }
        }

        [NonSerialized]
        private ICommand _addImageCommand;
        public ICommand AddImageCommand
        {
            get { return _addImageCommand; }
            set { _addImageCommand = value; }
        }

        [NonSerialized]
        private ICommand _addPasswordsCommand;
        public ICommand AddPasswordsCommand
        {
            get { return _addPasswordsCommand; }
            set { _addPasswordsCommand = value; }
        }
        public ObservableCollection<ITreeElement> Tree { get; set; }

        [NonSerialized]
        private ITreeElement _selectedElement;
        public ITreeElement SelectedElement
        {
            get { return _selectedElement; }
            set { _selectedElement = value; }
        }

        public DataClass()
        {
            Tree = new ObservableCollection<ITreeElement>();
            this.AddDirectoryCommand = new AddDirectoryCommand();
            this.AddImageCommand = new AddImageCommand();
            this.AddPasswordsCommand = new AddPasswordsCommand();
        }

        [OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            this.AddDirectoryCommand = new AddDirectoryCommand();
            this.AddImageCommand = new AddImageCommand();
            this.AddPasswordsCommand = new AddPasswordsCommand();
        }

    }
}
