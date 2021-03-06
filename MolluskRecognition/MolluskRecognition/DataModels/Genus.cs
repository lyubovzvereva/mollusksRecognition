﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolluskRecognition.DataModels
{
    /// <summary>
    /// Genus(род)
    /// </summary>
    public class Genus
    {
        /// <summary>
        /// Name of the genus
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Author of the genus
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Genus discovery year
        /// </summary>
        public DateTime Year { get; set; }

        /// <summary>
        /// Species of the genus
        /// </summary>
        public List<Species> Species { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Genus()
        {
            Year = DateTime.Today;
        }
    }
}
