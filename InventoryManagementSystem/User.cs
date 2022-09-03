using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace InventoryManagementSystem
{
    internal class User
    {
        public string id;
        public string username;
        public string name;
        public string role;
        public string password;

        public User(string id, string username, string name, string role, string password)
        {
            this.id = id;
            this.username = username;
            this.name = name;
            this.role = role;
            this.password = password;
        }

        public static int AddUser(string username, string name, string role, string password)
        {
            try
            {
                // Opening a connection to MySql server
                string connectionString = "server=localhost;database=inventory;uid=root;pwd=prince;";
                MySqlConnection conn = new MySqlConnection(connectionString);

                conn.Open();


                // SQL Query to insert new user
                string sqlquery = $"INSERT INTO `user` (`username`, `name`, `role`, `password`) VALUES (?username, ?name, ?role, ?password);";
                MySqlCommand cmd = new MySqlCommand(sqlquery, conn);
                cmd.Parameters.Add("?username", MySqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("?role", MySqlDbType.VarChar).Value = role;
                cmd.Parameters.Add("?password", MySqlDbType.VarChar).Value = MD5Hash(password);
                cmd.ExecuteNonQuery();
                conn.Close();
                //MySqlDataReader rdr = cmd.ExecuteReader();
                //rdr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

            return 1;
        }

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            try
            {
                // Opening a connection to MySql server
                string connectionString = "server=localhost;database=inventory;uid=root;pwd=prince;";
                MySqlConnection conn = new MySqlConnection(connectionString);

                conn.Open();


                // SQL Query to insert new user
                string sqlquery = "SELECT * FROM user;";
                MySqlCommand cmd = new MySqlCommand(sqlquery, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    users.Add(new User(rdr[0].ToString(), rdr[1].ToString(), rdr[2].ToString(), rdr[3].ToString(), rdr[4].ToString()));
                }
                rdr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return users;
            }

            return users;
        }

        public static int DeleteUser(string username)
        {
            try
            {
                // Opening a connection to MySql server
                string connectionString = "server=localhost;database=inventory;uid=root;pwd=prince;";
                MySqlConnection conn = new MySqlConnection(connectionString);

                conn.Open();


                // SQL Query to insert new user
                string sqlquery = $"DELETE FROM `user` WHERE (`username` = ?username);";
                MySqlCommand cmd = new MySqlCommand(sqlquery, conn);
                cmd.Parameters.Add("?username", MySqlDbType.VarChar).Value = username;
                cmd.ExecuteNonQuery();
                conn.Close();
         

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

            return 1;
        }

        public static int UpdateUser(int id, string username, string name, string role, string password)
        {
            try
            {
                // Opening a connection to MySql server
                string connectionString = "server=localhost;database=inventory;uid=root;pwd=prince;";
                MySqlConnection conn = new MySqlConnection(connectionString);

                conn.Open();


                // SQL Query to update user where id=id
                string sqlquery = "UPDATE `user` SET `name` = ?name, `role` = ?role, `password` = ?password WHERE (`id` = ?id);";
                
                MySqlCommand cmd = new MySqlCommand(sqlquery, conn);
                cmd.Parameters.Add("?username", MySqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("?role", MySqlDbType.VarChar).Value = role;
                cmd.Parameters.Add("?password", MySqlDbType.VarChar).Value = MD5Hash(password);
                cmd.Parameters.Add("?id", MySqlDbType.Int64).Value = id;
                cmd.ExecuteNonQuery();
                conn.Close();
                //MySqlDataReader rdr = cmd.ExecuteReader();
                //rdr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

            return 1;
        }

        public static Dictionary<string, string> AuthenticateUser(string username, string password)
        {
            bool authenticated = false;
            string role = "";
            string name = "";
            // dictionary
            Dictionary<string, string> result = new Dictionary<string, string>();

            try
            {
                // Opening a connection to MySql server
                string connectionString = "server=localhost;database=inventory;uid=root;pwd=prince;";
                MySqlConnection conn = new MySqlConnection(connectionString);

                conn.Open();


                // SQL Query to update user where id=id
                string sqlquery = "SELECT `name`, `password`, `role` FROM `user`  WHERE (`username` = ?username);";
                MySqlCommand cmd = new MySqlCommand(sqlquery, conn);
                cmd.Parameters.Add("?username", MySqlDbType.VarChar).Value = username;
                MySqlDataReader rdr = cmd.ExecuteReader();

                 
                while (rdr.Read())
                {
                    if (MD5Hash(password) == rdr[1].ToString()) authenticated = true; role = rdr[2].ToString(); name = rdr[0].ToString(); 
                }
                rdr.Close();

                
                result.Add("role", role);
                result.Add("name", name);

                if (authenticated)
                {
                    result.Add("auth", "1");
                    return result;
                } else return result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return result;
            }

        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
