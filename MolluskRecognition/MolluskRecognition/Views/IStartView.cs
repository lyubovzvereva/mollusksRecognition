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
    public interface IStartView
    {
        /// <summary>
        /// Set data context for view
        /// </summary>
        void SetDataContext(MainPresenter presenter);

        /// <summary>
        /// Activate view
        /// </summary>
        void Activate();

        /// <summary>
        /// Deactivate view
        /// </summary>
        void Deactivate();

        /// <summary>
        /// Get handler of main window
        /// </summary>
        Window GetWindowHandler();

        /// <summary>
        /// Get catalog view
        /// </summary>
        ICatalogView GetCatalogView();
    }
}
