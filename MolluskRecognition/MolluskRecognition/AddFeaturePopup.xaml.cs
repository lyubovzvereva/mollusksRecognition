using System.Windows;
using System.Windows.Controls;

namespace MolluskRecognition
{
	/// <summary>
	/// Interaction logic for AddFeaturePopup.xaml
	/// </summary>
	public partial class AddFeaturePopup : Window
	{
		public AddFeaturePopup()
		{
			InitializeComponent();
		}

        /// <summary>
        /// Нажатие на кнопку добавить подпризнак
        /// </summary>
		private void AddSubFeature_Click(object sender, RoutedEventArgs e)
		{
            this.ElementsStackPanel.Children.Add(new TextBox() { Margin = new Thickness(10) });
		}

        /// <summary>
        /// Нажатие на кнопку добавить признак
        /// </summary>
        private void AddFeatureButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
	}
}
