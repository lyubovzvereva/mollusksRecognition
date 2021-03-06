﻿using MolluskRecognition.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MolluskRecognition.DataModels
{
    /// <summary>
    /// Sample of the species
    /// </summary>
    public class Sample
    {
        /// <summary>
        /// Some description of the sample
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Length of the shell
        /// </summary>
        public double L { get; set; }

        /// <summary>
        /// Length of the dorsal margin
        /// </summary>
        public double LSmall { get; set; }

        /// <summary>
        /// Length of the small shoulder
        /// </summary>
        public double L1 { get; set; }

        /// <summary>
        /// Lenght of the big shoulder
        /// </summary>
        public double L2 { get; set; }

        /// <summary>
        /// Height of the shell
        /// </summary>
        public double H { get; set; }

        /// <summary>
        /// Lenght of the diagonal
        /// </summary>
        public double D { get; set; }

        /// <summary>
        /// Angle betta
        /// </summary>
        public double AngleBetta { get; set; }

        /// <summary>
        /// Angle alfa
        /// </summary>
        public double AngleAlfa { get; set; }

        /// <summary>
        /// Location in the collection
        /// </summary>
        public Collection CollectionLocation { get; set; }

        /// <summary>
        /// Path to the photo
        /// </summary>
        public string PhotoFileName { get; set; }

        /// <summary>
        /// Uri to file which combines from settings and fileName
        /// </summary>
        public Uri UriSource
        {
            get { return new Uri(Path.Combine(Settings.Default.LocationsImagesLocation, PhotoFileName)); }
        }
        //todo: another charcteristics
    }
}
