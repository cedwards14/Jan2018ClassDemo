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
           
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            //code to go here
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
        }

        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
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
                    //code to retrieve the up to date playlist and tracks
                    //for refreshing the playlist track list
                },"Track Added", "The track has been added , check your list below");
            }
        }

    }
}