using Microsoft.Win32;
using MolluskRecognition.Commands;
using MolluskRecognition.DataModels;
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

namespace MolluskRecognition.Presenters
{
    /// <summary>
    /// Presenter of editing locations
    /// todo: setup user settings with files location
    /// todo: maximize on click
    /// </summary>
    public class EditLocationsPresenter : IPresenterBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Edit locations view
        /// </summary>
        private IEditLocationsView view;

        /// <summary>
        /// Handler of parent window
        /// </summary>
        private Window windowHandler;

        /// <summary>
        /// If changed should be saved
        /// </summary>
        private bool saved = false;

        /// <summary>
        /// Current species
        /// </summary>
        private Species currentSpecies;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view"></param>
        /// <param name="windowHandler"></param>
        public EditLocationsPresenter(IEditLocationsView view, Window windowHandler, Species currentSpecies)
        {
            this.view = view;
            this.windowHandler = windowHandler;
            this.currentSpecies = currentSpecies;
            saved = false;
        }

        /// <summary>
        /// Activate presenter
        /// </summary>
        public void Activate()
        {
            Locations = new ObservableCollection<Location>(currentSpecies.Locations);
            view.SetDataContext(this);
            view.Activate(windowHandler);
        }

        /// <summary>
        /// Deactivate presenter
        /// </summary>
        public void Deactivate()
        {
            view.Deactivate();
        }
        #region bindings
        #region fields bindings
        /// <summary>
        /// Selected genus
        /// </summary>
        private ObservableCollection<Location> locations;

        /// <summary>
        /// Selected genus
        /// </summary>
        public ObservableCollection<Location> Locations
        {
            get { return locations; }
            set
            {
                locations = value;
                OnPropertyChanged("Locations");
            }
        }

        /// <summary>
        /// Selected location
        /// </summary>
        private Location selectedLocation;

        /// <summary>
        /// Selected location
        /// </summary>
        public Location SelectedLocation
        {
            get { return selectedLocation; }
            set
            {
                selectedLocation = value;
                OnPropertyChanged("SelectedLocation");
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
        /// Command to add new image
        /// </summary>
        private ICommand addCommand;

        /// <summary>
        /// Command to add new image
        /// </summary>
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(x => AddLocation(), x => CanAddLocation());
                }
                return addCommand;
            }
            set
            {
                addCommand = value;
            }
        }

        /// <summary>
        /// Command to delete selected image
        /// </summary>
        private ICommand deleteCommand;

        /// <summary>
        /// Command to delete selected image
        /// </summary>
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(x => DeleteLocation(), x => CanDeleteLocation());
                }
                return deleteCommand;
            }
            set
            {
                deleteCommand = value;
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
            if(result == true)
            {
                foreach(string file in dialog.FileNames)
                {
                    string ext = Path.GetExtension(file);
                    string newFileName = string.Format(Properties.Resources.LocationFileNamePattern, Properties.Settings.Default.LocationIndex, ext);
                    while(File.Exists(Path.Combine(Properties.Settings.Default.LocationsImagesLocation, newFileName)))
                    {
                        Properties.Settings.Default.LocationIndex = Properties.Settings.Default.LocationIndex +1;
                        newFileName = string.Format(Properties.Resources.LocationFileNamePattern, Properties.Settings.Default.LocationIndex,ext);
                    }
                    File.Copy(file, Path.Combine(Properties.Settings.Default.LocationsImagesLocation, newFileName));
                    Properties.Settings.Default.LocationIndex = Properties.Settings.Default.LocationIndex + 1;
                    Properties.Settings.Default.Save();
                    Locations.Add(new Location { FileName = newFileName });
                }
            }
        }

        /// <summary>
        /// If selected location can be deleted
        /// </summary>
        /// <returns></returns>
        private bool CanDeleteLocation()
        {
            return SelectedLocation != null;
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
                Locations.Remove(SelectedLocation);
                SelectedLocation = null;
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
