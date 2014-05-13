using MolluskRecognition.Presenters;
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

namespace MolluskRecognition
{
    /// <summary>
    /// Interaction logic for EditLocationsView.xaml
    /// </summary>
    public partial class EditLocationsView : Window, IEditLocationsView
    {
        public EditLocationsView()
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
    }
}
