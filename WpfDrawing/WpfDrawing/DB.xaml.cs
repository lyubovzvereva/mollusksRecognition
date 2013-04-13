using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.IO;

namespace WpfDrawing
{
    /// <summary>
    /// Interaction logic for DB.xaml
    /// </summary>
    public partial class DB : Window
    {
        public DB()
        {
            InitializeComponent();
            UIManager.man = new UIManager(dataGrid);
           
          //  win.h_l.Text = "12";
        }
         protected override void OnClosed(EventArgs e)
        {
            UIManager.man.Close();
            base.OnClosed(e);
        }

        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            
        }

        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //if (e.Column.Header.ToString() == "bitmap")
            //{
            //    DataRowView drv = e.Row.Item as DataRowView;
            //    long id = (int)drv[0];
            //    Stream stream = UIManager.man.Provider.ReadData(id, "fossils", "bitmap");
            //    BitmapImage image = new BitmapImage();
            //    image.BeginInit();
            //    image.CacheOption = BitmapCacheOption.OnLoad;
            //    image.StreamSource = stream;
            //    image.EndInit();
            //    previewimage.Source = image;
            //    stream.Close();
            //    popup.IsOpen = true;
            //    popup.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
            //    popup.StaysOpen = false;
            //}
        }
    }
    class BitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                DataRowView drv = value as DataRowView;
                if (drv == null)
                    return null;

                long id = (int)drv[0];
                BitmapImage image = null;
                try
                {//!!!!
                    Stream stream = UIManager.man.Provider.ReadData(id, "Photo", "bitmap");
                    if (stream.Length == 0)
                        return null;
                    image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                    stream.Close();
                }
                catch { }

                return image;
            }
            catch
            {
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    

}
