using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace pwśg_wpf_lab2
{
    //
    class DirectoryToString : IMultiValueConverter
    {
  

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values.Length == 2 && values[0] is string name &&  values[1] is int count)
            {
                return name + "(" + count + ")";
            }
            return null;
        }

    

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //Wyznacza czy wyświetlić pole po prawej w zależności od zaznaczonego elementu
    class SelectedToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //Jeśli string nie jest pusty to Collapsed
    class StringToCollapsed : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //Jeśli string nie jest pusty to Visible, inaczej Collapsed 
    class StringToVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //Zwracamy string z danymi na podstawie imageSource
    class ImageToResoultion : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BitmapSource imageSource)
            {
                return "Resolution: " + imageSource.Width.ToString() + "x" + imageSource.Height.ToString() + "\n"
              + "DPI: " + imageSource.DpiX.ToString() + "x" + imageSource.DpiY.ToString() + "\n" +
              "Format: " + imageSource.Format.ToString();
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //ukrywanie znaków stringa
    class StringToPassword : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                return new string('*', s.Length);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //Ustawia enable na false gdy edytujemy konto
    class ContentToEnable : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EditAccount)
            {
                return false;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //Kolor na podstawie siły hasła
    class PassStrengthToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            PasswordStrength passwordStrength = (PasswordStrength)value;
            switch (passwordStrength)
            {
                case PasswordStrength.Invalid:
                    return new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

                case PasswordStrength.VeryWeak:

                    return new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));

                case PasswordStrength.Weak:

                    return new SolidColorBrush(Color.FromArgb(255, 245, 111, 66));

                case PasswordStrength.Average:

                    return new SolidColorBrush(Color.FromArgb(255, 245, 200, 66));

                case PasswordStrength.Strong:

                    return new SolidColorBrush(Color.FromArgb(255, 170, 245, 66));

                case PasswordStrength.VeryStrong:

                    return new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));

                default:
                    return new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //Zwraca inta potrzebnego do statusBar przy haśle 
    class PassStrengthToInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0.0;
            PasswordStrength passwordStrength = (PasswordStrength)value;
            switch (passwordStrength)
            {
                case PasswordStrength.Invalid:
                    return 0.0;

                case PasswordStrength.VeryWeak:

                    return 20.0;

                case PasswordStrength.Weak:

                    return 40.0;

                case PasswordStrength.Average:

                    return 60.0;

                case PasswordStrength.Strong:

                    return 80.0;

                case PasswordStrength.VeryStrong:

                    return 100.0;

                default:
                    return 0.0;

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //Zwraca pierwszą literę słowa
    public class FirstLetterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value is string s)
            {
                if ( s.Length > 0)
                    return new string(char.ToUpper(s[0]), 1);
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
