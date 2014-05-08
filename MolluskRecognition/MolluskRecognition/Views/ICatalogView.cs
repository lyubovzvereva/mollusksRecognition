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
    public interface ICatalogView: IViewBase
    {
        /// <summary>
        /// Get handler of the window
        /// </summary>
        /// <returns></returns>
        Window GetWindowHandler();
    }
}
