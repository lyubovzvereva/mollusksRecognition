using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MolluskRecognition.DataModels;

namespace MolluskRecognition
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
		/// <summary>
		/// Список признаков
		/// </summary>
    	private List<Feature> features;

		/// <summary>
		/// Конструктор
		/// </summary>
    	public SearchWindow(List<Feature> features)
		{
			this.features = features;
            InitializeComponent();
			FillFeaturesList(features, 0);
		}

    	/// <summary>
    	/// Отобразить список свойст
    	/// </summary>
    	private void FillFeaturesList(IEnumerable<Feature> list, int marginLeft)
    	{
			Thickness mar = new Thickness(marginLeft, 0, 0, 0);
    		foreach(Feature feature in list)
    		{
    			Label label = new Label {Content = feature.Name, Margin = mar};
    			this.FeaturesStackPanel.Children.Add(label);

				if (feature.SubFeatures != null && feature.SubFeatures.Count > 0)
				{
					FillFeaturesList(feature.SubFeatures, marginLeft+10);
				}
				else
				{
					TextBox textBox = new TextBox {Text = feature.Value, Margin = mar};
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
    }
}
