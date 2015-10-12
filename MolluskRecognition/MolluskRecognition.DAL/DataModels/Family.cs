using System;
using System.ComponentModel.DataAnnotations;

namespace MolluskRecognition.DAL.DataModels
{
    public class Family
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
