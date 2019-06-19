using System;
using System.Linq;
using System.Net;
using System.Security;
using Microsoft.Extensions.DependencyInjection;
using FACP.PasswordStrength.Service.DI;
using FACP.PasswordStrength.Service.Interfaces;

namespace FACP.PasswordStrength.UIConsole
{
    class Program
    {
        #region DI Setup

        private static IPasswordStrengthService _passwordStrengthService;

        private static void DependencyInjectionInit()
        {
            var services = new ServiceCollection();
            services.AddInternalServices();

            var provider = services.BuildServiceProvider();

            _passwordStrengthService = provider.GetService<IPasswordStrengthService>();

        }

        #endregion

        #region Main

        static void Main(string[] args)
        {
            DependencyInjectionInit();

            ExecutePasswordStrengthCheck();
        }

        public static void ExecutePasswordStrengthCheck()
        {
            BuildHeader();

            var option = GetPasswordOption();

            HandleSelectedOption(option);
        }

        #endregion

        #region Password Strength Methods

        private static void BuildHeader()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("#------------------------------------------------------------------------#");
            Console.WriteLine("#                          PASSWORD STRENGTH                             #");
            Console.WriteLine("#------------------------------------------------------------------------#");
        }

        private static void HandleSelectedOption(int option)
        {
            if (option == 1 || option == 2)
            {
                HandlePasswordStrength(option == 2);
            }
            else
            {
                Console.WriteLine("Closing...");
            }
        }

        private static void EnterYourPassword()
        {
            const string message = "Enter your password: ";
            var random = new Random();
            message.ToList().ForEach(c =>
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(random.Next(50, 50));
            });
        }

        private static void HandlePasswordStrength(bool option)
        {
            //EnterYourPassword();
            Console.Write("Enter your password: ");

            var password = GetPassword(option);
            var passwordStrength = _passwordStrengthService.GetStrength(password);
            var passwordBreaches = _passwordStrengthService.NumberOfTimesAppearedInDataBreaches(password);


            Console.WriteLine();
            Console.Write("Your password strength: ");
            Write(passwordStrength.ToString());
            Console.WriteLine();

            if (!string.IsNullOrWhiteSpace(passwordBreaches))
            {
                Console.Write("Number of times the password has appeared in data breaches: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(passwordBreaches);
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();

            var optionYN = string.Empty;

            while (!optionYN.ToUpper().Equals("Y") && !optionYN.ToUpper().Equals("N"))
            {
                Console.WriteLine("Please, select one of the options below:");
                Console.WriteLine("Try again? (Y/N)");

                optionYN = Console.ReadLine();

                if (optionYN.ToUpper().Equals("Y"))
                    ExecutePasswordStrengthCheck();
            }

            Console.WriteLine("Closing...");

            Environment.Exit(0);
        }

        private static int GetPasswordOption()
        {
            Console.WriteLine("Please, select one of the options below:");
            Console.WriteLine("[ 1 ] - Show the password while typing.");
            Console.WriteLine("[ 2 ] - Hide the password while typing.");
            Console.WriteLine("[ 3 ] - Exit.");


            var optionInput = Console.ReadLine();

            if (!int.TryParse(optionInput, out int option))
            {
                Console.WriteLine("Selected option is not valid.");
                ExecutePasswordStrengthCheck();
            }

            return option;
        }

        private static string GetPassword(bool hide)
        {
            var password = new SecureString();

            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.RemoveAt(password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (i.KeyChar != '\u0000')
                {
                    password.AppendChar(i.KeyChar);

                    if (hide)
                        Console.Write("*");
                    else
                        Console.Write(i.KeyChar);
                }
            }

            Console.WriteLine("");

            return new NetworkCredential(string.Empty, password).Password;
        }

        private static void Write(string strength)
        {
            switch (strength)
            {
                case "VeryWeak":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Very Weak");
                    break;
                case "Weak":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Weak");
                    break;
                case "Reasonable":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Reasonable");
                    break;
                case "Strong":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Strong");
                    break;
                case "VeryStrong":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("Very Strong");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Green;
        }

        #endregion


    }
}
