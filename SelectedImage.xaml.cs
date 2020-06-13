using Microsoft.Win32;
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
    /// Interaction logic for SelectedImage.xaml
    /// </summary>
    public partial class SelectedImage : UserControl
    {

        public SelectedImage( MyImage myImage )
        {
            InitializeComponent();

            DataContext = myImage;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "pictures (*.png)|*.png",
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var stream = File.Create(saveFileDialog.FileName))
                {
                    SelectedImage.SaveAsPng(SelectedImage.GetImage(image), stream);
                }
                
            }
        }
        public static void SaveAsPng(RenderTargetBitmap src, Stream outputStream)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(src));
            encoder.Save(outputStream);
        }
        public static RenderTargetBitmap GetImage(Image view)
        {
            Size size = new Size(view.ActualWidth, view.ActualHeight);
            if (size.IsEmpty)
                return null;

            RenderTargetBitmap result = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual drawingvisual = new DrawingVisual();
            using (DrawingContext context = drawingvisual.RenderOpen())
            {
                context.DrawRectangle(new VisualBrush(view), null, new Rect(new Point(), size));
                context.Close();
            }

            result.Render(drawingvisual);
            return result;
        }
    }
}
