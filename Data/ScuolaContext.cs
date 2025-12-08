
using Microsoft.EntityFrameworkCore;
using ScuolaMVC.Models;

namespace ScuolaMVC.Data
{
    public class ScuolaContext : DbContext
    {
        public DbSet<Studente> Studenti { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ScuolaDBrepo;Trusted_Connection=True;");
        }
    }
}
