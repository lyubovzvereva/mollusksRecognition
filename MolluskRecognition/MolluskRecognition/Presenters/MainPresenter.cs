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
    public class MainPresenter: IPresenterBase
    {
        /// <summary>
        /// Main view
        /// </summary>
        private readonly IStartView _mainView;

        /// <summary>
        /// Catalog presenter
        /// </summary>
        private CatalogPresenter _catalogPresenter;

        /// <summary>
        /// Search window presenter
        /// </summary>
        private SearchPresenter _searchPresenter;

        /// <summary>
        /// Catalog view
        /// </summary>
        private ICatalogView _catalogView;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPresenter(IStartView startView)
        {
            this._mainView = startView;
        }

        /// <summary>
        /// Activate presenter
        /// </summary>
        public void Activate()
        {
            _mainView.SetDataContext(this);
            //set values
            _mainView.Activate(null);
        }

        /// <summary>
        /// Deactivate
        /// </summary>
        public void Deactivate()
        {
            _mainView.Deactivate();
        }
        #region bindings

        #region command bindings

        /// <summary>
        /// Command to show catalog
        /// </summary>
        private ICommand _showCatalogCommand;

        /// <summary>
        /// Command to show catalog
        /// </summary>
        public ICommand ShowCatalogCommand
        {
            get
            {
                if (_showCatalogCommand == null)
                {
                    _showCatalogCommand = new RelayCommand(x => ShowCatalog(), x => CanShowCatalog());
                }
                return _showCatalogCommand;
            }
            set
            {
                _showCatalogCommand = value;
            }
        }

        /// <summary>
        /// Command to show search window
        /// </summary>
        private ICommand _searchCommand;

        /// <summary>
        /// Command to show search window
        /// </summary>
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(x => ShowSearchWindow(), x => CanShowSearchWindow());
                }
                return _searchCommand;
            }
            set
            {
                _searchCommand = value;
            }
        }

        /// <summary>
        /// Command to show search window
        /// </summary>
        private ICommand _showMapCommand;

        /// <summary>
        /// Command to show search window
        /// </summary>
        public ICommand ShowMapCommand
        {
            get
            {
                if (_showMapCommand == null)
                {
                    _showMapCommand = new RelayCommand(x => ShowMapWindow(), x => CanShowMapWindow());
                }
                return _showMapCommand;
            }
            set
            {
                _showMapCommand = value;
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
            _searchPresenter = new SearchPresenter(_mainView.GetSearchView(), _mainView.GetWindowHandler());
            _searchPresenter.Activate();
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
            _catalogPresenter = new CatalogPresenter(_mainView.GetCatalogView(), _mainView.GetWindowHandler(), _mainView);
            _catalogPresenter.Activate();
        }
        #endregion command bindings
        #endregion bindings


    }
}
