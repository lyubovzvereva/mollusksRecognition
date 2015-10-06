using MolluskRecognition.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MolluskRecognition.Presenters
{
    public class SearchPresenter: IPresenterBase
    {
        /// <summary>
        /// Search window
        /// </summary>
        private readonly ISearchView _view;

        /// <summary>
        /// Handler of the main window
        /// </summary>
        private readonly Window _windowHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchPresenter(ISearchView view, Window windowHandler)
        {
            this._view = view;
            this._windowHandler = windowHandler;
        }

        /// <summary>
        /// Activate presenter
        /// </summary>
        public void Activate()
        {
            _view.SetDataContext(this);
            _view.Activate(_windowHandler);
        }

        /// <summary>
        /// Deactivate presenter
        /// </summary>
        public void Deactivate()
        {
            _view.Deactivate();
        }
    }
}
