using MolluskRecognition.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using MolluskRecognition.DataModels;
using System.ComponentModel;

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
