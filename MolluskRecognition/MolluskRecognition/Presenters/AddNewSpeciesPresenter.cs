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
    /// Presenter of the adding new species view
    /// </summary>
    public class AddNewSpeciesPresenter : IPresenterBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Add new species view
        /// </summary>
        private readonly IAddNewSpeciesView _view;

        /// <summary>
        /// Handler of the parent window
        /// </summary>
        private readonly Window _windowHandler;

        /// <summary>
        /// If saved was pressed
        /// </summary>
        private bool _saved = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view"></param>
        /// <param name="windowHandler"></param>
        public AddNewSpeciesPresenter(IAddNewSpeciesView view, Window windowHandler, Genus currentGenus)
        {
            this._view = view;
            this._windowHandler = windowHandler;
            _saved = false;
            Genus = currentGenus;
            Species = new Species();
        }
        /// <summary>
        /// Activate view
        /// </summary>
        public void Activate()
        {
            _view.SetDataContext(this);
            _view.Activate(_windowHandler);
        }

        /// <summary>
        /// Deactivate view
        /// </summary>
        public void Deactivate()
        {
            _view.Deactivate();
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

        /// <summary>
        /// Selected species
        /// </summary>
        public Species species;

        /// <summary>
        /// Selected species
        /// </summary>
        public Species Species
        {
            get { return species; }
            set
            {
                species = value;
                OnPropertyChanged("Species");
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
            return Species != null && !string.IsNullOrEmpty(Species.Name);
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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        #endregion 
    
        /// <summary>
        /// Get filled species
        /// </summary>
        /// <returns></returns>
        internal Species GetSpecies()
        {
            return _saved ? Species : null;
        }
    }
}
