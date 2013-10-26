using MolluskRecognition.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MolluskRecognition.Views
{
    /// <summary>
    /// Interface for catalog view
    /// </summary>
    public interface ICatalogView
    {
        /// <summary>
        /// Activate vie
        /// </summary>
        void Activate(Window owner);

        /// <summary>
        /// Deactivate view
        /// </summary>
        void Deactivate();

        /// <summary>
        /// Set data context for view
        /// </summary>
        void SetDataContext(CatalogPresenter presenter);
    }
}
