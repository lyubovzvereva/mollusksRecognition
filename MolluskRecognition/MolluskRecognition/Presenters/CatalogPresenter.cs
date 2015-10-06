using MolluskRecognition.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using MolluskRecognition.DAL.DataModels;
using System.ComponentModel;
using System.Windows.Input;
using MolluskRecognition.Commands;

namespace MolluskRecognition.Presenters
{
    public class CatalogPresenter : INotifyPropertyChanged, IPresenterBase
    {
        /// <summary>
        /// Catalog view
        /// </summary>
        private readonly ICatalogView _view;

        /// <summary>
        /// Handler of the main view
        /// </summary>
        private readonly Window _windowHandler;

        /// <summary>
        /// Main view
        /// </summary>
        private readonly IStartView _mainView;

        /// <summary>
        /// Constructor
        /// </summary>
        public CatalogPresenter(ICatalogView view, Window windowHandler, IStartView mainView)
        {
            this._view = view;
            this._windowHandler = windowHandler;
            this._mainView = mainView;
        }

        /// <summary>
        /// Activate presenter
        /// </summary>
        public void Activate()
        {
            Genuses = new ObservableCollection<Genus> {
                new Genus { 
                    Author = "author name",
                    Name = "some genus name",
                    Year = DateTime.Today,
                    Species = new List<Species> {
                        new Species {
                            Name = "some species name",
                            Age = "some age",
                            Author = "some author",
                            Year = DateTime.Today.AddYears(-10),
                            Locations = new List<Location> { 
                                new Location { 
                                    FileName = "IMG_6686.jpg" },
                                    new Location { 
                                    FileName = "IMG_6686.jpg" },
                                    new Location { 
                                    FileName = "IMG_6686.jpg" },
                                    new Location { 
                                    FileName = "IMG_6686.jpg" },
                                    new Location { 
                                    FileName = "IMG_6686.jpg" },
                                    new Location { 
                                    FileName = "IMG_6580.jpg" },
                                    new Location { 
                                    FileName = "IMG_6686.jpg" },
                                    new Location { 
                                    FileName = "IMG_6580.jpg" },
                                    new Location { 
                                    FileName = "IMG_6686.jpg" },
                                    new Location { 
                                    FileName = "IMG_6686.jpg" },
                                    new Location { 
                                    FileName = "IMG_6686.jpg" },
                                    new Location { 
                                    FileName = "IMG_6580.jpg" },
                                    new Location { 
                                    FileName = "IMG_6686.jpg" },
                                    new Location { 
                                    FileName = "IMG_6686.jpg" }
                            }
                        }
                    }
                }
            };
            SculptureTypes = new ObservableCollection<SculptureType> { SculptureType.Concentric, SculptureType.Radial, SculptureType.RadialOrConcentric };
            ShellTypes = new ObservableCollection<ShellType> { ShellType.A1, ShellType.A2, ShellType.B1, ShellType.B2, ShellType.G1, ShellType.G2, ShellType.G3 };
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
        /// List of available genuses
        /// </summary>
        private ObservableCollection<Genus> _genuses;

        /// <summary>
        /// List of available genuses
        /// </summary>
        public ObservableCollection<Genus> Genuses 
        {
            get { return _genuses; }
            set
            {
                _genuses = value;
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
        private ObservableCollection<ShellType> _shellTypes;

        /// <summary>
        /// Available shell types
        /// </summary>
        public ObservableCollection<ShellType> ShellTypes
        {
            get { return _shellTypes; }
            set
            {
                _shellTypes = value;
                OnPropertyChanged("ShellTypes");
            }
        }
        /// <summary>
        /// Available shell types
        /// </summary>
        private ObservableCollection<SculptureType> _sculptureTypes;

        /// <summary>
        /// Available shell types
        /// </summary>
        public ObservableCollection<SculptureType> SculptureTypes
        {
            get { return _sculptureTypes; }
            set
            {
                _sculptureTypes = value;
                OnPropertyChanged("SculptureTypes");
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
        /// Command to edit locations
        /// </summary>
        private ICommand _editLocationCommand;

        /// <summary>
        /// Command to edit locations
        /// </summary>
        public ICommand EditLocationCommand
        {
            get
            {
                if (_editLocationCommand == null)
                {
                    _editLocationCommand = new RelayCommand(x => EditLocation(), x => CanEditLocation());
                }
                return _editLocationCommand;
            }
            set
            {
                _editLocationCommand = value;
            }
        }


        /// <summary>
        /// Command to edit cuts
        /// </summary>
        private ICommand _editCutsCommand;

        /// <summary>
        /// Command to edit cuts
        /// </summary>
        public ICommand EditCutsCommand
        {
            get
            {
                if (_editCutsCommand == null)
                {
                    _editCutsCommand = new RelayCommand(x => EditCuts(), x => CanEditCuts());
                }
                return _editCutsCommand;
            }
            set
            {
                _editCutsCommand = value;
            }
        }

        /// <summary>
        /// Command to edit samples
        /// </summary>
        private ICommand _editSamplesCommand;

        /// <summary>
        /// Command to edit samples
        /// </summary>
        public ICommand EditSamplesCommand
        {
            get
            {
                if (_editSamplesCommand == null)
                {
                    _editSamplesCommand = new RelayCommand(x => EditSamples(), x => CanEditSamples());
                }
                return _editSamplesCommand;
            }
            set
            {
                _editSamplesCommand = value;
            }
        }
        /// <summary>
        /// Command to add new species
        /// </summary>
        private ICommand _addNewSpeciesCommand;

        /// <summary>
        /// Command to add new species
        /// </summary>
        public ICommand AddNewSpeciesCommand
        {
            get
            {
                if (_addNewSpeciesCommand == null)
                {
                    _addNewSpeciesCommand = new RelayCommand(x => AddNewSpecies(), x => CanAddNewSpecies());
                }
                return _addNewSpeciesCommand;
            }
            set
            {
                _addNewSpeciesCommand = value;
            }
        }


        /// <summary>
        /// Command to add new genus
        /// </summary>
        private ICommand _addNewGenusCommand;

        /// <summary>
        /// Command to add new genus
        /// </summary>
        public ICommand AddNewGenusCommand
        {
            get
            {
                if (_addNewGenusCommand == null)
                {
                    _addNewGenusCommand = new RelayCommand(x => AddNewGenus(), x => CanAddNewGenus());
                }
                return _addNewGenusCommand;
            }
            set
            {
                _addNewGenusCommand = value;
            }
        }


        #region commands methods

        /// <summary>
        /// Adding new species
        /// </summary>
        private void AddNewSpecies()
        {
            IAddNewSpeciesView speciesView = _mainView.GetAddNewSpeciesView();
            AddNewSpeciesPresenter speciesPresenter = new AddNewSpeciesPresenter(speciesView, _view.GetWindowHandler(), SelectedGenus);
            speciesPresenter.Activate();
            Species newSpecies = speciesPresenter.GetSpecies();
            if (newSpecies != null)
            {
                SelectedGenus.Species.Add(newSpecies);
                SelectedSpecies = newSpecies;
            }
        }

        /// <summary>
        /// If can add new species
        /// </summary>
        private bool CanAddNewSpecies()
        {
            // We can add new species if a genus selected
            return SelectedGenus != null;
        }

        /// <summary>
        /// If can add new genus
        /// </summary>
        private bool CanAddNewGenus()
        {
            // We always can add new genus
            return true;
        }

        /// <summary>
        /// Adding new genus
        /// </summary>
        private void AddNewGenus()
        {
            IAddGenusView genusView = _mainView.GetAddGenusView();
            AddNewGenusPresenter genusPresenter = new AddNewGenusPresenter(genusView, _view.GetWindowHandler());
            genusPresenter.Activate();
            Genus newGenus = genusPresenter.GetGenus();
            if(newGenus!= null)
            {
                Genuses.Add(newGenus);
                SelectedGenus = newGenus;
            }
        }

        /// <summary>
        /// If can edit samples
        /// </summary>
        private bool CanEditSamples()
        {
            //todo
            return true;
        }

        /// <summary>
        /// Editing samples
        /// </summary>
        private void EditSamples()
        {
            EditSamplesPresenter cutsPresenter = new EditSamplesPresenter(_mainView.GetEditSamplesView(), _view.GetWindowHandler(), SelectedSpecies);
            cutsPresenter.Activate();
            var newSamples = cutsPresenter.GetSamples();
            if (newSamples != null)
            {
                SelectedSpecies.Samples = newSamples;
            }
        }

        /// <summary>
        /// If can edit cuts
        /// </summary>
        private bool CanEditCuts()
        {
            // Can edit cuts if some species selected
            return SelectedSpecies != null;
        }

        /// <summary>
        /// Editing cuts
        /// </summary>
        private void EditCuts()
        {
            EditCutsPresenter cutsPresenter = new EditCutsPresenter(_mainView.GetEditCutsView(), _view.GetWindowHandler(), SelectedSpecies);
            cutsPresenter.Activate();
            var newSections = cutsPresenter.GetSections();
            if (newSections != null)
            {
                SelectedSpecies.Sections = newSections;
            }
        }

        /// <summary>
        /// If can edit location
        /// </summary>
        /// <returns></returns>
        private bool CanEditLocation()
        {
            // Can edit locations if some species selected
            return SelectedSpecies != null;
        }

        /// <summary>
        /// Editing locations
        /// </summary>
        private void EditLocation()
        {
            EditLocationsPresenter locationPresenter = new EditLocationsPresenter(_mainView.GetEditLocationsView(), _view.GetWindowHandler(), SelectedSpecies);
            locationPresenter.Activate();
            var newLocations = locationPresenter.GetLocations();
            if(newLocations != null)
            {
                SelectedSpecies.Locations = newLocations;
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
            this.Deactivate();
        }

        #endregion command methods
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
