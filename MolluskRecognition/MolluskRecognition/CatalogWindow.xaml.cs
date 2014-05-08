using MolluskRecognition.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MolluskRecognition.Presenters;

namespace MolluskRecognition
{
	/// <summary>
	/// Interaction logic for CatalogWindow.xaml
	/// </summary>
    public partial class CatalogWindow : Window, ICatalogView
	{
        /// <summary>
        /// Constructor
        /// </summary>
		public CatalogWindow()
		{
			InitializeComponent();
		}

        /// <summary>
        /// Activate view
        /// </summary>
        public void Activate(Window owner)
        {
            this.Owner = owner;
            this.ShowDialog();
        }

        /// <summary>
        /// Deactivate view
        /// </summary>
        public void Deactivate()
        {
            this.Close();
        }

        /// <summary>
        /// Set data context for the view
        /// </summary>
        public void SetDataContext(IPresenterBase presenter)
        {
            this.DataContext = presenter;
        }

        /// <summary>
        /// Get handler of the window
        /// </summary>
        /// <returns></returns>
        public Window GetWindowHandler()
        {
            return Window.GetWindow(this);
        }
    }
}
