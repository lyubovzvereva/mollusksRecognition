using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MolluskRecognition.DAL.DataModels;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Media;
using MolluskRecognition.Views;
using MolluskRecognition.Presenters;

namespace MolluskRecognition
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window, ISearchView
    {
        /// <summary>
        /// Список признаков
        /// </summary>
        private List<Feature> _features;

        /// <summary>
        /// Конструктор
        /// </summary>
        public SearchWindow(List<Feature> features)
        {
            this._features = features;
            InitializeComponent();
            FillFeaturesList(features, 0);
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Отобразить список свойст
        /// </summary>
        private void FillFeaturesList(IEnumerable<Feature> list, int marginLeft)
        {
            Thickness mar = new Thickness(marginLeft, 0, 0, 0);
            foreach (Feature feature in list)
            {
                Label label = new Label { Content = feature.Name, Margin = mar };
                this.FeaturesStackPanel.Children.Add(label);

                if (feature.SubFeatures != null && feature.SubFeatures.Count > 0)
                {
                    FillFeaturesList(feature.SubFeatures, marginLeft + 10);
                }
                else
                {
                    TextBox textBox = new TextBox { Text = feature.Value, Margin = mar };
                    this.FeaturesStackPanel.Children.Add(textBox);
                }
            }
        }

        /// <summary>
        /// Событие нажатия на кнопку "Добавить признак"
        /// </summary>
        private void AddFeature_Click(object sender, RoutedEventArgs e)
        {
            AddFeaturePopup addFeature = new AddFeaturePopup();
            addFeature.ShowDialog();
        }

        /// <summary>
        /// Нажатие на кнопку "Загрузить изображение"
        /// todo: сделать это нормально
        /// </summary>
        private void loadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            bool? result = openDialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    BitmapImage myBitmapImage = new BitmapImage(new Uri(openDialog.FileName, UriKind.Absolute));

                    //convert to grayscale

                    FormatConvertedBitmap newFormatedBitmapSource = new FormatConvertedBitmap();
                    newFormatedBitmapSource.BeginInit();
                    newFormatedBitmapSource.Source = myBitmapImage;
                    newFormatedBitmapSource.DestinationFormat = PixelFormats.Gray8;
                    newFormatedBitmapSource.EndInit();


                    double dpi = 96;
                    int width = newFormatedBitmapSource.PixelWidth;
                    int height = newFormatedBitmapSource.PixelHeight;

                    int stride = width; // 4 bytes per pixel
                    byte[] pixelData = new byte[stride * height];
                    newFormatedBitmapSource.CopyPixels(pixelData, stride, 0);
                    BitmapSource bmpSource = BitmapSource.Create(width, height, dpi, dpi, PixelFormats.Gray8, null, pixelData, stride);

                    molluskImage.Source = bmpSource;


                    //molluskImage.RenderTransform = trGrp;


                }
                catch
                {

                    MessageBox.Show("произошла ошибка! Это не изображение! Попробуйте снова.");
                }
            }
        }

        /// <summary>
        /// Activate vie
        /// </summary>
        public void Activate(Window owner)
        {
            this.Owner = owner;
            this.ShowDialog();
        }

        /// <summary>
        /// Deactivate view
        /// </summary>
        public void Deactivate()
        {
            this.Close();
        }

        /// <summary>
        /// Set data context for view
        /// </summary>
        public void SetDataContext(IPresenterBase presenter)
        {
            this.DataContext = presenter;
        }
    }
}
