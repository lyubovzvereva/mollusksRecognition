using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolluskRecognition.DataModels
{
    /// <summary>
    /// Class for describing location in collection
    /// </summary>
    public class Collection
    {
        /// <summary>
        /// Number of collection
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Number of sample in collection
        /// </summary>
        public int SampleNumber { get; set; }
    }
}
