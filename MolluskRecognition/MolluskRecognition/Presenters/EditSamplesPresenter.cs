using Microsoft.Win32;
using MolluskRecognition.Commands;
using MolluskRecognition.DAL.DataModels;
using MolluskRecognition.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using MolluskRecognition.DAL;

namespace MolluskRecognition.Presenters
{
    /// <summary>
    /// Presenter of edit samples
    /// </summary>
    public class EditSamplesPresenter : IPresenterBase, INotifyPropertyChanged
    {
         /// <summary>
        /// Edit sample view
        /// </summary>
        private readonly IEditSamplesView _view;

        /// <summary>
        /// Handler of parent window
        /// </summary>
        private readonly Window _windowHandler;

        /// <summary>
        /// If changed should be saved
        /// </summary>
        private bool _saved = false;

        /// <summary>
        /// Current species
        /// </summary>
        private readonly Species _currentSpecies;

        private readonly ISettingsProvider _settingsProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view"></param>
        /// <param name="windowHandler"></param>
        public EditSamplesPresenter(IEditSamplesView view, Window windowHandler, Species currentSpecies, ISettingsProvider settingsProvider)
        {
            this._view = view;
            this._windowHandler = windowHandler;
            this._currentSpecies = currentSpecies;
            _saved = false;
            _settingsProvider = settingsProvider;
        }

        /// <summary>
        /// Activate presenter
        /// </summary>
        public void Activate()
        {
            if (_currentSpecies.Samples != null)
            {
                Samples = new ObservableCollection<Sample>(_currentSpecies.Samples);
            }
            else Samples = new ObservableCollection<Sample>();
            _view.SetDataContext(this);
            _view.Activate(_windowHandler);
        }

        /// <summary>
        /// Deactivate presenter
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
        private ObservableCollection<Sample> _samples;

        /// <summary>
        /// Selected genus
        /// </summary>
        public ObservableCollection<Sample> Samples
        {
            get { return _samples; }
            set
            {
                _samples = value;
                OnPropertyChanged("Samples");
            }
        }

        /// <summary>
        /// Selected sample
        /// </summary>
        private Sample _selectedSample;

        /// <summary>
        /// Selected sample
        /// </summary>
        public Sample SelectedSample
        {
            get { return _selectedSample; }
            set
            {
                _selectedSample = value;
                OnPropertyChanged("SelectedSample");
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
        /// Command to add new image
        /// </summary>
        private ICommand _addCommand;

        /// <summary>
        /// Command to add new image
        /// </summary>
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(x => AddLocation(), x => CanAddLocation());
                }
                return _addCommand;
            }
            set
            {
                _addCommand = value;
            }
        }

        /// <summary>
        /// Command to delete selected image
        /// </summary>
        private ICommand _deleteCommand;

        /// <summary>
        /// Command to delete selected image
        /// </summary>
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(x => DeleteLocation(), x => CanDeleteLocation());
                }
                return _deleteCommand;
            }
            set
            {
                _deleteCommand = value;
            }
        }

        /// <summary>
        /// If new sample can be added
        /// </summary>
        /// <returns></returns>
        private bool CanAddLocation()
        {
            return true;
        }

        /// <summary>
        /// Adding new sample
        /// </summary>
        /// <returns></returns>
        private void AddLocation()
        {
            OpenFileDialog dialog = new OpenFileDialog() { Multiselect = true };
            bool? result = dialog.ShowDialog();
            if(result == true)
            {
                foreach(string file in dialog.FileNames)
                {
                    string ext = Path.GetExtension(file);
                    string newFileName = string.Format(Properties.Resources.LocationFileNamePattern, _settingsProvider.LocationIndex, ext);
                    while(File.Exists(Path.Combine(_settingsProvider.LocationsImagesLocation, newFileName)))
                    {
                        _settingsProvider.LocationIndex = _settingsProvider.LocationIndex + 1;
                        newFileName = string.Format(Properties.Resources.LocationFileNamePattern, _settingsProvider.LocationIndex, ext);
                    }
                    File.Copy(file, Path.Combine(_settingsProvider.LocationsImagesLocation, newFileName));
                    _settingsProvider.LocationIndex = _settingsProvider.LocationIndex + 1;
                    _settingsProvider.Save();
                    Samples.Add(new Sample(_settingsProvider) { PhotoFileName = newFileName });
                }
            }
        }

        /// <summary>
        /// If selected sample can be deleted
        /// </summary>
        /// <returns></returns>
        private bool CanDeleteLocation()
        {
            return SelectedSample != null;
        }

        /// <summary>
        /// Deleting selected sample
        /// </summary>
        /// <returns></returns>
        private void DeleteLocation()
        {
            MessageBoxResult result = MessageBox.Show(Properties.Resources.DeleteLocationQuestion, Properties.Resources.WarningMessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                Samples.Remove(SelectedSample);
                SelectedSample = null;
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
            return true;
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
        /// Get filled samples
        /// null if not saved
        /// </summary>
        /// <returns></returns>
        public List<Sample> GetSamples()
        {
            return _saved ? _samples.ToList() : null;
        }

    }
}
