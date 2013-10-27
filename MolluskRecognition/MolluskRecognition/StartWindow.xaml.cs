using System.Collections.Generic;
using System.Windows;
using MolluskRecognition.DataModels;
using MolluskRecognition.Views;

namespace MolluskRecognition
{
	/// <summary>
	/// Interaction logic for StartWindow.xaml
	/// </summary>
    public partial class StartWindow : Window, IStartView
	{
        /// <summary>
        /// Constuctor
        /// </summary>
		public StartWindow()
		{
			InitializeComponent();
		}

        /// <summary>
        /// Set data context for view
        /// </summary>
        public void SetDataContext(Presenters.MainPresenter presenter)
        {
            this.DataContext = presenter;
        }

        /// <summary>
        /// Activate view
        /// </summary>
        public new void Activate()
        {
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
        /// Get handler of main window
        /// </summary>
        public Window GetWindowHandler()
        {
            return Window.GetWindow(this);
        }

        /// <summary>
        /// Get catalog view
        /// </summary>
        public ICatalogView GetCatalogView()
        {
            return new CatalogWindow();
        }

        /// <summary>
        /// Get search view
        /// </summary>
        public ISearchView GetSearchView()
        {
            return new SearchWindow();
        }
    }
}
