
using HESound.Models;
using Microsoft.EntityFrameworkCore;

namespace HESound.Data
{
    public class HESoundContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Sound> Sounds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=hesound.db");
    }
}
