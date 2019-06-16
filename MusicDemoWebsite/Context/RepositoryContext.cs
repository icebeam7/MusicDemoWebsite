using Microsoft.EntityFrameworkCore;
using MusicDemoWebsite.Models;

namespace MusicDemoWebsite.Context
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
    }
}
