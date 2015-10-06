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
    public class EditCutsPresenter : IPresenterBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Edit locations view
        /// </summary>
        private readonly IEditCutsView _view;

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
        /// <param name="view">Edit cuts view</param>
        /// <param name="windowHandler">handler to parent window</param>
        /// <param name="currentSpecies">selected species</param>
        public EditCutsPresenter(IEditCutsView view, Window windowHandler, Species currentSpecies, ISettingsProvider settingsProvider)
        {
            this._view = view;
            this._windowHandler = windowHandler;
            this._currentSpecies = currentSpecies;
            _settingsProvider = settingsProvider;
            _saved = false;
        }

        /// <summary>
        /// Activate presenter
        /// </summary>
        public void Activate()
        {
            if (_currentSpecies.Sections != null)
            {
                Sections = new ObservableCollection<Section>(_currentSpecies.Sections);
            }
            else Sections = new ObservableCollection<Section>();
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
        private ObservableCollection<Section> _sections;

        /// <summary>
        /// Selected genus
        /// </summary>
        public ObservableCollection<Section> Sections
        {
            get { return _sections; }
            set
            {
                _sections = value;
                OnPropertyChanged("Sections");
            }
        }

        /// <summary>
        /// Selected location
        /// </summary>
        private Section _selectedSection;

        /// <summary>
        /// Selected location
        /// </summary>
        public Section SelectedSection
        {
            get { return _selectedSection; }
            set
            {
                _selectedSection = value;
                OnPropertyChanged("SelectedSection");
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
        /// If new location can be added
        /// </summary>
        /// <returns></returns>
        private bool CanAddLocation()
        {
            return true;
        }

        /// <summary>
        /// Adding new location
        /// </summary>
        /// <returns></returns>
        private void AddLocation()
        {
            OpenFileDialog dialog = new OpenFileDialog() { Multiselect = true };
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                foreach (string file in dialog.FileNames)
                {
                    string ext = Path.GetExtension(file);
                    string newFileName = string.Format(Properties.Resources.LocationFileNamePattern, _settingsProvider.LocationIndex, ext);
                    while (File.Exists(Path.Combine(_settingsProvider.LocationsImagesLocation, newFileName)))
                    {
                        _settingsProvider.LocationIndex = _settingsProvider.LocationIndex + 1;
                        newFileName = string.Format(Properties.Resources.LocationFileNamePattern, _settingsProvider.LocationIndex, ext);
                    }
                    File.Copy(file, Path.Combine(_settingsProvider.LocationsImagesLocation, newFileName));
                    _settingsProvider.LocationIndex = _settingsProvider.LocationIndex + 1;
                    _settingsProvider.Save();
                    Sections.Add(new Section(_settingsProvider) { FileName = newFileName });
                }
            }
        }

        /// <summary>
        /// If selected location can be deleted
        /// </summary>
        /// <returns></returns>
        private bool CanDeleteLocation()
        {
            return SelectedSection != null;
        }

        /// <summary>
        /// Deleting selected location
        /// </summary>
        /// <returns></returns>
        private void DeleteLocation()
        {
            MessageBoxResult result = MessageBox.Show(Properties.Resources.DeleteLocationQuestion, Properties.Resources.WarningMessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                Sections.Remove(SelectedSection);
                SelectedSection = null;
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
        #endregion bindingss

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
        /// Get filled sections
        /// null if not saved
        /// </summary>
        /// <returns></returns>
        public List<Section> GetSections()
        {
            return _saved ? _sections.ToList() : null;
        }
    }
}
