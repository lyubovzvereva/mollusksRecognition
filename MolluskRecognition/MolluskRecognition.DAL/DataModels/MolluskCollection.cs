using System.ComponentModel.DataAnnotations;

namespace MolluskRecognition.DAL.DataModels
{
    /// <summary>
    /// Class for describing location in collection
    /// </summary>
    public class MolluskCollection
    {

        [Key]
        public int Id { get; set; }

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
