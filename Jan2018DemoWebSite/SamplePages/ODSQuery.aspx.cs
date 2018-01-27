
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#region additional name spaces
using Chinook.Data.POCOs;
#endregion


namespace Jan2018DemoWebSite.SamplePages
{
    public partial class ODSQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //first, we wish to access the specific row that was selected by pressing the view link which is the select command button
            //of the gridview.
            //Remember the View Link is a command Button 
            GridViewRow agrow = AlbumList.Rows[AlbumList.SelectedIndex];

            //access the data from gthe gridview templatecontrol
            //use the .FindControl("IdControlName") to access the desired control
            string albumid = (agrow.FindControl("AlbumId") as Label).Text;
            //Send the extracted value to another specified page
            //pagename?parameters&...
            // ? paramters set following 
            // Paramter set idlabel=value
            //& seperates multiple paramter sets
            Response.Redirect("AlbumDetails.aspx?aid=" + albumid);
            
        }

        protected void CountAlbums_Click(object sender, EventArgs e)
        {
            //Traverse a Gridview display
            //the only records available to us at this time out of the dataset assigned to the gridview 
            // are the rows beign displayed

            //Create a list<t> ot hold the counds of the display 
            List<ArtistAlbumCounts> Artists = new List<ArtistAlbumCounts>();


            //reusable pointer to an instance of the specified class 
            ArtistAlbumCounts item = null;
            int artistid = 0;

            //set up the loop to traverse the gridview 
            foreach (GridViewRow line in AlbumList.Rows)
            {
                //access the artistid 
                artistid = int.Parse((line.FindControl("ArtistList") as DropDownList).SelectedValue);

                //determine if you have already created a count instance in the list<T> for this artist
                // if not , create a  new instance for the artist and set its count to 1
                // if found incremete the counter (+1)

                //search for the artist in List<T>
                //what will be returned is either null (not found)
                // or the instance in the list<T>
                item = Artists.Find(x => x.ArtistId == artistid);

                if(item == null)
                {
                    //create instance, initialize add to list<t>
                    item = new ArtistAlbumCounts();
                    item.ArtistId = artistid;
                    item.AlbumCount = 1;
                    Artists.Add(item);
                }
                else
                {
                    item.AlbumCount++;
                }
            }
            //attach the list<T> (Collection) to the display control
            ArtistAlbumCountList.DataSource = Artists;
            ArtistAlbumCountList.DataBind();
        }
    }
}