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

namespace pwśg_wpf_lab2
{
    /// <summary>
    /// Interaction logic for SelectedDirectory.xaml
    /// </summary>
    public partial class SelectedDirectory : UserControl
    {
       
        public SelectedDirectory(Directory directory)
        {
            InitializeComponent();
            this.DataContext = directory;
        }
    }
}
