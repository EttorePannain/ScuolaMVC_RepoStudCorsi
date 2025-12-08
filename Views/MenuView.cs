using System;

public class MenuView {
    public void Show() {
        var studView = new StudenteView();

        while (true) {
            Console.Clear();
            Console.WriteLine("MENU PRINCIPALE");
            Console.WriteLine("1. Gestione Studenti");
            Console.WriteLine("0. Esci");
            Console.Write("Scelta: ");

            switch(Console.ReadLine()) {
                case "1": studView.Menu(); break;
                case "0": return;
            }
        }
    }
}