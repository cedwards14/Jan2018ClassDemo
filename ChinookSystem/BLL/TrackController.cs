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
    public class TrackController
    {

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Tracks_List()
        {
            //Create a transaction instance of your context class
            using (var context = new ChinookContext())
            {
                return context.Tracks.OrderBy(x => x.Name).ToList();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Track Tracks_Get(int trackid)
        {
            //Create a transaction instance of your context class
            using (var context = new ChinookContext())
            {
                return context.Tracks.Find(trackid);
            }
        }

    }
}
