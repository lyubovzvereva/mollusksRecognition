using MolluskRecognition.Commands;
using MolluskRecognition.DataModels;
using MolluskRecognition.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MolluskRecognition.Presenters
{
    /// <summary>
    /// Presenter of adding new genus
    /// </summary>
    public class AddNewGenusPresenter : IPresenterBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Add genus view
        /// </summary>
        private IAddGenusView genusView;

        /// <summary>
        /// Handler of parent window
        /// </summary>
        private Window windowHandler;

        /// <summary>
        /// If was saved
        /// </summary>
        private bool saved = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="genusView"></param>
        /// <param name="windowHandler"></param>
        public AddNewGenusPresenter(IAddGenusView genusView, Window windowHandler)
        {
            this.genusView = genusView;
            this.windowHandler = windowHandler;
            saved = false;
        }

        /// <summary>
        /// Activate presenter
        /// </summary>
        public void Activate()
        {
            Genus = new DataModels.Genus();
            genusView.SetDataContext(this);
            genusView.Activate(windowHandler);
        }

        /// <summary>
        /// Deactivate presenter
        /// </summary>
        public void Deactivate()
        {
            genusView.Deactivate();
        }

        /// <summary>
        /// Get new genus
        /// </summary>
        /// <returns></returns>
        public Genus GetGenus()
        {
            return saved ? Genus : null;
        }

        #region bindings
        #region fields bindings
        /// <summary>
        /// Selected genus
        /// </summary>
        public Genus genus;

        /// <summary>
        /// Selected genus
        /// </summary>
        public Genus Genus
        {
            get { return genus; }
            set
            {
                genus = value;
                OnPropertyChanged("Genus");
            }
        }
        #endregion fields bindings

        #region command bindings
        /// <summary>
        /// Command to save all
        /// </summary>
        private ICommand saveCommand;

        /// <summary>
        /// Command to save all
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(x => Save(), x => CanSave());
                }
                return saveCommand;
            }
            set
            {
                saveCommand = value;
            }
        }

        /// <summary>
        /// Command to cancel
        /// </summary>
        private ICommand cancelCommand;

        /// <summary>
        /// Command to cancel
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(x => Cancel(), x => CanCancel());
                }
                return cancelCommand;
            }
            set
            {
                cancelCommand = value;
            }
        }
        /// <summary>
        /// If can cancel
        /// </summary>
        private bool CanCancel()
        {
            return true;
        }

        /// <summary>
        /// Cancelling
        /// </summary>
        private void Cancel()
        {
            this.Deactivate();
        }

        /// <summary>
        /// If can save enterred data
        /// </summary>
        private bool CanSave()
        {
            return Genus != null && !string.IsNullOrEmpty(Genus.Name);
        }

        /// <summary>
        /// Saving data
        /// </summary>
        private void Save()
        {
            MessageBox.Show("Saved");
            saved = true;
            this.Deactivate();
        }

        #endregion command bindings
        #endregion bindings

        #region Inotify property
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raising onPropertyChanged event to notify UI
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        #endregion 
    }
}
