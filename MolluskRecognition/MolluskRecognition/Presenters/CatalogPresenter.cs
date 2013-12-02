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
            Genuses = new ObservableCollection<Genus> { new Genus { Author = "author name", Name = "some genus name", Year = DateTime.Today, Species = new List<Species> { new Species { Name = "some species name", Age = "some age", Author = "some author", Year = DateTime.Today.AddYears(-10) } } } };
            SculptureTypes = new ObservableCollection<SculptureType> { SculptureType.Concentric, SculptureType.Radial, SculptureType.RadialOrConcentric };
            ShellTypes = new ObservableCollection<ShellType> { ShellType.A1, ShellType.A2, ShellType.B1, ShellType.B2, ShellType.G1, ShellType.G2, ShellType.G3 };
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

        /// <summary>
        /// Available shell types
        /// </summary>
        private ObservableCollection<ShellType> shellTypes;

        /// <summary>
        /// Available shell types
        /// </summary>
        public ObservableCollection<ShellType> ShellTypes
        {
            get { return shellTypes; }
            set
            {
                shellTypes = value;
                OnPropertyChanged("ShellTypes");
            }
        }
        /// <summary>
        /// Available shell types
        /// </summary>
        private ObservableCollection<SculptureType> sculptureTypes;

        /// <summary>
        /// Available shell types
        /// </summary>
        public ObservableCollection<SculptureType> SculptureTypes
        {
            get { return sculptureTypes; }
            set
            {
                sculptureTypes = value;
                OnPropertyChanged("SculptureTypes");
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

        /// <summary>
        /// Command to edit locations
        /// </summary>
        private ICommand editLocationCommand;

        /// <summary>
        /// Command to edit locations
        /// </summary>
        public ICommand EditLocationCommand
        {
            get
            {
                if (editLocationCommand == null)
                {
                    editLocationCommand = new RelayCommand(x => EditLocation(), x => CanEditLocation());
                }
                return editLocationCommand;
            }
            set
            {
                editLocationCommand = value;
            }
        }


        /// <summary>
        /// Command to edit cuts
        /// </summary>
        private ICommand editCutsCommand;

        /// <summary>
        /// Command to edit cuts
        /// </summary>
        public ICommand EditCutsCommand
        {
            get
            {
                if (editCutsCommand == null)
                {
                    editCutsCommand = new RelayCommand(x => EditCuts(), x => CanEditCuts());
                }
                return editCutsCommand;
            }
            set
            {
                editCutsCommand = value;
            }
        }

        /// <summary>
        /// Command to edit samples
        /// </summary>
        private ICommand editSamplesCommand;

        /// <summary>
        /// Command to edit samples
        /// </summary>
        public ICommand EditSamplesCommand
        {
            get
            {
                if (editSamplesCommand == null)
                {
                    editSamplesCommand = new RelayCommand(x => EditSamples(), x => CanEditSamples());
                }
                return editSamplesCommand;
            }
            set
            {
                editSamplesCommand = value;
            }
        }
        /// <summary>
        /// Command to add new species
        /// </summary>
        private ICommand addNewSpeciesCommand;

        /// <summary>
        /// Command to add new species
        /// </summary>
        public ICommand AddNewSpeciesCommand
        {
            get
            {
                if (addNewSpeciesCommand == null)
                {
                    addNewSpeciesCommand = new RelayCommand(x => AddNewSpecies(), x => CanAddNewSpecies());
                }
                return addNewSpeciesCommand;
            }
            set
            {
                addNewSpeciesCommand = value;
            }
        }


        /// <summary>
        /// Command to add new genus
        /// </summary>
        private ICommand addNewGenusCommand;

        /// <summary>
        /// Command to add new genus
        /// </summary>
        public ICommand AddNewGenusCommand
        {
            get
            {
                if (addNewGenusCommand == null)
                {
                    addNewGenusCommand = new RelayCommand(x => AddNewGenus(), x => CanAddNewGenus());
                }
                return addNewGenusCommand;
            }
            set
            {
                addNewGenusCommand = value;
            }
        }


        #region commands methods

        private object AddNewSpecies()
        {
            throw new NotImplementedException();
        }

        private bool CanAddNewSpecies()
        {
            throw new NotImplementedException();
        }

        private bool CanAddNewGenus()
        {
            throw new NotImplementedException();
        }

        private object AddNewGenus()
        {
            throw new NotImplementedException();
        }

        private bool CanEditSamples()
        {
            throw new NotImplementedException();
        }

        private object EditSamples()
        {
            throw new NotImplementedException();
        }


        private bool CanEditCuts()
        {
            throw new NotImplementedException();
        }

        private object EditCuts()
        {
            throw new NotImplementedException();
        }

        private bool CanEditLocation()
        {
            throw new NotImplementedException();
        }

        private object EditLocation()
        {
            throw new NotImplementedException();
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
