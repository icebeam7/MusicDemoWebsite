using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicDemoWebsite.Models
{
    [Table("SongTable")]
    public class Song
    {
        [Key]
        public Guid SongID { get; set; }

        public string SongName { get; set; }

        public Guid ArtistID { get; set; }
        public Artist Artist { get; set; }
    }
}
