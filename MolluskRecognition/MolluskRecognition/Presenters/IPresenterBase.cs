using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolluskRecognition.Presenters
{
    /// <summary>
    /// Interface of all presenters
    /// </summary>
    public interface IPresenterBase
    {
        /// <summary>
        /// Activate presenter
        /// </summary>
        void Activate();

        /// <summary>
        /// Deactivate presenter
        /// </summary>
        void Deactivate();
    }
}
