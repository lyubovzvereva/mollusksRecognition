using MolluskRecognition.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MolluskRecognition.DataModels
{
    /// <summary>
    /// Location(Местоположение)
    /// todo: create folder for images
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Name of the file with location
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
