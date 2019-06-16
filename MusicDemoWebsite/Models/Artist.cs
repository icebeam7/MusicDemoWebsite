using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicDemoWebsite.Models
{
    [Table("ArtistTable")]
    public class Artist
    { 
        [Key]
        public Guid ArtistID { get; set; }

        public string ArtistName { get; set; }

        public List<Song> Songs { get; set; }
    }
}
