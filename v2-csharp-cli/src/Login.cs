using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace SecureVault
{
    public class Login
    {
        public static EncryptionService encryption;

        public void handleLogin()
        {
            int input = -1;
            Console.WriteLine("\n    Login Menu");
            Console.WriteLine("===Secure-Vault===");
            Console.WriteLine("[1] Login");
            Console.WriteLine("[2] Create Account");
            Console.WriteLine("[0] Exit\n");

            try { input = int.Parse(Console.ReadLine()); }
            catch (Exception) { Console.WriteLine("Invalid input.\n"); }

            switch (input)
            { 
                case 0:
                    App.state = AppState.QUIT;
                    break;
                case 1:
                    login();
                    break;
                case 2:
                    createAccount();
                    break;
            }
        }

        public void login()
        {
            string username, password, encrypted_password, decrypted_password;

            try
            {
                Console.WriteLine("\nLogin Menu");
                Console.WriteLine("===Login===");
                Console.Write("Enter username: ");
                username = Console.ReadLine();
                Console.Write("Enter password: ");
                password = Console.ReadLine();

                encryption = new EncryptionService(password); // Ensure encryption key is initialized

                using (StreamReader reader = new StreamReader("users.txt"))
                {
                    string line;
                    bool found = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length == 2 && parts[0] == username)
                        {
                            found = true;
                            encrypted_password = parts[1];
                            decrypted_password = encryption.Decrypt(encrypted_password);
                            if (decrypted_password == password)
                            {
                                Console.WriteLine("\nLogin successful!\n");
                                App.state = AppState.MAIN_MENU;
                                return;
                            }
                            else
                            {
                                Console.WriteLine("\nIncorrect password.\n");
                                return;
                            }
                        }
                    }
                    if (!found)
                    {
                        Console.WriteLine("\nUsername not found.\n");
                    }
                }

            }
            catch (IOException)
            {
                Console.WriteLine("\nError: could not open file.\n");
            }
        }   

        public void createAccount()
        {
            string username, password, encrypted_password;
            try {
                using (StreamWriter writer = new StreamWriter("users.txt", true))
                {
                    Console.Write("Enter username: ");
                    username = Console.ReadLine();
                    Console.Write("Enter password: ");
                    password = Console.ReadLine();
                    Console.WriteLine("\nAccount created successfully!\n");

                    encrypted_password = encryption.Encrypt(password);
                    writer.WriteLine($"{username}:{encrypted_password}");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("\nError: could not open file.\n");
            }
        }
    }
}