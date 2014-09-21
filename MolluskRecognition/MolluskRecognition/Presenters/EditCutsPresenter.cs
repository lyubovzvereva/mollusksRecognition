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
    public class EditCutsPresenter : IPresenterBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Edit locations view
        /// </summary>
        private IEditCutsView view;

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
        /// <param name="view">Edit cuts view</param>
        /// <param name="windowHandler">handler to parent window</param>
        /// <param name="currentSpecies">selected species</param>
        public EditCutsPresenter(IEditCutsView view, Window windowHandler, Species currentSpecies)
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
            if (currentSpecies.Sections != null)
            {
                Sections = new ObservableCollection<Section>(currentSpecies.Sections);
            }
            else Sections = new ObservableCollection<Section>();
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
        private ObservableCollection<Section> sections;

        /// <summary>
        /// Selected genus
        /// </summary>
        public ObservableCollection<Section> Sections
        {
            get { return sections; }
            set
            {
                sections = value;
                OnPropertyChanged("Sections");
            }
        }

        /// <summary>
        /// Selected location
        /// </summary>
        private Section selectedSection;

        /// <summary>
        /// Selected location
        /// </summary>
        public Section SelectedSection
        {
            get { return selectedSection; }
            set
            {
                selectedSection = value;
                OnPropertyChanged("SelectedSection");
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
            if (result == true)
            {
                foreach (string file in dialog.FileNames)
                {
                    string ext = Path.GetExtension(file);
                    string newFileName = string.Format(Properties.Resources.LocationFileNamePattern, Properties.Settings.Default.LocationIndex, ext);
                    while (File.Exists(Path.Combine(Properties.Settings.Default.LocationsImagesLocation, newFileName)))
                    {
                        Properties.Settings.Default.LocationIndex = Properties.Settings.Default.LocationIndex + 1;
                        newFileName = string.Format(Properties.Resources.LocationFileNamePattern, Properties.Settings.Default.LocationIndex, ext);
                    }
                    File.Copy(file, Path.Combine(Properties.Settings.Default.LocationsImagesLocation, newFileName));
                    Properties.Settings.Default.LocationIndex = Properties.Settings.Default.LocationIndex + 1;
                    Properties.Settings.Default.Save();
                    Sections.Add(new Section { FileName = newFileName });
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
            saved = true;
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
        public List<DataModels.Section> GetSections()
        {
            return saved ? sections.ToList() : null;
        }
    }
}
