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
    /// Presenter of edit samples
    /// </summary>
    public class EditSamplesPresenter : IPresenterBase, INotifyPropertyChanged
    {
         /// <summary>
        /// Edit sample view
        /// </summary>
        private IEditSamplesView view;

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
        public EditSamplesPresenter(IEditSamplesView view, Window windowHandler, Species currentSpecies)
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
            if (currentSpecies.Samples != null)
            {
                Samples = new ObservableCollection<Sample>(currentSpecies.Samples);
            }
            else Samples = new ObservableCollection<Sample>();
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
        private ObservableCollection<Sample> samples;

        /// <summary>
        /// Selected genus
        /// </summary>
        public ObservableCollection<Sample> Samples
        {
            get { return samples; }
            set
            {
                samples = value;
                OnPropertyChanged("Samples");
            }
        }

        /// <summary>
        /// Selected sample
        /// </summary>
        private Sample selectedSample;

        /// <summary>
        /// Selected sample
        /// </summary>
        public Sample SelectedSample
        {
            get { return selectedSample; }
            set
            {
                selectedSample = value;
                OnPropertyChanged("SelectedSample");
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
                    string newFileName = string.Format(Properties.Resources.LocationFileNamePattern, Properties.Settings.Default.LocationIndex, ext);
                    while(File.Exists(Path.Combine(Properties.Settings.Default.LocationsImagesLocation, newFileName)))
                    {
                        Properties.Settings.Default.LocationIndex = Properties.Settings.Default.LocationIndex +1;
                        newFileName = string.Format(Properties.Resources.LocationFileNamePattern, Properties.Settings.Default.LocationIndex,ext);
                    }
                    File.Copy(file, Path.Combine(Properties.Settings.Default.LocationsImagesLocation, newFileName));
                    Properties.Settings.Default.LocationIndex = Properties.Settings.Default.LocationIndex + 1;
                    Properties.Settings.Default.Save();
                    Samples.Add(new Sample { PhotoFileName = newFileName });
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

        /// <summary>
        /// Get filled samples
        /// null if not saved
        /// </summary>
        /// <returns></returns>
        public List<DataModels.Sample> GetSamples()
        {
            return saved ? samples.ToList() : null;
        }

    }
}
