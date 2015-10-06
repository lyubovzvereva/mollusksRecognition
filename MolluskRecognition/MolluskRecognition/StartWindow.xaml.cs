using System.Collections.Generic;
using System.Windows;
using MolluskRecognition.DAL.DataModels;
using MolluskRecognition.Views;
using MolluskRecognition.Presenters;

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
        public void SetDataContext(IPresenterBase presenter)
        {
            this.DataContext = presenter;
        }

        /// <summary>
        /// Activate view
        /// </summary>
        public void Activate(Window owner)
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

        /// <summary>
        /// Get add genus view
        /// </summary>
        /// <returns></returns>
        public IAddGenusView GetAddGenusView()
        {
            return new AddGenusPopup();
        }

        /// <summary>
        /// Get add species view
        /// </summary>
        /// <returns></returns>
        public IAddNewSpeciesView GetAddNewSpeciesView()
        {
            return new AddNewSpeciesPopup();
        }

        /// <summary>
        /// Get edit locations view
        /// </summary>
        /// <returns></returns>
        public IEditLocationsView GetEditLocationsView()
        {
            return new EditLocationsView();
        }


        /// <summary>
        /// Get edit cuts view
        /// </summary>
        /// <returns></returns>
        public IEditCutsView GetEditCutsView()
        {
            return new EditCutsView();
        }

        /// <summary>
        /// Get edit samples view
        /// </summary>
        /// <returns></returns>
        public IEditSamplesView GetEditSamplesView()
        {
            return new EditSamplesView();
        }
    }
}
