using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region additionalnamespaces

using ChinookSystem.DAL;
using System.ComponentModel;
using Chinook.Data.Entities;
using Chinook.Data.POCOs;

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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Tracks_GetByAlbumID(int albumid)
        {
            //Create a transaction instance of your context class
            using (var context = new ChinookContext())
            {
                return context.Tracks.Where(x => x.AlbumId == albumid).Select(x => x).ToList();
            }
        }




      
            [DataObjectMethod(DataObjectMethodType.Select, false)]
            public List<TrackList> List_TracksForPlaylistSelection(string tracksby, int argid)
            {
                using (var context = new ChinookContext())
                {
                    List<TrackList> results = null;

                    //code to go here

                    return results;
                }
            }//eom


        
    }
}
