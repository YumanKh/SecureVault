using System;
using System.Collections.Generic;
using System.Text;

namespace SecureVault
{
    public struct PasswordEntry
    {
        public string Website { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class PasswordManager
    {
        public void handlePasswordManager()
        {
            int input = -1;
            Console.WriteLine("\n    Password Manager");
            Console.WriteLine("===Secure-Vault===");
            Console.WriteLine("[1] Add Password");
            Console.WriteLine("[2] Get Password");
            Console.WriteLine("[3] Delete Password");
            Console.WriteLine("[4] List Passwords");
            Console.WriteLine("[0] Return to menu\n");

            try { input = int.Parse(Console.ReadLine()); }
            catch (Exception) { Console.WriteLine("Invalid input.\n"); }

            switch (input)
            {
                case 0:
                    App.state = AppState.MAIN_MENU;
                    break;
                case 1:
                    addPassword();
                    break;
            }
        }
        public void addPassword()
        {
            string website, username, password, encrypted_password;

            Console.WriteLine("\n===Add Password===");
            Console.Write("Enter site: ");
            website = Console.ReadLine();
            Console.Write("Enter username: ");
            username = Console.ReadLine();
            Console.Write("Enter password: ");
            password = Console.ReadLine();

            EncryptionService encrypt = new EncryptionService(password);
            encrypted_password = encrypt.Encrypt(password);

            try
            {
                using (StreamWriter writer = new StreamWriter("passwords.txt", true))
                {
                    writer.WriteLine($"{website}|{username}|{encrypted_password}");
                }
            }
            catch (Exception) { Console.WriteLine("Error saving password: could not open file.\n"); }
        }
    }
}
