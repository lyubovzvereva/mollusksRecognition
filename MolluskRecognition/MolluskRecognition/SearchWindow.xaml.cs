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
			FillFeaturesList(features);
		}

		/// <summary>
		/// Отобразить список свойст
		/// </summary>
		/// <param name="list"></param>
    	private void FillFeaturesList(List<Feature> list)
    	{
    		foreach(Feature feature in list)
    		{
    			Label label = new Label {Content = feature.Name};
    			this.FeaturesStackPanel.Children.Add(label);

				if (feature.SubFeatures != null && feature.SubFeatures.Count > 0)
				{
					FillFeaturesList(feature.SubFeatures);
				}
				else
				{
					TextBox textBox = new TextBox {Text = feature.Value};
					this.FeaturesStackPanel.Children.Add(textBox);
				}
    		}
    	}
    }
}
