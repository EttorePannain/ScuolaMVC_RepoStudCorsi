
# Questa release contiene:

# Cosa abbiamo fatto: 
** PASSAGGIO AD ALTRO PROGETTO CON REPOSITORY (codice pulito)

# Step 1 
Passaggio da ScuolaMVC_noREPO a ScuolaMVC_Repo
# Step 2
Modificato Modello Studente (aggiungere campi)
# Step 3
Satbilizzato architettura Console MVC + Repository + EF SQL Server
#Step 4
Modificato DbContext per nuovo modello Studente
# Step 5
Aggiunto Repository per Studente
# Step 6
Modificato Controller per Studente
# Step 7
Modificato View per Studente
# Step 8
Passaggio tra componente View - Controller - Repository (da parametri ad oggetti Studente)    


# DA:
######### #########################
Program → MenuView → StudenteView → StudentiController → EF → SQL Server

#  A:

Program → MenuView → StudenteView → Controller → Repository → EF → SQL Server

########## #########################



# Implementazione -- Creare un nuovo progetto ScuolaMVCRepo_StudCorsi
**
Ora che l’architettura Console MVC + Repository + EF SQL Server è finalmente stabile, possiamo aggiungere:

Un nuovo modello Corso con relazione a Studente con:
- CRUD completo per Corso
- Funzione: Iscrivi uno Studente a un Corso
- Modifiche nei Repository
- Modifiche nei Controller
- Modifiche nelle Views console
- Nuova Migration per aggiornare il DB ScuolaDBrepo

Il tutto mantenendo l' applicazione MVC console.

*/
#
namespace ScuolaMVC.Models
{
    public class Corso
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public List<StudenteCorso> Studenti { get; set; } = new();
    }
}

# STEP 1 — Il nuovo MODELLO Corso
✔ Un Corso ha molti Studenti
✔ Uno Studente può essere iscritto a più Corsi

→ Relazione N:N

Useremo una tabella ponte: StudenteCorso

#
namespace ScuolaMVC.Models
{
    public class StudenteCorso
    {
        public int StudenteId { get; set; }
        public Studente Studente { get; set; }
        public int CorsoId { get; set; }
        public Corso Corso { get; set; }
    }
}

# STEP 2 — Aggiornamento del DbContext
Data/ScuolaContext.cs

Aggiungere:

public DbSet<Corso> Corsi { get; set; }
public DbSet<StudenteCorso> StudenteCorsi { get; set; }


E configurare la relazione N:N:

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
E bisognerà aggiungere in Studente.cs:

public List<StudenteCorso> Corsi { get; set; } = new();



Step 2.1 — Creare la Migration su nuovo database ScuolaDB_Stud_Corsi
per preservare vecchio DB creando un nuovo database per i studenti/corsi:

nello ScuolaContext:
options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ScuolaDB_Stud_Corsi;Trusted_Connection=True;");


PM> Add-Migration AggiungiModelloCorso
Update-Database




# STEP 3 — Repository per corso

... Come per Studente, creiamo l’interfaccia e la classe concreta per Corso.

Repositories/ICorsoRepository.cs
Repositories/CorsoRepository.cs

ICorsoRepository.cs

using ScuolaMVC.Models;

public interface ICorsoRepository : IRepository<Corso>
{
    void IscriviStudente(int corsoId, int studenteId);
}

CorsoRepository.cs

using ScuolaMVC.Data;
using ScuolaMVC.Models;

public class CorsoRepository : Repository<Corso>, ICorsoRepository
{
    private readonly ScuolaContext _ctx;

    public CorsoRepository(ScuolaContext ctx) : base(ctx)
    {
        _ctx = ctx;
    }

    public void IscriviStudente(int corsoId, int studenteId)
    {
        var iscrizione = new StudenteCorso
        {
            CorsoId = corsoId,
            StudenteId = studenteId
        };

        _ctx.StudenteCorsi.Add(iscrizione);
        _ctx.SaveChanges();
    }
}

# STEP 4 — Controller Corso

Creiamo un controller simile a StudentiController:

Controllers/CorsiController.cs


Contenuto:

using ScuolaMVC.Models;

public class CorsiController
{
    private readonly ICorsoRepository _repo;

    public CorsiController(ICorsoRepository repo)
    {
        _repo = repo;
    }

    public List<Corso> GetAll() => _repo.GetAll();

    public void Create(string nome)
    {
        _repo.Add(new Corso { Nome = nome });
        _repo.Save();
    }

    public void Update(int id, string nome)
    {
        var c = _repo.GetById(id);
        if (c == null) return;
        c.Nome = nome;
        _repo.Update(c);
        _repo.Save();
    }

    public void Delete(int id)
    {
        var c = _repo.GetById(id);
        if (c == null) return;
        _repo.Delete(c);
        _repo.Save();
    }

    public void IscriviStudente(int corsoId, int studenteId)
    {
        _repo.IscriviStudente(corsoId, studenteId);
    }
}

# STEP 5 — View Console per Corsi

Nuovo file:

Views/CorsoView.cs

using System;

public class CorsoView
{
    public void Menu()
    {
        var repo = new CorsoRepository(new ScuolaContext());
        var studRepo = new StudenteRepository(new ScuolaContext());
        var controller = new CorsiController(repo);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("GESTIONE CORSI");
            Console.WriteLine("1. Inserisci corso");
            Console.WriteLine("2. Elenco corsi");
            Console.WriteLine("3. Modifica corso");
            Console.WriteLine("4. Elimina corso");
            Console.WriteLine("5. Iscrivi studente a corso");
            Console.WriteLine("0. Torna al menu principale");
            Console.Write("Scelta: ");

            switch(Console.ReadLine())
            {
                case "1":
                    Console.Write("Nome corso: ");
                    controller.Create(Console.ReadLine());
                    break;

                case "2":
                    foreach (var c in controller.GetAll())
                        Console.WriteLine($"{c.Id} - {c.Nome}");
                    Console.ReadKey();
                    break;

                case "3":
                    Console.Write("ID: ");
                    int idm = int.Parse(Console.ReadLine());
                    Console.Write("Nuovo nome: ");
                    controller.Update(idm, Console.ReadLine());
                    break;

                case "4":
                    Console.Write("ID da eliminare: ");
                    controller.Delete(int.Parse(Console.ReadLine()));
                    break;

                case "5":
                    Console.Write("ID corso: ");
                    int corsoId = int.Parse(Console.ReadLine());
                    Console.Write("ID studente: ");
                    int studId = int.Parse(Console.ReadLine());
                    controller.IscriviStudente(corsoId, studId);
                    break;

                case "0": return;
            }
        }
    }
}

# STEP 6 — Aggiornare il Menu principale

In MenuView aggiungere:

case "2": new CorsoView().Menu(); break;


E aggiornare il menu testuale

contenuto  di MenuView.cs


Console.WriteLine("MENU PRINCIPALE");
Console.WriteLine("1. Gestione Studenti");
Console.WriteLine("2. Gestione Corsi");
Console.WriteLine("0. Esci");
Console.Write("Scelta: ");

switch(Console.ReadLine()) {
    case "1": studView.Menu(); break;
    case "2": new CorsoView().Menu(); break;
    case "0": return;
}



########### #########################
# Entity Framework Core - Code First Migrations
########### #########################

# Creare una migration
Add-Migration NomeMigration

# Applicare le migration al database
Update-Database

# Tornare a una migration prece#dente
Update-Database NomeMigration

# Eliminare l’ultima migration (solo file, non DB)
Remove-Migration

# drop tabelle database EF
Drop-Database
Add-Migration Iniziale

# aggiungere campi
se in modello esistente (esempio Studente.Eta)
PM> Add-Migration AggiungiEta
Update-Database

# aggiungere modelli
se in modello esistente (esempio Studente.Eta)
PM> Add-Migration AggiungiEta
Update-Database

########## #########################
# Entity Framework Core - Code First Migrations
########## #########################

# Documentazione ufficiale:
https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
# Guida introduttiva:
https://learn.microsoft.com/en-us/ef/core/get-started/?tabs=netcore-cli
# Comandi principali:
# Creare una migration
Add-Migration NomeMigration
# Applicare le migration al database
Update-Database
# Tornare a una migration precedente
Update-Database NomeMigration
# Eliminare l’ultima migration (solo file, non DB)
Remove-Migration
# drop tabelle database EF
Drop-Database
Add-Migration Iniziale
# Esempi:
# aggiungere campi
se in modello esistente (esempio Studente.Eta)
PM> Add-Migration AggiungiEta
Update-Database
# aggiungere modelli
se in modello esistente (esempio Studente.Eta)
PM> Add-Migration AggiungiEta
Update-Database