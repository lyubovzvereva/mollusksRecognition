using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace MolluskRecognition.DAL.DataModels
{
    /// <summary>
    /// Разрезы
    /// </summary>
    public class Section : Entity
    {
        public Section() { }

        /// <summary>
        /// Name of the file with section
        /// </summary>
        public string FileName { get; set; }
    }
}
