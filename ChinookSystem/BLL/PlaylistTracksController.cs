﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using Chinook.Data.DTO;
using Chinook.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
using DMIT2018Common.UserControls;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {

                //What would happen if there is no match for the incoming perameter values

                //validate that a playlist actually exists 

                var results = (from x in context.Playlists where x.UserName.Equals(username) && x.Name.Equals(playlistname)select x).FirstOrDefault();

                if(results ==null)
                {
                    return null;
                }
                else
                {
                    var theTracks = from x in context.PlaylistTracks
                                    where x.PlaylistId.Equals(results.PlaylistId)
                                    orderby x.TrackNumber
                                    select new UserPlaylistTrack
                                    {
                                        TrackID = x.TrackId,
                                        TrackNumber = x.TrackNumber,
                                        TrackName = x.Track.Name,
                                        Milliseconds = x.Track.Milliseconds,
                                        UnitPrice = x.Track.UnitPrice
                                    
                                    };
                    return theTracks.ToList();
                }
              
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            List<string> reasons = new List<string>();
            //this list of strings will be used with the BusinessRuleException 
            using (var context = new ChinookContext())
            {
                //Part one
                //optional add of the new playlist 
                //Validate track is NOT on the existing playlist
                //determine if the playlist already exists on the database
                Playlist exists = context.Playlists.Where(x => x.Name.Equals(playlistname,StringComparison.OrdinalIgnoreCase) && x.UserName.Equals(username,StringComparison.OrdinalIgnoreCase)).Select(x => x).FirstOrDefault();
                PlaylistTrack newTrack = null;
                int trackNumber = 0;

                if(exists == null)
                {
                    //add the parent record (playlist record)
                    //no tracks exist yet for the new playlist
                    // therefore the tracknumber is 1
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    exists = context.Playlists.Add(exists);
                    trackNumber = 1;
                 
                }
                else
                {

                    //the playlist exists on the database
                    //the playlist may not have any tracks
                    //adjust the tracknumber to be the next track

                    trackNumber = exists.PlaylistTracks.Count() +1;
                    // will this be a duplicate track???
                   // Lookup up the current tracks of the playlist
                   // testing for the incoming trackid
                    newTrack = exists.PlaylistTracks.SingleOrDefault(x => x.TrackId == trackid);
                    //validation rule: track may only exist once on the playlist
                    if(newTrack != null)
                    {
                        // rule is violated 
                        //since track already exists on playlist
                        //throw an exception to stop OLTP processing
                        //this example will demonstrate using the BusinessRuleException 

                        reasons.Add("Track already exists on the playlist");
                    }
                }


                //Part two 


                //check if any errors were found

                if (reasons.Count() != 0)
                {
                    //issue a businessrule exception
                    // A BusinessRule exception is an object that has been designed to hold
                    //multiple errors
                    throw new BusinessRuleException("Adding track to playlist", reasons);
                    
                }
                else
                {
                    //Add the track to the playlist Tracks
                    newTrack = new PlaylistTrack();
                    newTrack.TrackNumber = trackNumber;
                    newTrack.TrackId = trackid;

                    //what about the foreign key to the PlayList?
                    //the parent entity has been set up with a HashSet
                    //therefore, if you add a child via the 
                    //navigational property the hashset
                    // will take care of filling the foreign key
                    //with the appropriate pkey value 
                    //during .savechanges() during the commit
                    //savechanges handles all the foreign keys


                    //add the new track to the playlist using the navigational property
                    exists.PlaylistTracks.Add(newTrack);

                    //physically place the record(s) on the database
                    //ANd commit the transaction that is the using 
                    // with .SaveChanges
                    context.SaveChanges();
                }
            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //code to go here 
                var exists = (from x in context.Playlists
                              where x.Name.Equals(playlistname)
                                       && x.UserName.Equals(username)
                              select x).FirstOrDefault();

                if(exists == null)
                {
                    throw new Exception("PLaylist has been removed from the files");
                }
                else
                {
                    PlaylistTrack moveTrack = (from x in exists.PlaylistTracks
                                               where x.TrackId == trackid
                                               select x).FirstOrDefault();

                    if(moveTrack == null)
                    {
                        throw new Exception("PLay list track has been removed from the files");
                    }
                    else
                    {
                        //Create an instance pointer to be used to point to the other track involved in the move.

                        PlaylistTrack otherTrack = null;
                        //direction
                        if(direction.Equals("up"))
                        {

                            //up
                            //recheck that the track is not the first track
                            //If so, throw an error, otherwise move the track
                            if(moveTrack.TrackNumber==1)
                            {

                                throw new Exception("Pleaylist track already at top");

                            }
                            else
                            {
                                otherTrack = (from x in exists.PlaylistTracks
                                              where x.TrackNumber == moveTrack.TrackNumber -1
                                              select x).FirstOrDefault();
                                if(otherTrack==null)
                                {
                                    throw new Exception("Switching track was not found");
                                }
                                else
                                {
                                    moveTrack.TrackNumber -= 1;
                                    otherTrack.TrackNumber += 1;
                                }
                            }

                        }

                        else
                        {
                            if (moveTrack.TrackNumber == exists.PlaylistTracks.Count())
                            {
                                throw new Exception("Pleaylist track already below");
                            }
                            else
                            {
                                otherTrack = (from x in exists.PlaylistTracks
                                              where x.TrackNumber == moveTrack.TrackNumber + 1
                                              select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    throw new Exception("Switching track was not found");
                                }
                                else
                                {
                                    moveTrack.TrackNumber += 1;
                                    otherTrack.TrackNumber -= 1;
                                }
                            }
                        }//end of up and down
                        //save the changes to the data
                        //we are saving 2 different entities
                        //indicate the property to save for a particular entity instance
                        context.Entry(moveTrack).Property(y => y.TrackNumber).IsModified = true;
                        context.Entry(otherTrack).Property(y => y.TrackNumber).IsModified = true;
                        //commit your changes
                        context.SaveChanges();
                       

                    }
                }
            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
                var exists = (from x in context.Playlists
                              where x.Name.Equals(playlistname)
                                       && x.UserName.Equals(username)
                              select x).FirstOrDefault();

                if (exists == null)
                {
                    throw new Exception("PLaylist has been removed from the files");
                }
                else
                {
                    //get a list of tracks that will be kept in order of tracknumber
                    //you do not know if the physical order is the same as the logical TrackNumber order

                    //.Any() allows you to search for an item in a list  using a condition, returns true if found
                    //looking for an item in lsitA is inside listB
                    //in this example we DO NOT want to find it thus !
                    var tracksKept = exists.PlaylistTracks.Where(tr => !trackstodelete.Any(tod => tod == tr.TrackId)).OrderBy(tr=> tr.TrackNumber).Select(tr =>tr);


                    //delete tracks
                    PlaylistTrack item = null;
                    foreach(var deletetrackid in trackstodelete)
                    {
                        item = exists.PlaylistTracks.Where(tr => tr.TrackId == deletetrackid).FirstOrDefault();

                        if (item != null)
                        {
                            exists.PlaylistTracks.Remove(item);
                        }
                    

                    }


                    //renumber the remaining tracks(Tracks that were kept)

                    int number = 1;
                    foreach (var tkept in tracksKept)
                    {
                        tkept.TrackNumber = number;
                        number++;
                        context.Entry(tkept).Property(y => y.TrackNumber).IsModified = true;

                    }
                    //commit work
                    context.SaveChanges();


                }


            }
        }//eom
    }
}
