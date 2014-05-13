using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolluskRecognition.DataModels
{
    /// <summary>
    /// Species(Вид)
    /// </summary>
    public class Species
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Species()
        {
            Year = DateTime.Today;
            Locations = new List<Location>();
        }

        /// <summary>
        /// Name of the species
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Author of the species
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Species discovery year
        /// </summary>
        public DateTime Year { get; set; }

        /// <summary>
        /// Age of the species
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// Synonymy description of the species
        /// </summary>
        public string Synonymy { get; set; }

        /// <summary>
        /// Shell description
        /// </summary>
        public string Shell { get; set; }

        /// <summary>
        /// Description of the initial shell type
        /// </summary>
        public string InitialShellTypeDescription { get; set; }

        /// <summary>
        /// Description of the umbones (макушка)
        /// </summary>
        public string Umbones { get; set; }

        /// <summary>
        /// Description of the dorsal margin (замочный край)
        /// </summary>
        public string DorsalMargin { get; set; }

        /// <summary>
        /// Description of the ventral margin (брюшной край)
        /// </summary>
        public string VentralMargin { get; set; }

        /// <summary>
        /// Description of the posterior end (задний конец)
        /// </summary>
        public string PosteriorEnd { get; set; }

        /// <summary>
        /// Description of the front end (передний конец)
        /// </summary>
        public string FrontEnd { get; set; }

        /// <summary>
        /// Description of the sculpture
        /// </summary>
        public string Sculpture { get; set; }

        /// <summary>
        /// Description of comparison
        /// </summary>
        public string Comparison { get; set; }

        /// <summary>
        /// Type of the initial shell
        /// </summary>
        public ShellType InitialShellType { get; set; }

        /// <summary>
        /// Type of the sculpture
        /// </summary>
        public SculptureType SculptureType { get; set; }

        /// <summary>
        /// List of locations
        /// </summary>
        public List<Location> Locations { get; set; }

        /// <summary>
        /// List of sections (срезы)
        /// </summary>
        public List<Section> Sections { get; set; }

        /// <summary>
        /// Mollusk samples
        /// </summary>
        public List<Sample> Samples { get; set; }

        
    }
    /// <summary>
    /// Types of initial shell
    /// </summary>
    public enum ShellType
    {
        A1,
        A2,
        B1,
        B2,
        /// <summary>
        /// Г1
        /// </summary>
        G1,

        /// <summary>
        /// Г2
        /// </summary>
        G2,

        /// <summary>
        /// Г3
        /// </summary>
        G3
    }

    /// <summary>
    /// Sculpture type
    /// </summary>
    public enum SculptureType
    {
        /// <summary>
        /// Radial type
        /// </summary>
        Radial,

        /// <summary>
        /// Concentric type
        /// </summary>
        Concentric,

        /// <summary>
        /// Radial or concentric type
        /// </summary>
        RadialOrConcentric
    }
}
