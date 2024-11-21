using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketTeam
{
    internal class Program
    {
        public static Connect conn = new Connect();

        // 1. Összes adat lekérdezése
        public static void GetAllData()
        {
            using (var connection = conn.Connection)
            {
                connection.Open();

                string sql = "SELECT * FROM `player`";

                using (var cmd = new MySqlCommand(sql, connection))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var player = new
                        {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            Height = dr.GetInt32(2),
                            Weight = dr.GetInt32(3),
                            CreatedTime = dr.GetDateTime(4)
                        };

                        Console.WriteLine($"Player Info: Name = {player.Name}, Height = {player.Height}cm, Weight = {player.Weight}kg");
                    }
                }
            }
        }

        // 2. Új játékos hozzáadása
        public static void AddPlayer(string name, int height, int weight)
        {
            using (var connection = conn.Connection)
            {
                connection.Open();

                string sql = "INSERT INTO `player` (name, height, weight, createdTime) VALUES (@name, @height, @weight, NOW())";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@height", height);
                    cmd.Parameters.AddWithValue("@weight", weight);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} player(s) added successfully.");
                }
            }
        }

        // 3. Játékos törlése ID alapján
        public static void DeletePlayerById(int id)
        {
            using (var connection = conn.Connection)
            {
                connection.Open();

                string sql = "DELETE FROM `player` WHERE id = @id";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} player(s) deleted successfully.");
                }
            }
        }

        // 4. Játékos módosítása ID alapján
        public static void UpdatePlayerById(int id, string name, int height, int weight)
        {
            using (var connection = conn.Connection)
            {
                connection.Open();

                string sql = "UPDATE `player` SET name = @name, height = @height, weight = @weight WHERE id = @id";

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@height", height);
                    cmd.Parameters.AddWithValue("@weight", weight);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} player(s) updated successfully.");
                }
            }
        }

        // Főprogram
        static void Main(string[] args)
        {
            // Tesztelje a funkciókat a következőképpen:

            // 1. Játékosok listázása
            Console.WriteLine("Játékosok listázása:");
            GetAllData();

            // 2. Új játékos hozzáadása
            Console.WriteLine("\nÚj játékos hozzáadása:");
            AddPlayer("Michael Jordan", 198, 98);

            // 3. Játékos törlése ID alapján
            Console.WriteLine("\nJátékos törlése (ID = 1):");
            DeletePlayerById(1);

            // 4. Játékos módosítása ID alapján
            Console.WriteLine("\nJátékos módosítása (ID = 2):");
            UpdatePlayerById(2, "Updated Player", 200, 110);

            // 5. Újra listázás
            Console.WriteLine("\nFrissített játékos lista:");
            GetAllData();
        }
    }
}
