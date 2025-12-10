
using System;
//using ScuolaMVC_RepoStudCorsi.Controllers;
using ScuolaMVC_RepoStudCorsi.Data;
using ScuolaMVC_RepoStudCorsi.Models;

public class StudenteView
{
    public void Menu()
    {
        var repo = new StudenteRepository(new ScuolaContext());
        var controller = new StudenteController(repo);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("GESTIONE STUDENTI");
            Console.WriteLine("1. Inserisci");
            Console.WriteLine("2. Elenco");
            Console.WriteLine("3. Modifica");
            Console.WriteLine("4. Elimina");
            Console.WriteLine("0. Torna al menu principale");
            Console.Write("Scelta: ");

            switch(Console.ReadLine())
            {
                case "1":
                    Console.Write("Nome: ");
                    var n = Console.ReadLine();
                    Console.Write("Cognome: ");
                    var c = Console.ReadLine();
                    Console.Write("Eta: ");
                    var e = int.Parse(Console.ReadLine());
                    var newS = new Studente { Nome = n, Cognome = c, Eta = e };
                    controller.Create(newS);
                    break;

                case "2":
                    var list = controller.GetAll();
                    foreach(var st in list)
                        Console.WriteLine($"{st.Id} - {st.Nome} - {st.Cognome} - {st.Eta}");
                    Console.ReadKey();
                    break;

                case "3":
                    Studente sUpd = new Studente();
                    Console.Write("ID: ");
                    sUpd.Id= int.Parse(Console.ReadLine());
                    Console.Write("Nuovo nome: ");
                    sUpd.Nome = Console.ReadLine();
                    Console.Write("Nuovo Cognome: ");
                    sUpd.Cognome = Console.ReadLine();
                    Console.Write("Nuova eta: ");
                    sUpd.Eta = int.Parse(Console.ReadLine());
                    controller.Update(sUpd);
                    break;

                case "4":
                    Console.Write("ID da eliminare: ");
                    int ide = int.Parse(Console.ReadLine());
                    controller.Delete(ide);
                    break;

                case "0": return;
            }
        }
    }
}
