using System.Collections.Generic;
using Simple.Data;

namespace MolluskRecognition.DAL.DataModels
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
