﻿using System;
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
            public List<TrackList> List_TracksForPlaylistSelection(string passedparameter, int argid)
            {
                using (var context = new ChinookContext())
                {


                var results = from x in context.Tracks
                              where passedparameter.Equals("Artist") ? x.Album.ArtistId == argid :
                                      passedparameter.Equals("MediaType") ? x.MediaTypeId == argid :
                                      passedparameter.Equals("Genre") ? x.Genre.GenreId == argid :
                                      x.AlbumId == argid
                              orderby x.Name
                              select new TrackList
                              {
                                  TrackID = x.TrackId,
                                  Name = x.Name,
                                  Title = x.Album.Title,
                                  MediaName = x.MediaType.Name,
                                  GenreName = x.Genre.Name,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };

                return results.ToList();
                }
            }//eom

        //this method will create a flat POCO dataset
        //to use in reporting

            [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<GenreAlbumReport> GenreAlbumReport_Get()
        {
            using (var context = new ChinookContext())
            {
                //there is no need to sort this dataset
                //the final sort will be don in the report
                var results = from x in context.Tracks
                              select new GenreAlbumReport
                              {
                                  GenreName = x.Genre.Name,
                                  AlbumTitle = x.Album.Title,
                                  TrackName = x.Name,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice

                              };
                return results.ToList();
}
        }
        
    }
}
