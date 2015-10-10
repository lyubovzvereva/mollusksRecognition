using MolluskRecognition.Presenters;
using MolluskRecognition.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MolluskRecognition.DAL;

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
            try
            {
                var catalog = new AggregateCatalog();

                var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (exePath == null)
                    throw new CompositionException("Cannot resolve executing file path");

                catalog.Catalogs.Add(new DirectoryCatalog(exePath));

                var container = new CompositionContainer(catalog);

                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                throw;
            }

            var mainView = new StartWindow();
            var mainPresenter = new MainPresenter(mainView, _settingsProvider);
            mainPresenter.Activate();
        }
    }
}
