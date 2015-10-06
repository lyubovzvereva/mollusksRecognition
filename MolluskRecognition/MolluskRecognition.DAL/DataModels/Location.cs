using System;
using System.IO;

namespace MolluskRecognition.DAL.DataModels
{
    /// <summary>
    /// Location(Местоположение)
    /// todo: create folder for images
    /// </summary>
    public class Location
    {
        private readonly ISettingsProvider _settingsProvider;
        public Location(ISettingsProvider settingsProvider)
        {
            this._settingsProvider = settingsProvider;
        }

        /// <summary>
        /// Name of the file with location
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Uri to file which combines from settings and fileName
        /// </summary>
        public Uri UriSource
        {
            get { return new Uri(Path.Combine(_settingsProvider.LocationsImagesLocation, FileName)); }
        }
    }
}
