using MolluskRecognition.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MolluskRecognition.DataModels
{
    /// <summary>
    /// Разрезы
    /// </summary>
    public class Section
    {
        /// <summary>
        /// Name of the file with section
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Uri to file which combines from settings and fileName
        /// </summary>
        public Uri UriSource
        {
            get { return new Uri(Path.Combine(Settings.Default.LocationsImagesLocation, FileName)); }
        }
    }
}
