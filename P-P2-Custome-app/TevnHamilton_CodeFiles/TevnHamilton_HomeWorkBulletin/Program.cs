using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TevnHamilton_HomeWorkBulletin;

namespace TevnHamilton_HomeWorkBulletin
{
    class Program
    {
        private static Menu _menu ;
        private static  int _userIdNum;
        private static MySqlConnection conn = null;
        private static  string cs = @"server=192.168.1.11;userid=dbsAdmin1807;password=password;database=TevinHamilton_MDV229_AppProject_DataBase;port=8889";
        static void Main(string[] args)
        {
            _menu = new Menu("Login","Sign Up","Exit");
            _menu.Title = "HomeWork Bulletin";
            Display();
        }
        private static  void Display()
        {
            Console.WriteLine("");
            Console.Clear();
            _menu.Display();
            SelectOption();
        }
        private static void SelectOption()
        {
            switch (Utility.IntValidate("\nPlease make a selection"))
            {
                case 1:
                    Login();
                    break;
                case 2:
                    SignUp();
                    break;
                case 3:
                    Console.WriteLine("Press return to exit");
                    Environment.Exit(0);
                    break;
                
                default:
                    SelectOption();
                    break;
            }
        }
        private static void Login()
        {
            string userEA = Utility.StringValidate("Please enter email Address");
            string userPassword = Utility.StringValidate("Please enter password");

            conn = new MySqlConnection(cs);
            conn.Open();

            // Form SQL Statement
            string stm = $"Select * from Users where EmailAddress = @userEA and Password = @userPassword limit 1;";

            // @userFN, @userLN, @userEA, @userPassword
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            
            cmd.Parameters.AddWithValue("@userEA", userEA);
            cmd.Parameters.AddWithValue("@userPassword", userPassword);

            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
            if(!rdr.HasRows)
            {
                Console.WriteLine("email or password is incorrect");
                Login();
            }

            Console.WriteLine("Welcome Back!!!");
            Console.WriteLine("\r\nPress return to continue.");
            Console.ReadKey();
            HomeWorkBulletin homeWorkBulletin = new HomeWorkBulletin(userEA);
        }
        private static void SignUp()
        {
            //prompt the user and valdidation using Utility class and duble check the user know the password they entered 
            string userFN = Utility.StringValidate("Please enter firstname ");
            string userLN = Utility.StringValidate("please enter last name");
            string userEA = Utility.StringValidate("please enter Email Address");
            string userPassword = Utility.StringValidate("please enter password");
            string userVP = Utility.StringValidate("please verify passord");
            while (userPassword != userVP)
            {
                Console.WriteLine("passwords do not matach please enter the correct password.");
                userVP = Console.ReadLine();
            }
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();
            
            // Form SQL Statement
            string stm = $"insert into Users(FirstName,LastName,EmailAddress,Password)" +
                $" Values (@userFN,@userLN,@userEA,@userPassword); ";

                // @userFN, @userLN, @userEA, @userPassword
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@userFN", userFN);
            cmd.Parameters.AddWithValue("@userLN", userLN);
            cmd.Parameters.AddWithValue("@userEA", userEA);
            cmd.Parameters.AddWithValue("@userPassword", userPassword);
            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
            Console.WriteLine("Welcome new user");
            Console.ReadKey();
            HomeWorkBulletin homeWorkBulletin = new HomeWorkBulletin(userEA);


        }
    }
}
