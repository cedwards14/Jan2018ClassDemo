using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using Chinook.Data.POCOs;
#endregion

namespace Jan2018DemoWebsite.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

       

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() => 
            {

                TracksBy.Text = "Artist";

                SearchArgID.Text = ArtistDDL.SelectedValue;
                TracksSelectionList.DataBind();
             
            },"Tracks by Artist","Add an artist track to your playlist by clicking on the + ");


          
            //code to go here

        }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "MediaType";
            SearchArgID.Text = MediaTypeDDL.SelectedValue;
            TracksSelectionList.DataBind();
        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Genre";

            SearchArgID.Text = GenreDDL.SelectedValue;
            TracksSelectionList.DataBind();
        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Album";

            SearchArgID.Text = AlbumDDL.SelectedValue;
            TracksSelectionList.DataBind();
        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //code to go here
            if(string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Required Data", "playlist name is required to retrieve tracks");
            }
           else
            {
                string username = "HandyHansen";
                string playlistname = PlaylistName.Text;
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    List<UserPlaylistTrack> results = sysmgr.List_TracksForPlaylist(playlistname, username);
                    if(results.Count()==0)
                    {
                        MessageUserControl.ShowInfo("Check Playlist name");
                    }
                    PlayList.DataSource = results;
                    PlayList.DataBind();
                });

            }
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            if (PlayList.Rows.Count == 0)
            {
                MessageUserControl.ShowInfo("Warning", "You must have a playlist selected");
            }
            else
            {
                if (string.IsNullOrEmpty(PlaylistName.Text))
                {
                    MessageUserControl.ShowInfo("Warning", "You must have a playlist name. Please enter your playlist name");
                    //optionally you might wish to empty the playlist gridview
                }
                else
                {
                    //check that a single track has been checked
                    // if so, extract the needed data from the selected Gridviewrow
                    //the trackid is a hidden column on the GridViewrow
                    int trackid = 0;
                    int tracknumber = 0;
                    int rowselected = 0;
                    CheckBox playlistselection = null;
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        //access the control on the selected Gridviewrow 
                        //point to the checkbox 
                        playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        //is the checkbox on 
                        if (playlistselection.Checked)
                        {
                            rowselected++; //counter for number of checked items
                            //save necessary data for moving a track 
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                        }

                    }//eofor

                    //howmany tracks were checked
                    if (rowselected != 1)
                    {
                        MessageUserControl.ShowInfo("Warning", "Select ont track to move");
                    }
                    else
                    {
                        //is the selected track the first track?
                        if (tracknumber == PlayList.Rows.Count)
                        {
                            MessageUserControl.ShowInfo("Warning", "Cannot move last track down");
                        }
                        else
                        {
                            MoveTrack(trackid, tracknumber, "down");
                            //move the track. 
                        }
                    }


                }
            }
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            if(PlayList.Rows.Count==0)
            {
                MessageUserControl.ShowInfo("Warning", "You must have a playlist selected");
            }
            else
            {
                if(string.IsNullOrEmpty(PlaylistName.Text))
                    {
                    MessageUserControl.ShowInfo("Warning", "You must have a playlist name. Please enter your playlist name");
                    //optionally you might wish to empty the playlist gridview
                }
                    else
                    {
                    //check that a single track has been checked
                    // if so, extract the needed data from the selected Gridviewrow
                    //the trackid is a hidden column on the GridViewrow
                    int trackid = 0;
                    int tracknumber = 0;
                    int rowselected = 0;
                    CheckBox playlistselection = null;
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        //access the control on the selected Gridviewrow 
                        //point to the checkbox 
                        playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        //is the checkbox on 
                        if(playlistselection.Checked)
                        {
                            rowselected++; //counter for number of checked items
                            //save necessary data for moving a track 
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                        }
                 
                    }//eofor

                    //howmany tracks were checked
                    if(rowselected != 1)
                    {
                        MessageUserControl.ShowInfo("Warning", "Select ont track to move");
                    }
                    else
                    {
                        //is the selected track the first track?
                        if(tracknumber==1)
                        {
                            MessageUserControl.ShowInfo("Warning", "Cannot move track one up");
                        }
                        else
                        {
                            MoveTrack(trackid, tracknumber, "up");
                        }
                    }


                 }
            }
        
        }


        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
            MessageUserControl.TryRun(() =>
            {

                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack("HandyHansen", PlaylistName.Text, trackid, tracknumber, direction);

                List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text,"HandyHansen");
                PlayList.DataSource = info;
                PlayList.DataBind();

            },"Moved", "Track has been moved " + direction);


        }

    

        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
           if(string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Enter a PlayList name");

            }
            else
            {
                if(PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("PlayList has no tracks to delete");
                }
                else
                {
                    //gather all selected rows
                    List<int> trackstodelete = new List<int>();
                    int rowselected = 0;
                    CheckBox playlistselection = null;
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        if(playlistselection.Checked)
                        {
                            rowselected++;
                            trackstodelete.Add(int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text));
                        }
                    }

                    //was at least one track selected

                if(rowselected == 0)
                    {
                        MessageUserControl.ShowInfo("Warning","you must select atleast one track to delete");
                    }
                else
                    {
                        //send the list of tracks to the the BLL to delete
                        MessageUserControl.TryRun(()=> 
                        {
                            PlaylistTracksController sysmgr = new PlaylistTracksController();
                            sysmgr.DeleteTracks("HandyHansen",PlaylistName.Text,trackstodelete);
                            List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, "HandyHansen");
                            PlayList.DataSource = info;
                            PlayList.DataBind();
                        },"Removed","Tracks have been removed");
                    }
                }
            }
        }

        protected void TracksSelectionList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //this method will only exceute if the user has pressed the plus sign on a visible row from the display

            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("PlayList Name", "You must supply a playlist name");
            }
            else
            {
                //via security one can obtain the user name
                string username = "HandyHansen";
                string playlistname = PlaylistName.Text;

                //the trackid is attched to each listview row
                //via the commandargument parameter
                //access to the trackid is done via the ListViewCommandEventArgs e parameter
                //the e paramterer is treated as an object
                //Some e parameters need to be cast as strings 

                int trackid = int.Parse(e.CommandArgument.ToString());

                //allrequired data can now be sent to the BLL for further processing 
                //user friendly  error handling

                MessageUserControl.TryRun(()=>
                {

                    //connect to your BLL
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    sysmgr.Add_TrackToPLaylist(playlistname, username, trackid);
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(playlistname, username);
                    PlayList.DataSource = info;
                    PlayList.DataBind();
                },"Track Added", "The track has been added , check your list below");
            }
        }

    }
}