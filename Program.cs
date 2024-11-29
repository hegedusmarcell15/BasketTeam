using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.BC;
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
        public static void GetAllData()
        {
            conn.Connection.Open();

            string sql = "SELECT * FROM `player`";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);

            MySqlDataReader dr = cmd.ExecuteReader();
            
            dr.Read();

            do 
            {
                var player = new
                {
                    id = dr.GetInt32(0),
                    name = dr.GetString(1),
                    height = dr.GetInt32(2),
                    weight = dr.GetInt32(3),
                    CreatedTime = dr.GetDateTime(4)
                };
                Console.WriteLine($"Játékos adatok: {player.name}, {player.CreatedTime}");
            }
            while (dr.Read());
           

            dr.Close();

            

            conn.Connection.Close();
        }
        public static void AddNewPlayer(string name, int height, int weight)
        { 
            try
            {
                conn.Connection.Open();

                string sql = $"INSERT INTO `player`(`name`, `height`, `weight`) VALUES ('{name}',{height},{weight})";

                MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
                cmd.ExecuteNonQuery();

                conn.Connection.Close();
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            
        }
        public static void DelPlayer(int id)
        {
            conn.Connection.Open();

            string sql = $"DELETE FROM `player` WHERE `id` = {id};";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            cmd.ExecuteNonQuery();

            conn.Connection.Close();
        }

        public static void UpdatePlayer(int id, string name, int height, int weight)
        {
            conn.Connection.Open();

            string sql = $"UPDATE `player` SET `name`='{name}',`height`={height},`weight`={weight} WHERE `id` = {id};";

            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            
            cmd.ExecuteNonQuery();

            conn.Connection.Close();
        }
        static void Main(string[] args)
        {
            //try
            //{
            //Console.WriteLine("Kérem a játékos nevét: ");
            //string name = Console.ReadLine();
            //Console.WriteLine("Kérem a játékos magasságát: ");
            //int height = int.Parse(Console.ReadLine());
            //Console.WriteLine("Kérem a játékos súlyát: ");
            //int weight = int.Parse(Console.ReadLine());
            //AddNewPlayer(name, height, weight);
            //}
            //catch
            //{
            //Console.WriteLine("Nem jó karakter");
            //}
            //GetAllData();

            /* try
             {
                 Console.WriteLine("Kérem a játékos azonosítót a törléshez: ");
                 int id = int.Parse(Console.ReadLine());
                 DelPlayer(id);

             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message);
             }*/
            try
            {
                Console.WriteLine("Kérem a játékos azonosítót: ");
                int id = int.Parse(Console.ReadLine());

                Console.WriteLine("Kérem az új nevet: ");
                string name = Console.ReadLine();
                Console.WriteLine("Kérem az új magasságot: ");
                int height = int.Parse(Console.ReadLine());
                Console.WriteLine("Kérem az új súlyt: ");
                int weight = int.Parse(Console.ReadLine());

                UpdatePlayer(id, name, height, weight);

                Console.WriteLine("Sikeres frissítés");
            }
            catch (Exception)
            {

                throw;
            }
            Console.ReadKey();
        }
    }
}
