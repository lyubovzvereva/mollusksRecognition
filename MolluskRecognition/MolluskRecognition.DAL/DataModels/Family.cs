using System;
using System.ComponentModel.DataAnnotations;

namespace MolluskRecognition.DAL.DataModels
{
    public class Family : Entity
    {
        public Family() { }
        public string Name { get; set; }
    }
}
