
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional name spaces
using Chinook.Data.POCOs;
#endregion


namespace Chinook.Data.DTO
{
    public class ClientPlaylist
    {
        public string playlist { get; set; }
        public List<TracksAndGenre> tracklist { get; set; }
    }
}
