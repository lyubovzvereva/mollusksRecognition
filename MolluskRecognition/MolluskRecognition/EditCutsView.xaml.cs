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
    /// Interaction logic for EditCutsView.xaml
    /// </summary>
    public partial class EditCutsView : Window, IEditCutsView
    {
        public EditCutsView()
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
        public void SetDataContext(Presenters.IPresenterBase presenter)
        {
            this.DataContext = presenter;
        }
    }
}
