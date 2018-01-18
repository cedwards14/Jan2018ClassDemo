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
    [Table("Tracks")]
    public class Track
    {
        [Key]
        public int TrackId { get; set; }
       [StringLength(200, ErrorMessage ="The name has a max of 200 characters")]
       public string name { get; set; }
        public int? AlbumId { get; set; }
        public int MediatypeId { get; set; }
        public int? GenreId { get; set; }
        [StringLength(220, ErrorMessage = "The Composer has a max of 200 characters")]
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        public decimal UnitPrice { get; set; }


        //virtual nav
        public virtual Album Album { get; set; }
    }
}
