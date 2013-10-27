using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Data;

namespace MolluskRecognition.DataModels
{
    public class DbFacade
    {
        public List<Family> GetFamilies()
        {
            var db = Database.Open();
            return db.Family.All();
        }
    }
}
