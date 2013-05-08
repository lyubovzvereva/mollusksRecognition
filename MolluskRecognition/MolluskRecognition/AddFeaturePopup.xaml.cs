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

		private void AddSubFeature_Click(object sender, RoutedEventArgs e)
		{
			this.ElementsStackPanel.Children.Add(new TextBox());
		}
	}
}
