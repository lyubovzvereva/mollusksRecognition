using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace MolluskRecognition.DAL.DataModels
{
    /// <summary>
    /// Location(Местоположение)
    /// todo: create folder for images
    /// </summary>
    public class Location : Entity
    {
        private readonly string _imagesLocation;
        public Location(string imagesLocation, string fileName)
        {
            this._imagesLocation = imagesLocation;
            FileName = fileName;
        }

        /// <summary>
        /// Name of the file with location
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Uri to file which combines from settings and fileName
        /// </summary>
        public Uri UriSource
        {
            get { return new Uri(Path.Combine(_imagesLocation, FileName)); }
        }
    }
}
