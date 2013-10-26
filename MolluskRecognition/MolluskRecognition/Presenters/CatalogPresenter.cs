using MolluskRecognition.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MolluskRecognition.Presenters
{
    public class CatalogPresenter
    {
        /// <summary>
        /// Catalog view
        /// </summary>
        private ICatalogView view;

        /// <summary>
        /// Handler of the main view
        /// </summary>
        private Window windowHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        public CatalogPresenter(ICatalogView view, Window windowHandler)
        {
            this.view = view;
            this.windowHandler = windowHandler;
        }

        /// <summary>
        /// Activate presenter
        /// </summary>
        public void Activate()
        {
            view.SetDataContext(this);
            view.Activate(windowHandler);
        }

        /// <summary>
        /// Deactivate presenter
        /// </summary>
        public void Deactivate()
        {
            view.Deactivate();
        }
    }
}
