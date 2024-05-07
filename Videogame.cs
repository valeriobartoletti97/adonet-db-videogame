using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    public class Videogame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int SoftwareHouseId { get; set; }
        public static int counter = 500;


        public Videogame( string name, string overview, string releaseDate, DateTime createdAt, DateTime updatedAt, int softwareHouseId)
        {
            counter++;
            Id = counter;
            Name = name;
            Overview = overview;
            ReleaseDate = releaseDate;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            SoftwareHouseId = softwareHouseId;
        }
    }

    public static class VideogameManager
    {
        public static string CONNECTION_STRING = "Data Source=localhost;Initial Catalog=db-videogames-query;Integrated Security=True";
        public static void InsertVideogame()
        {
            Console.WriteLine("Inserisci nome del videogioco:");
            string name = Console.ReadLine();
            Console.WriteLine("Inserisci descrizione del videogioco:");
            string overview = Console.ReadLine();
            Console.WriteLine("Inserisci la data di rilascio del videogioco in formato GG/MM/AAAA:");
            string releaseDate = Console.ReadLine();
            DateTime createdAt = DateTime.Now;
            DateTime updatedAt = DateTime.Now;
            Random r = new Random();
            int softwareHouseId = r.Next(1,7);


            Videogame newGame = new Videogame(name,overview, releaseDate, createdAt, updatedAt, softwareHouseId);
            using (SqlConnection connectionSql = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    connectionSql.Open();
                    string query = "INSERT INTO videogames (name,overview,release_date,created_at,updated_at,software_house_id) VALUES (@name, @overview, @release_date, @created_at, @updated_at, @softwarehouseId)";
                    SqlCommand cmd = new SqlCommand(query, connectionSql);
                    cmd.Parameters.Add(new SqlParameter("@name", name));
                    cmd.Parameters.Add(new SqlParameter("@overview", overview));
                    cmd.Parameters.Add(new SqlParameter("@release_date", releaseDate));
                    cmd.Parameters.Add(new SqlParameter("@created_at", createdAt));
                    cmd.Parameters.Add(new SqlParameter("@updated_at", updatedAt));
                    cmd.Parameters.Add(new SqlParameter("@softwarehouseid", softwareHouseId));

                    int affectedRows = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        public static void SearchGameId()
        {
            using (SqlConnection connectionSql = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    connectionSql.Open();
                    Console.Write("Inserisci l'id del gioco che stai cercando:");
                    string searchId = Console.ReadLine();
                    string query = "SELECT name, release_date FROM videogames WHERE id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, connectionSql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", searchId));
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string dataName= reader.GetString(0);
                                Console.WriteLine($"Il nome del gioco è : {dataName}");
                            }
                            else
                            {
                                Console.WriteLine("L'id del gioco che cerchi non è presente nel database.");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
