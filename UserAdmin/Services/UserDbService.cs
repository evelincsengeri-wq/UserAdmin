using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using UserAdmin.Models;

namespace UserAdmin.Services
{
    internal class UserDbService
    {
        public string ConnectionString = "Server=localhost;Database=useradmin;User=root;Password=;";  //Kapcsolat létrehozása az adatbázissal

        public void Add(User user)
        {
            var connection = new MySqlConnection(ConnectionString);  //Kapcsolat létrehozása az adatbázissal
            connection.Open(); //Kapcsolat megnyitása az adatbázissal

            string sql = @"INSERT INTO `users`(`username`, `email`, `password`, `registeredAt`) 
VALUES (@Username,@Email,@Password,@RegisteredAt)";

            var cmd = new MySqlCommand(sql, connection); //SQL lekérdezés létrehozása

            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Email", user.Email);                   //SQL lekérdezés paraméterek hozzáadása
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@RegisteredAt", user.RegisteredAt);
            cmd.ExecuteNonQuery();                                          //SQL lekérdezés ezzel a sorral végrehajtódik az adatbázisban

            connection.Close();
        }

        // Fixed method: implement FindByEmail and return null if not found.
        public User? FindByEmail(string email)
        {
            using var connection = new MySqlConnection(ConnectionString); //Kapcsolat létrehozása az adatbázissal
            connection.Open();

            string sql = @"SELECT `username`,`email`,`password`,`registeredAt` FROM `users` WHERE `email`=@email";  //SQL lekérdezés létrehozása

            var cmd = new MySqlCommand(sql, connection);   //SQL lekérdezés létrehozása

            cmd.Parameters.AddWithValue("@email", email); //SQL lekérdezés paraméterek hozzáadása

            var reader = cmd.ExecuteReader(); //SQL lekérdezés végrehajtása

            
            if (reader.Read())             //Ha van találat az adatbázisban
            {
                var user = new User         //Új User objektum létrehozása
                {
                    Username = reader.GetString(0),
                    Email = reader.GetString(1),                         //SQL lekérdezés eredményének lekérése
                    Password = reader.GetString(2),
                    RegisteredAt = reader.GetDateTime(3)
                };
                connection.Close();
                return user; // Return the found user
            }


            connection.Close(); //Kapcsolat lezárása az adatbázissal
            return null;  // Return null if no user is found with the given email
        } 
    }
}
