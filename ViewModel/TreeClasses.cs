using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace pwśg_wpf_lab2
{
    [Serializable]
    public class ObservableObject : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    public interface ITreeElement
    {
        string Name { get; set; }

        RenameCommand RenameCommand { get; set; }
        DeleteCommand DeleteCommand { get; set; }

        ICommand SwitchView { get; set; }

        bool IsBeeingRename { get; set; }


    }
    [Serializable]
    public class Directory :ObservableObject, ITreeElement
    {
        public string Name { get; set; }

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
            get { return  _addPasswordsCommand; }
            set {  _addPasswordsCommand = value; }
        }

       
        private ObservableCollection<ITreeElement> _items;
        public ObservableCollection<ITreeElement> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        [NonSerialized]
        private RenameCommand _renameCommand;
        public RenameCommand RenameCommand
        {
            get { return _renameCommand; }
            set { _renameCommand = value; }
        }

      
        private DeleteCommand _deleteCommand;
        public DeleteCommand DeleteCommand
        {
            get { return _deleteCommand; }
            set { _deleteCommand = value; }
        }

        [NonSerialized]
        private ICommand _deleteChildCommand;
        public ICommand DeleteChildCommand
        {
            get { return _deleteChildCommand; }
            set { _deleteChildCommand = value; }
        }

      

        [NonSerialized]
        private ICommand _switchView;
        public ICommand SwitchView
        {
            get { return _switchView; }
            set { _switchView = value; }
        }

        private bool _isBeeingRename = false;
        public bool IsBeeingRename
        {
            get { return _isBeeingRename; }
            set
            {
                _isBeeingRename = value;
                OnPropertyChanged("IsBeeingRename");
            }
        }

        public Directory()
        {
            Items = new ObservableCollection<ITreeElement>();

            AddDirectoryCommand = new AddDirectoryCommand();
            AddImageCommand = new AddImageCommand();
            AddPasswordsCommand = new AddPasswordsCommand();
            RenameCommand = new RenameCommand();
            DeleteChildCommand = new DeleteCommand(this.Items);
            SwitchView = new SwitchViewCommand();
        }
        [OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            AddDirectoryCommand = new AddDirectoryCommand();
            AddImageCommand = new AddImageCommand();
            AddPasswordsCommand = new AddPasswordsCommand();
            RenameCommand = new RenameCommand();
            DeleteChildCommand = new DeleteCommand(this.Items);
            SwitchView = new SwitchViewCommand();
        }
    }

    [Serializable]
    public class MyImage : ObservableObject, ITreeElement
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }

        [NonSerialized]
        private RenameCommand _renameCommand;
        public RenameCommand RenameCommand
        {
            get { return _renameCommand; }
            set { _renameCommand = value; }
        }

    
        private DeleteCommand _deleteCommand;
        public DeleteCommand DeleteCommand
        {
            get { return _deleteCommand; }
            set { _deleteCommand = value; }
        }

        private bool _isBeeingRename = false;
        public bool IsBeeingRename
        {
            get { return _isBeeingRename; }
            set
            {
                _isBeeingRename = value;
                OnPropertyChanged("IsBeeingRename");
            }
        }
        [NonSerialized]
        private ICommand _switchView;
        public ICommand SwitchView
        {
            get { return _switchView; }
            set { _switchView = value; }
        }
        public MyImage()
        {
            this.RenameCommand = new RenameCommand();
            this.SwitchView = new SwitchViewCommand();
        }
        [OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            this.RenameCommand = new RenameCommand();
            this.SwitchView = new SwitchViewCommand();
        }

    }

    [Serializable]
    public class Passwords : ObservableObject, ITreeElement
    {

        private bool _isSelected = false;

        public bool IsBeeingRename
        {
            get { return _isSelected; }
            set { 
                _isSelected = value;
                OnPropertyChanged("IsBeeingRename");
            }
        }

        public string Name { get; set; }
        private AccountInfo _selectedAccount;
        public AccountInfo SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
            }
        }
        public ObservableCollection<AccountInfo> Accounts { get; set; }


        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                AccountCollection.Refresh();
            }
        }

        private bool FilterFunc(object obj)
        {
            if (string.IsNullOrEmpty(Filter))
            {
                return true;
            }
            return (obj as AccountInfo).Name.ToUpper().Contains(Filter.ToUpper());


        }

        [NonSerialized]
        private ICollectionView _accountCollection;
        public ICollectionView AccountCollection
        {
            get { return _accountCollection; }
            set
            {
                _accountCollection = value;
            }
        }


       [NonSerialized]
        private RenameCommand _renameCommand;
        public RenameCommand RenameCommand
        {
            get { return _renameCommand; }
            set { _renameCommand = value; }
        }


        private DeleteCommand _deleteCommand;
        public DeleteCommand DeleteCommand
        {
            get { return _deleteCommand; }
            set { _deleteCommand = value; }
        }

        [NonSerialized]
        private ICommand _switchView;

        public ICommand SwitchView
        {
            get { return _switchView; }
            set { _switchView = value; }
        }
        public Passwords()
        {
            this.Accounts =   new ObservableCollection<AccountInfo>(); 

            this.AccountCollection = CollectionViewSource.GetDefaultView(Accounts);
            this.AccountCollection.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            this.AccountCollection.GroupDescriptions.Add(new PropertyGroupDescription("Name", new FirstLetterConverter()));
            this.AccountCollection.Filter += FilterFunc;
            this.SwitchView = new SwitchViewCommand();
            this.RenameCommand = new RenameCommand();
           
        }
        [OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            this.AccountCollection = CollectionViewSource.GetDefaultView(Accounts);
            this.AccountCollection.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            this.AccountCollection.GroupDescriptions.Add(new PropertyGroupDescription("Name", new FirstLetterConverter()));
            this.AccountCollection.Filter += FilterFunc;
            this.SwitchView = new SwitchViewCommand();
            this.RenameCommand = new RenameCommand();
        }
    }
    [Serializable]
    public class AccountInfo : ObservableObject, ICloneable, IEquatable<AccountInfo>
    {
        public AccountInfo()
        {
            this._creationTime = DateTime.Now;
            this.LastEditTime = this._creationTime;
        }
        private DateTime _creationTime;

        public DateTime CreationTime
        {
            get { return _creationTime; }
        }


        public DateTime LastEditTime { get; set; }

        private string _iconSource;
        public string IconSource
        {
            get { return _iconSource; }
            set
            {
                _iconSource = value;
                OnPropertyChanged("IconSource");
            }
        }


        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }

            }
        }

        private string _emial;
        public string Email
        {
            get { return _emial; }
            set
            {
                if (_emial != value)
                {
                    _emial = value;
                    OnPropertyChanged("Email");
                }

            }
        }
        private PasswordStrength _passwordStrength;
        public PasswordStrength PasswordStrength
        {
            get { return _passwordStrength; }
            set
            {
                if (_passwordStrength != value)
                {
                    _passwordStrength = value;
                    OnPropertyChanged("PasswordStrength");
                }
            }
        }


        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged("Login");
                }

            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PasswordStrength = PasswordStrengthUtils.CalculatePasswordStrength(value);
                    OnPropertyChanged("Password");
                }

            }
        }

        private string _website;
        public string Website
        {
            get { return _website; }
            set
            {
                if (_website != value)
                {
                    _website = value;
                    OnPropertyChanged("Website");
                }

            }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged("Notes");
                }

            }
        }


        public object Clone()
        {
            return new AccountInfo() { Email = Email, IconSource = IconSource, Login = Login, Name = Name, Notes = Notes, Password = Password, Website = Website };
        }
        public bool Equals(AccountInfo other)
        {
            if (other.Email != this.Email ||
                other.Password != this.Password ||
                other.IconSource != this.IconSource ||
                other.Name != this.Name ||
                other.Login != this.Login ||
                other.Website != this.Website ||
                other.Notes != this.Notes)
            {
                return false;
            }
            return true;
        }

        public void AssignNewValues(AccountInfo other)
        {
            this.Email = other.Email;
            this.Password = other.Password;
            this.IconSource = other.IconSource;
            this.Name = other.Name;
            this.Login = other.Login;
            this.Website = other.Website;
            this.Notes = other.Notes;
            LastEditTime = DateTime.Now;
        }
    }

}
