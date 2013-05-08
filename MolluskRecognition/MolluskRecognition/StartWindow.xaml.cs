using System.Windows;

namespace MolluskRecognition
{
	/// <summary>
	/// Interaction logic for StartWindow.xaml
	/// </summary>
	public partial class StartWindow : Window
	{
		public StartWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Событие клика на кнопку "Каталог НДМ"
		/// </summary>
		private void CatalogButton_Click(object sender, RoutedEventArgs e)
		{
			CatalogWindow catalog = new CatalogWindow();
			catalog.Owner = this;
			catalog.ShowDialog();
		}

		/// <summary>
		/// Событие клика на кнопку "Поиск"
		/// </summary>
		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			SearchWindow main = new SearchWindow();
			main.ShowDialog();
		}

		/// <summary>
		/// Событие клика на кнопку "Карты"
		/// </summary>
		private void MapButton_Click(object sender, RoutedEventArgs e)
		{
			MapsWindow maps = new MapsWindow();
			maps.Owner = this;
			maps.ShowDialog();
		}

	}
}
