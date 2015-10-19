using MolluskRecognition.Commands;
using MolluskRecognition.DAL.DataModels;
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
        private readonly IAddGenusView _genusView;

        /// <summary>
        /// Handler of parent window
        /// </summary>
        private readonly Window _windowHandler;

        /// <summary>
        /// If was saved
        /// </summary>
        private bool _saved = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="genusView"></param>
        /// <param name="windowHandler"></param>
        public AddNewGenusPresenter(IAddGenusView genusView, Window windowHandler)
        {
            this._genusView = genusView;
            this._windowHandler = windowHandler;
            _saved = false;
        }

        /// <summary>
        /// Activate presenter
        /// </summary>
        public void Activate()
        {
            Genus = new Genus();
            _genusView.SetDataContext(this);
            _genusView.Activate(_windowHandler);
        }

        /// <summary>
        /// Deactivate presenter
        /// </summary>
        public void Deactivate()
        {
            _genusView.Deactivate();
        }

        /// <summary>
        /// Get new genus
        /// </summary>
        /// <returns></returns>
        public Genus GetGenus()
        {
            return _saved ? Genus : null;
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
        private ICommand _saveCommand;

        /// <summary>
        /// Command to save all
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(x => Save(), x => CanSave());
                }
                return _saveCommand;
            }
            set
            {
                _saveCommand = value;
            }
        }

        /// <summary>
        /// Command to cancel
        /// </summary>
        private ICommand _cancelCommand;

        /// <summary>
        /// Command to cancel
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(x => Cancel(), x => CanCancel());
                }
                return _cancelCommand;
            }
            set
            {
                _cancelCommand = value;
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
            _saved = true;
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion 
    }
}
