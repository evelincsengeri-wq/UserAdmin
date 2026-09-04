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
    }
}
