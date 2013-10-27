using MolluskRecognition.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MolluskRecognition.Views
{
    public interface ISearchView
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
        void SetDataContext(SearchPresenter presenter);
    }
}
