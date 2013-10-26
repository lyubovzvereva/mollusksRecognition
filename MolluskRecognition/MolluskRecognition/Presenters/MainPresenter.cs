using MolluskRecognition.Commands;
using MolluskRecognition.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MolluskRecognition.Presenters
{
    /// <summary>
    /// Main presenter of main view
    /// </summary>
    public class MainPresenter
    {
        /// <summary>
        /// Main view
        /// </summary>
        private IStartView mainView;

        /// <summary>
        /// Catalog presenter
        /// </summary>
        private CatalogPresenter catalogPresenter;

        /// <summary>
        /// Catalog view
        /// </summary>
        private ICatalogView catalogView;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPresenter(IStartView startView)
        {
            this.mainView = startView;
        }

        /// <summary>
        /// Activate presenter
        /// </summary>
        public void Activate()
        {
            mainView.SetDataContext(this);
            //set values
            mainView.Activate();
        }

        /// <summary>
        /// Deactivate
        /// </summary>
        public void Deactivate()
        {
            mainView.Deactivate();
        }
        #region bindings

        #region command bindings

        /// <summary>
        /// Command to show catalog
        /// </summary>
        private ICommand showCatalogCommand;

        /// <summary>
        /// Command to show catalog
        /// </summary>
        public ICommand ShowCatalogCommand
        {
            get
            {
                if (showCatalogCommand == null)
                {
                    showCatalogCommand = new RelayCommand(x => ShowCatalog(), x => CanShowCatalog());
                }
                return showCatalogCommand;
            }
            set
            {
                showCatalogCommand = value;
            }
        }

        /// <summary>
        /// Command to show search window
        /// </summary>
        private ICommand searchCommand;

        /// <summary>
        /// Command to show search window
        /// </summary>
        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand(x => ShowSearchWindow(), x => CanShowSearchWindow());
                }
                return searchCommand;
            }
            set
            {
                searchCommand = value;
            }
        }

        /// <summary>
        /// Command to show search window
        /// </summary>
        private ICommand showMapCommand;

        /// <summary>
        /// Command to show search window
        /// </summary>
        public ICommand ShowMapCommand
        {
            get
            {
                if (showMapCommand == null)
                {
                    showMapCommand = new RelayCommand(x => ShowMapWindow(), x => CanShowMapWindow());
                }
                return showMapCommand;
            }
            set
            {
                showMapCommand = value;
            }
        }

        /// <summary>
        /// Could map window be shown
        /// </summary>
        private bool CanShowMapWindow()
        {
            return true;
        }

        /// <summary>
        /// Showing the map window
        /// </summary>
        private void ShowMapWindow()
        {
            //todo
        }

        /// <summary>
        /// Could search window be shown
        /// </summary>
        private bool CanShowSearchWindow()
        {
            return true;
        }

        /// <summary>
        /// Showing the search window
        /// </summary>
        private void ShowSearchWindow()
        {
            //todo
        }

        /// <summary>
        /// Could catalog be shown
        /// </summary>
        private bool CanShowCatalog()
        {
            return true;
        }

        /// <summary>
        /// Showing catalog
        /// </summary>
        private void ShowCatalog()
        {
            catalogPresenter = new CatalogPresenter(mainView.GetCatalogView(), mainView.GetWindowHandler());
            catalogPresenter.Activate();
        }
        #endregion command bindings
        #endregion bindings


    }
}
