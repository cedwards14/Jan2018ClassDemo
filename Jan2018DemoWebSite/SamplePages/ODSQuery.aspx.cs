using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    }
}