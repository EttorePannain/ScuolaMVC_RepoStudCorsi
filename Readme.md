# Flusso No Repository
Program --> MenuView.Show() 
        --> studView.Menu()
        --> Create()
        --> Studente.controller.Create(nome, cognome)
            - db.Studenti.Add(studente)
            - db.SaveChanges

** Il controller conosce troppi dettagli del database.

# QUALI MODIFICHE DOVREMMO FARE?

**  aggiungere campi
se in modello esistente (esempio Studente.Eta)
PM> Add-Migration AggiungiEta
Update-Database

**# aggiungere modelli
1. Model → crea classe con Id
2. DbContext → aggiungi DbSet
3. Migration → Add-Migration
4. Database → Update-Database
5. (Opzionale) Aggiorna repository (scrivere codice)
6. (Opzionale) Aggiorna controller / console app (scrivere codice)

2) aggiungere DbSet nel DBContext: 
public DbSet<Docente> Docenti {get;set;} public DbSet<Corso> Corsi {get;set;}
3) Add-Migration AggiungiCorso
4) Update-Database

PM> Add-Migration Add_Modelli_Docente_e_Corso
Update-Database

# Cosa faremo invece: 
** PASSAGGIO AD ALTRO PROGETTO CON REPOSITORY (codice pulito)

# Step 1 
Passaggio da ScuolaMVC a ScuolaMVCRepo

# Step 2
Modificare Modello Studente (aggiungere campi)
Aggiungere  Modelli (Docente, Corsi)




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


# DA:
######### #########################
Program → MenuView → StudenteView → StudentiController → EF → SQL Server

#  A:

Program → MenuView → StudenteView → Controller → Repository → EF → SQL Server

########## #########################