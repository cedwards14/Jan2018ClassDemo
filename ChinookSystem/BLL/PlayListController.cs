using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region additionalnamespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;
using Chinook.Data.DTO;
using Chinook.Data.POCOs;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class PlayListController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]

        public List<ClientPlaylist> Playlist_ClientPlaylist(int trackcountlimit) //list here
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Playlists //add context
                              where x.PlaylistTracks.Count() > trackcountlimit
                              select new ClientPlaylist
                              {
                                  playlist = x.Name,
                                  tracklist = (from y in x.PlaylistTracks //bring in poco using
                                               select new TracksAndGenre
                                               {
                                                   tracks = y.Track.Name,
                                                   genre = y.Track.Genre.Name
                                               }).ToList() 
                              };
                return results.ToList(); //return some sweet results
            }
        }
    }
}
