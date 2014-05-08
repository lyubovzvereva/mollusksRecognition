using MolluskRecognition.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MolluskRecognition.Views
{
    /// <summary>
    /// Interfaces of all views
    /// </summary>
    public interface IViewBase
    {
        /// <summary>
        /// Activate view
        /// </summary>
        void Activate(Window owner);

        /// <summary>
        /// Deactivate view
        /// </summary>
        void Deactivate();

        /// <summary>
        /// Set data context for the view
        /// </summary>
        void SetDataContext(IPresenterBase presenter);
    }
}
