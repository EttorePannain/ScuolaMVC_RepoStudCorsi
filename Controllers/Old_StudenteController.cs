using System.Collections.Generic;
using System.Linq;
using ScuolaMVC.Models;
using ScuolaMVC.Data;

namespace ScuolaMVC.Controllers // Corretto il namespace per evitare conflitti tra controller e modelli
{
    public class Old_StudenteController
    {
        public List<Studente> GetAll()
        {
            using var db = new ScuolaContext();
            return db.Studenti.ToList();
        }

        public Studente GetById(int id)
        {
            using var db = new ScuolaContext();
            return db.Studenti.Find(id);
        }

        public Studente Create(Studente s)
        {
            using var db = new ScuolaContext();
          
            db.Studenti.Add(s);
            db.SaveChanges();
            return s;
        }
        public Studente Create(string nome, string cognome)
        {
            using var db = new ScuolaContext();
            Studente s = new Studente { Nome = nome, Cognome = cognome };
            db.Studenti.Add(s);
            db.SaveChanges();
            return s;
        }

        public bool Update(int id, string nome, string cognome)
        {
            using var db = new ScuolaContext();
            var s = db.Studenti.Find(id);
            if (s == null) return false;
            s.Nome = nome;
            s.Cognome = cognome;
            db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            using var db = new ScuolaContext();
            var s = db.Studenti.Find(id);
            if (s == null) return false;
            db.Studenti.Remove(s);
            db.SaveChanges();
            return true;
        }
    }
}