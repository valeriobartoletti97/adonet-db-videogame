namespace adonet_db_videogame
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Inserisci un'opzione:");
            Console.WriteLine("1. Inserisci nuovo videogioco");
            Console.WriteLine("2. Cerca gioco per id:");
            Console.WriteLine("3. Cerca gioco per nome:");
            Console.WriteLine("4. Cancella videogioco:");
            Console.WriteLine("5. Termina programma:");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    VideogameManager.InsertVideogame();
                    break;
                case "2":
                    VideogameManager.SearchGameId();
                    break;
                case "3":
                    VideogameManager.SearchGameName();
                    break;
                case "5":
                    break; 
            }
        }
    }
}
