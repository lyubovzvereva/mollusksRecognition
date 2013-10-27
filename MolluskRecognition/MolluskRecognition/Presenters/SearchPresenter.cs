using MolluskRecognition.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MolluskRecognition.Presenters
{
    public class SearchPresenter
    {
        /// <summary>
        /// Search window
        /// </summary>
        private ISearchView view;

        /// <summary>
        /// Handler of the main window
        /// </summary>
        private Window windowHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchPresenter(ISearchView view, Window windowHandler)
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
