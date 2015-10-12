using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace MolluskRecognition.DAL
{
    /// <summary>
    /// Provider for configuration settings
    /// </summary>
    public interface ISettingsProvider
    {
        string LocationsImagesLocation { get; set; }
        long LocationIndex { get; set; }
        void Save();
        void CheckRequiredFolders();
    }

    [Export(typeof(ISettingsProvider))]
    public class SettingsProvider : ISettingsProvider
    {
        public string LocationsImagesLocation
        {
            get { return Settings.Default.LocationsImagesLocation; }
            set { Settings.Default.LocationsImagesLocation = value; }
        }

        public long LocationIndex
        {
            get { return Settings.Default.LocationIndex; }
            set { Settings.Default.LocationIndex = value; }
        }

        public void Save()
        {
            Settings.Default.Save();
        }

        public void CheckRequiredFolders()
        {
            if (string.IsNullOrEmpty(LocationsImagesLocation))
            {
                var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (exePath == null)
                    throw new ConfigurationErrorsException("Cannot resolve executing file path");

                LocationsImagesLocation = Path.Combine(exePath, "Pictures");

                Save();
            }

            if (!Directory.Exists(LocationsImagesLocation))
                Directory.CreateDirectory(LocationsImagesLocation);
        }
    }
}
