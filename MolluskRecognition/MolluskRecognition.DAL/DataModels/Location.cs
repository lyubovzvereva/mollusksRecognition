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
        public Location() { }

        public Location(Guid id, string fileName) : base(id)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Name of the file with location
        /// </summary>
        public string FileName { get; set; }
    }
}
