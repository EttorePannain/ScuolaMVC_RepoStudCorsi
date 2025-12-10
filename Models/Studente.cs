using System.Collections.Generic;


namespace ScuolaMVC_RepoStudCorsi.Models;

public class Studente {
    public int Id {get;set;}
    public string Nome {get;set;}
    public string Cognome {get;set;}
    public int Eta {get;set; }
    public List<StudenteCorso> Corsi { get; set; } = new();
}