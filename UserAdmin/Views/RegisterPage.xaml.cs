using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserAdmin.Models;
using UserAdmin.Services;

namespace UserAdmin.Views
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private readonly UserDbService _userDbService = new UserDbService();
        public RegisterPage()
        {

            InitializeComponent();   //A regisztrációs oldal inicializálása
        }

        private void Register_Click(object sender, RoutedEventArgs e)   //A regisztráció gomb eseménykezelője
        {
            var username = UsernameBox.Text.Trim();   //A felhasználónév mező értékének lekérése
            var email = EmailBox.Text;         //Az email mező értékének lekérése
            var password = PasswordBoxInput.Password; //A jelszó mező értékének lekérése
            var confirmPassword = ConfirmPasswordBox.Password; //A jelszó megerősítés mező értékének lekérése

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword)) //Ha bármelyik mező üres
            {
                ErrorText.Text = "Kérlek töltsd ki az összes mezőt!"; //Hibaüzenet megjelenítése"
                ErrorText.Visibility = Visibility.Visible; //Hibaüzenet láthatóvá tétele
                return;
            }

            if (password.Length < 6) //Ha a jelszó hossza kevesebb, mint 6 karakter
            {
                ErrorText.Text = "A jelszónak legalább 6 karakter hosszúnak kell lennie!"; //Hibaüzenet megjelenítése
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            if (password != confirmPassword)
            {
                ErrorText.Text = "A jelszavak nem egyeznek!"; //Hibaüzenet megjelenítése
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            var user = new User          //Új felhasználó létrehozása
            {
                Username = username,
                Email = email,
                Password = password,
                RegisteredAt = DateTime.Now
            };

            _userDbService.Add(user); //A felhasználó hozzáadása az adatbázishoz
            MessageBox.Show("Sikeres regisztráció!"); //Sikeres regisztráció üzenet megjelenítése
        }

        private void Login_Click(object sender, RoutedEventArgs e)     //A bejelentkezés gomb eseménykezelője
        {

        }
    }
}
