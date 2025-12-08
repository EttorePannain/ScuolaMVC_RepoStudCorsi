
using ScuolaMVC.Models;

public class StudenteController
{
    private readonly IStudenteRepository  _repo;

    public StudenteController(IStudenteRepository repo)
    {
        _repo = repo;
    }

    public List<Studente> GetAll() => _repo.GetAll();


    public void Create(Studente s)
    {
        _repo.Add(s);
        _repo.Save();
    }
    // metodo di aggiornamento con parametri singoli (rimpiazzato)
    public void Update(int id, string nome, int eta)
    {
        var s = _repo.GetById(id);
        if (s == null) return;
        s.Nome = nome;
        s.Eta = eta;
        _repo.Update(s);
        _repo.Save();
    }
    // metodo di aggiornamento con oggetto studente
    public void Update(Studente sUpd)
    {
         var s = _repo.GetById(sUpd.Id);
        if (s == null) return;
        s.Nome = sUpd.Nome;
        s.Eta = sUpd.Eta;
        s.Cognome = sUpd.Cognome;
        _repo.Update(s);
        _repo.Save();
    }

    public void Delete(Studente sDel)
    {
        var s = _repo.GetById(sDel.Id);
        if (s == null) return;
        _repo.Delete(s);
        _repo.Save();
    }

    public void Delete(int id)
    {
        var s = _repo.GetById(id);
        if (s == null) return;
        _repo.Delete(s);
        _repo.Save();
    }
}
