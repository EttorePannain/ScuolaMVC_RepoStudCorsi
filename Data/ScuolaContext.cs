
using Microsoft.EntityFrameworkCore;
using ScuolaMVC_RepoStudCorsi.Models;

namespace ScuolaMVC_RepoStudCorsi.Data
{
    public class ScuolaContext : DbContext
    {
        public DbSet<Studente> Studenti { get; set; }
        //
        public DbSet<Corso> Corsi { get; set; }
        public DbSet<StudenteCorso> StudentiCorsi { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StudenteCorso>()
                .HasKey(sc => new { sc.StudenteId, sc.CorsoId });

            modelBuilder.Entity<StudenteCorso>()
                .HasOne(sc => sc.Studente)
                .WithMany(s => s.Corsi)
                .HasForeignKey(sc => sc.StudenteId);

            modelBuilder.Entity<StudenteCorso>()
                .HasOne(sc => sc.Corso)
                .WithMany(c => c.Studenti)
                .HasForeignKey(sc => sc.CorsoId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ScuolaDBrepoStudCorsi;Trusted_Connection=True;");
        }
    }
}
