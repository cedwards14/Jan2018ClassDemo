using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region additionalnamespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion
namespace ChinookSystem.BLL
{
    [DataObject]
   public class ArtistController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<Artist> Artists_List()
        {
            //Create a transaction instance of your context class
            using (var context = new ChinookContext())
            {
                return context.Artists.OrderBy(x => x.Name).ToList();
             }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Artist Artists_Get(int artistid)
        {
            //Create a transaction instance of your context class
            using (var context = new ChinookContext())
            {
                return context.Artists.Find(artistid);
            }
        }

    }
}
