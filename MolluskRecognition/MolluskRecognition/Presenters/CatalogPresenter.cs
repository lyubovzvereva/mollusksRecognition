using MolluskRecognition.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using MolluskRecognition.DataModels;
using System.ComponentModel;
using System.Windows.Input;
using MolluskRecognition.Commands;

namespace MolluskRecognition.Presenters
{
    public class CatalogPresenter : INotifyPropertyChanged
    {
        /// <summary>
        /// Catalog view
        /// </summary>
        private ICatalogView view;

        /// <summary>
        /// Handler of the main view
        /// </summary>
        private Window windowHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        public CatalogPresenter(ICatalogView view, Window windowHandler)
        {
            this.view = view;
            this.windowHandler = windowHandler;
        }

        /// <summary>
        /// Activate presenter
        /// </summary>
        public void Activate()
        {
            Genuses = new ObservableCollection<Genus> { new Genus { Author = "author name", Name = "some genus name", Year = DateTime.Today } };
            SpeciesList = new ObservableCollection<Species> { new Species { Name = "some species name", Age = "some age", Author = "some author", Year = DateTime.Today.AddYears(-10) } };
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
        /// List of available genuses
        /// </summary>
        private ObservableCollection<Genus> genuses;

        /// <summary>
        /// List of available genuses
        /// </summary>
        public ObservableCollection<Genus> Genuses 
        {
            get { return genuses; }
            set
            {
                genuses = value;
                OnPropertyChanged("Genuses");
            }
        }

        /// <summary>
        /// Selected genus
        /// </summary>
        public Genus selectedGenus;

        /// <summary>
        /// Selected genus
        /// </summary>
        public Genus SelectedGenus
        {
            get { return selectedGenus; }
            set
            {
                selectedGenus = value;
                OnPropertyChanged("SelectedGenus");
            }
        }

        /// <summary>
        /// List of available species
        /// </summary>
        private ObservableCollection<Species> speciesList;

        /// <summary>
        /// List of available species
        /// todo: get it from selected genus
        /// </summary>
        public ObservableCollection<Species> SpeciesList
        {
            get { return speciesList; }
            set
            {
                speciesList = value;
                OnPropertyChanged("SpeciesList");
            }
        }
        /// <summary>
        /// Selected genus
        /// </summary>
        public Species selectedSpecies;

        /// <summary>
        /// Selected genus
        /// </summary>
        public Species SelectedSpecies
        {
            get { return selectedSpecies; }
            set
            {
                selectedSpecies = value;
                OnPropertyChanged("SelectedSpecies");
            }
        }
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


        #region commands methods
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
            this.Deactivate();
        }

        #endregion command methods
        #endregion command bindings


        #endregion fields bindings
        #endregion bindings

        #region Inotify property
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        #endregion 
    }
}
