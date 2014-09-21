using MolluskRecognition.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MolluskRecognition.Views
{
    /// <summary>
    /// Interface of start view 
    /// </summary>
    public interface IStartView: IViewBase
    {
        /// <summary>
        /// Get handler of main window
        /// </summary>
        Window GetWindowHandler();

        /// <summary>
        /// Get catalog view
        /// </summary>
        ICatalogView GetCatalogView();

        /// <summary>
        /// Get search view
        /// </summary>
        ISearchView GetSearchView();

        /// <summary>
        /// Get add genus view
        /// </summary>
        /// <returns></returns>
        IAddGenusView GetAddGenusView();

        /// <summary>
        /// Get add species view
        /// </summary>
        /// <returns></returns>
        IAddNewSpeciesView GetAddNewSpeciesView();

        /// <summary>
        /// Get edit locations view
        /// </summary>
        /// <returns></returns>
        IEditLocationsView GetEditLocationsView();

        /// <summary>
        /// Get edit cuts view
        /// </summary>
        /// <returns></returns>
        IEditCutsView GetEditCutsView();

        /// <summary>
        /// Get edit samples view
        /// </summary>
        /// <returns></returns>
        IEditSamplesView GetEditSamplesView();
    }
}
