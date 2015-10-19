using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolluskRecognition.DAL.DataModels
{
    public class SculptureType : Entity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
