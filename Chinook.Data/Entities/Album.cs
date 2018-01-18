using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region additional name spaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace Chinook.Data.Entities
{
    [Table("Albums")]
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        [StringLength(160,ErrorMessage = "The title's max length is 160 characters")]
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public int ReleaseYear { get; set; }
        [StringLength(50, ErrorMessage = "The ReleaseLabels's max length is 50 characters")]
        public string ReleaseLabel { get; set; }



    }
}
