using System;
using System.IO;

namespace MolluskRecognition.DAL.DataModels
{
    /// <summary>
    /// Разрезы
    /// </summary>
    public class Section
    {
        private readonly ISettingsProvider _settingsProvider;
        public Section(ISettingsProvider settingsProvider)
        {
            this._settingsProvider = settingsProvider;
        }

        /// <summary>
        /// Name of the file with section
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
