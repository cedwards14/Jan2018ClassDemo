using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region additionalnamespaces
using Chinook.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Albums_List()
        {
            //Create a transaction instance of your context class
            using (var context = new ChinookContext())
            {
                return context.Albums.OrderBy(x => x.Title).ToList();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Album Album_Get(int albumid)
        {
            //Create a transaction instance of your context class
            using (var context = new ChinookContext())
            {
                return context.Albums.Find(albumid);
            }
        }


    }
}
