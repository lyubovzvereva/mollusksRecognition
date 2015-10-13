using MolluskRecognition.Presenters;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using MolluskRecognition.DAL;
using MolluskRecognition.DAL.Queries;

namespace MolluskRecognition
{
    class Program
    {
        [Import] private ISettingsProvider _settingsProvider;

        [STAThread]
        static void Main(string[] args)
        {
            new Program().Start();
        }

        private void Start()
        {
            if (ComposeParts())
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", _settingsProvider.CurrentApplicationFolder);
                
                _settingsProvider.CheckRequiredFolders();
                var mainView = new StartWindow();
                var mainPresenter = new MainPresenter(mainView, _settingsProvider);
                mainPresenter.Activate();
            }
        }

        private bool ComposeParts()
        {
            try
            {
                var catalog = new AggregateCatalog();

                var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (exePath == null)
                    throw new CompositionException("Cannot resolve executing file path");

                catalog.Catalogs.Add(new DirectoryCatalog(exePath));

                var container = new CompositionContainer(catalog);

                container.ComposeParts(this);

                return true;
            }
            catch (CompositionException compositionException)
            {
                return false;
            }
        }
    }
}
