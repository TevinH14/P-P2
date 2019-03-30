using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TevnHamilton_HomeWorkBulletin
{
    class HomeWorkBulletin
    {
        private Menu _menu;
        private int _userId; 
        private string _userEmail;
        private int _counter;
        private MySqlConnection conn = null;
        private string cs = @"server=192.168.1.11;userid=dbsAdmin1807;password=password;database=TevinHamilton_MDV229_AppProject_DataBase;port=8889";
        public HomeWorkBulletin(string email)
        {
            Console.Clear();
            _userEmail = email;
            GetUserId();
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();

            // Form SQL Statement
            string stm = "select * from Assignments where userId = @userId order by AssignmentStartDate asc, AssignmentName limit 5 ;";
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@userId", _userId);
            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
            int counter = 1;
            while (rdr.Read())
            {
                string assignmentName = (rdr["AssignmentName"].ToString());
                string assignmentNote = (rdr["AssignmentNote"].ToString());
                string assignmentStartDate = (rdr["AssignmentStartDate"].ToString());
                string assignmentDueData = (rdr["AssignmentDueDate"].ToString());
                Console.WriteLine($"{counter}) Assignment Name:{assignmentName}, Assignment Notes: {assignmentNote}," +
                    $"Start date: {assignmentStartDate}, Due Date: {assignmentDueData}.");
                counter++;
            }
            _menu = new Menu("course", "completed Courses", "Assignments", "settings");
            _menu.Title = "HomeWorkBulletin";
            Console.WriteLine("\r\n");
            Display();
        }
        private void Display()
        {
            _menu.Display();
            SelectOption();
        }
        private void SelectOption()
        {
            switch (Utility.IntValidate("\nPlease make a selection"))
            {
                case 1:
                    Course();
                    break;
                case 2:
                    CompletedAssigmnents();
                    break;
                case 3:
                    Assignments();
                    break;
                case 4:
                    Settings();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    SelectOption();
                    break;
            }
        }
        private void Course()
        {
            Console.Clear();
            Menu menu = new Menu("Display Course", "Add Course", "Back to home page", "Exit App");
            menu.Title = "Courses";
            menu.Display();
            switch (Utility.IntValidate("\nPlease make a selection"))
            {
                case 1:
                    CourseDisplay();
                    break;
                case 2:
                    AddCourse();
                    break;
                case 3:
                    Console.Clear();
                    Display();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Course();
                    break;
            }
        }
        private void CourseDisplay()
        {
            Console.Clear();
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();
            // Form SQL Statement
            string stm = "select * from Courses where userId = @userId order by CourseStartDate asc;";
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@userId", _userId);
            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
            int counter = 1;
            while (rdr.Read())
            {
                string courseName = (rdr["CourseName"].ToString());
                string courseStartDate = (rdr["CourseStartDate"].ToString());
                string courseEndDate = (rdr["CourseEndDate"].ToString());
                Console.WriteLine($"{counter}) Course Name:{courseName}, Start date: {courseStartDate}, End Date: {courseEndDate}.");
                counter++;
            }
            Console.WriteLine("\r\n");
            Menu menu = new Menu("Add course","Main Menu","Exit App");
            menu.Title = "Courses Display";
            menu.Display();
            switch (Utility.IntValidate("\nPlease make a selection"))
            {
                case 1:
                    AddCourse();
                    break;
                case 2:
                    Console.Clear();
                    Display();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    CourseDisplay();
                    break;
            }
        }
        private void AddCourse()
        {
            Console.Clear();
            //prompt the user and valdidation using Utility class and duble check the user know the password they entered 
            string courseName = Utility.StringValidate("Please enter Course Name");
            string courseStartDate = Utility.DateValidation("Please enter course Start Date following this format (2019-01-01)");
            string courseEndDate = Utility.DateValidation("Please enter course End Date following this format (2019-01-01)");
          
           
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();

            // Form SQL Statement
            string stm = $"insert into Courses(UserId,courseName,courseStartDate,courseEndDate)" +
                $" Values (@userId,@courseName,@courseStartDate,@courseEndDate); ";

            // @userFN, @userLN, @userEA, @userPassword
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@userId", _userId);
            cmd.Parameters.AddWithValue("@courseName", courseName);
            cmd.Parameters.AddWithValue("@courseStartDate", courseStartDate);
            cmd.Parameters.AddWithValue("@courseEndDate", courseEndDate);
            
            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
            CourseDisplay();
        }
        private void CompletedAssigmnents()
        {
            Console.Clear();
            Console.Clear();
            Menu menu = new Menu("Display completed assignment", "Add completed assignment", "Back to home page", "Exit App");
            menu.Title = "Courses";
            menu.Display();
            switch (Utility.IntValidate("\nPlease make a selection"))
            {
                case 1:
                    DisplayCompletedAssignments();
                    break;
                case 2:
                    AddCompletedCourse();
                    break;
                case 3:
                    Console.Clear();
                    Display();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Course();
                    break;
            }
        }
        private void DisplayCompletedAssignments()
        {
            Console.Clear();
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();
            // Form SQL Statement
            string stm = "select * from CompletedAssignments where userId = @userId order by AssignmentsStartDate asc;";
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@userId", _userId);
            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
            int counter = 1;
            while (rdr.Read())
            {
                string assignmentId = (rdr["CompletedAssignmentsId"].ToString());
                string assignmentName = (rdr["AssignmentsName"].ToString());
                
                Console.WriteLine($"{counter}) Assignment Id:{assignmentId}, Assignment Name: {assignmentName}");
                counter++;
            }
            Console.WriteLine("\r\n");
            Menu menu = new Menu("Add Assignment to complete", "Main Menu", "Exit App");
            menu.Title = "Courses Display";
            menu.Display();
            switch (Utility.IntValidate("\nPlease make a selection"))
            {
                case 1:
                    AddCompletedCourse();
                    break;
                case 2:
                    Console.Clear();
                    Display();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    CourseDisplay();
                    break;
            }
        }
        private void AddCompletedCourse()
        {
            Console.Clear();
            DisplayAssignments();
            int assignmentId = Utility.IntValidate("enter the Assignment Id of the assignment you like to add to complete");
            
        // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();
            // Form SQL Statement
            string stm = $" select* from Assignments where AssignmentId = @AssignmentId order by AssignmentStartDate asc; ";
            MySqlDataReader rdr2 = null;
            // Prepare SQL Statement
            MySqlCommand cmd2 = new MySqlCommand(stm, conn);
            cmd2.Parameters.AddWithValue("@AssignmentId", assignmentId);

            // Execute SQL Statement and Convert Results to a String
            rdr2 = cmd2.ExecuteReader();
            while (rdr2.Read())
            {
                string assignmentName = (rdr2["AssignmentName"].ToString());
                string assignmentNote = (rdr2["AssignmentNote"].ToString());
                string assignmentSD = (rdr2["AssignmentStartDate"].ToString());
                string assignmentED = (rdr2["AssignmentDueDate"].ToString());
                string course = (rdr2["course"].ToString());
                UpdateCompletedAssignments(assignmentName, assignmentNote, assignmentSD, assignmentED, course);
            }
            Console.WriteLine("Your assignment have been transfered\r\n");
            Console.WriteLine("press return to continue.");
            Console.ReadKey();
            DisplayCompletedAssignments();
        }
        private void Assignments()
        {
            Console.Clear();
            Menu menu = new Menu("Display Assignments", "Add Assignments", "Back to home page", "Exit App");
            menu.Title = "Assignments";
            menu.Display();
            switch (Utility.IntValidate("\nPlease make a selection"))
            {
                case 1:
                    AssignmentDisplay();
                    break;
                case 2:
                    AddAssignment();
                    break;
                case 3:
                    Console.Clear();
                    Display();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Course();
                    break;
            }
        }
        private void AssignmentDisplay()
        {
            Console.Clear();
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();

            // Form SQL Statement
            string stm = "select * from Assignments where UserId = @userId order by AssignmentStartDate asc;";
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@userId", _userId);
            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
            int counter = 1;
            while (rdr.Read())
            {
                string assignmentName = (rdr["AssignmentName"].ToString());
                string assignmentNote = (rdr["AssignmentNote"].ToString());
                string assignmentStartDate = (rdr["AssignmentStartDate"].ToString());
                string assignmentDueData = (rdr["AssignmentDueDate"].ToString());
                Console.WriteLine($"{counter}) Assignment Name:{assignmentName}, Assignment Notes: {assignmentNote}," +
                    $"Start date: {assignmentStartDate}, Due Date: {assignmentDueData}.");
                counter++;
            }
            Console.WriteLine("======================================================================");
            Menu menu = new Menu("Add Assignment", "Main Menu", "Exit App");
            menu.Title = "Assignment Display";
            menu.Display();
            switch (Utility.IntValidate("\nPlease make a selection"))
            {
                case 1:
                    AddCourse();
                    break;
                case 2:
                    Console.Clear();
                    Display();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    CourseDisplay();
                    break;
            }
        }
        private void AddAssignment()
        {
            Console.Clear();
            //prompt the user and valdidation using Utility class and duble check the user know the password they entered 
            string assignmentName = Utility.StringValidate("Please enter Assignment Name.");
            string AssignmentNotes = Utility.StringValidate("please enter Assignment notes.");
            string assignmentStartDate = Utility.DateValidation("Please enter Assignment Start Date following this format (2019-01-01)");
            string assignmentEndDate = Utility.DateValidation("Please enter Assignment End Date following this format (2019-01-01)");
            
            UserCourseDisplay();
            int course = Utility.IntValidate("pick a course to assigned to the assignment.");
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();

            // Form SQL Statement
            string stm = $"insert into Assignments(userId,AssignmentName,AssignmentNote,AssignmentStartDate,AssignmentDueDate,course)" +
                $" Values (@userId,@AssignmentName,@AssignmentNote,@AssignmentStartDate,@AssignmentDueDate,@course); ";

            // @userFN, @userLN, @userEA, @userPassword
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@userId", _userId);
            cmd.Parameters.AddWithValue("@AssignmentName", assignmentName);
            cmd.Parameters.AddWithValue("@AssignmentNote", AssignmentNotes);
            cmd.Parameters.AddWithValue("@AssignmentStartDate", assignmentStartDate);
            cmd.Parameters.AddWithValue("@AssignmentDueDate", assignmentEndDate);
            cmd.Parameters.AddWithValue("@course", course);

            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
            AssignmentDisplay();
        }
        private void Settings()
        {
            Console.Clear();
            Menu menu = new Menu("Display profile info", "Report", "Rate", "Main Menu","Exit App");
            menu.Title = "Settings";
            menu.Display();
            switch (Utility.IntValidate("\nPlease make a selection"))
            {
                case 1:
                    DisplayUser();
                    break;
                case 2:
                    Report();
                    break;
                case 3:
                    Rate();
                    break;
                case 4:
                    Console.Clear();
                    Display();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Course();
                    break;
            }
        }
        private void DisplayUser()
        {
            Console.Clear();
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();

            // Form SQL Statement
            string stm = "select * from Users where UserId = @userId order by Firstname asc, FirstName limit 1 ;";


            // @userFN, @userLN, @userEA, @userPassword
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@userId", _userId);
            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string firstName = (rdr["FirstName"].ToString());
                string lastName = (rdr["LastName"].ToString());
                string emailAddress = (rdr["EmailAddress"].ToString());
                Console.WriteLine($" FirstName:{firstName}, Last Name: {lastName}, EmailAddress{emailAddress}");
            }
            Console.WriteLine("\r\nPress return to return to the setting menu");
            Console.ReadKey();
            Settings();
        }
        private void Report()
        {

            Console.Clear();
            
            string reportComment = Utility.DateValidation("Please enter your comment below");          
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();

            // Form SQL Statement
            string stm = $"insert into Report(userId,ReportComment)" +
                $" Values (@userId,@ReportComment); ";
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@userId", _userId);
            cmd.Parameters.AddWithValue("@ReportComment", reportComment);
            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
            Console.WriteLine("\r\nPress enter to go back to settings");
            Console.ReadKey();
            Settings();
        }
        private void Rate()
        {

            Console.Clear();
            //prompt the user and valdidation using Utility class and duble check the user know the password they entered 
            
            string rating = Utility.DateValidation("Please enter Assignment Start Date following this format (2019-01-01)");
            string reviewComment = Utility.DateValidation("Please enter Assignment End Date following this format (2019-01-01)");
          
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();

            // Form SQL Statement
            string stm = $"insert into Reviews(userId,Rating,ReviewComment)" +
                $" Values (@userId,@Rating,@ReviewComment; ";

            // @userFN, @userLN, @userEA, @userPassword
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@userId", _userId);
            cmd.Parameters.AddWithValue("@Rating", rating);
            cmd.Parameters.AddWithValue("@ReviewComment", reviewComment);

            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
            Console.WriteLine("\r\nPress enter to go back to settings");
            Console.ReadKey();
            Settings();        
        }
        private void GetUserId()
        {
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();

            // Form SQL Statement
            string stm = "select * from Users where EmailAddress = @EmailAddress;";
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@EmailAddress", _userEmail);
            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
            // string version = Convert.ToString(cmd.ExecuteScalar());
            string userIdString;
            while (rdr.Read())
            {
                userIdString = (rdr["UserId"].ToString());
                Int32.TryParse(userIdString, out _userId);
            }
            
        }
        private void UserCourseDisplay()
        {
            Console.Clear();
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();
            // Form SQL Statement
            string stm = "select * from Courses where userId = @userId order by CourseStartDate asc;";
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@userId", _userId);
            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
            int counter = 1;
            while (rdr.Read())
            {
                string courseId = (rdr["CourseId"].ToString());
                string courseName = (rdr["CourseName"].ToString());
               
              
                Console.WriteLine($"{counter}) Course Id:{courseId},Course Name:{courseName}.");
                counter++;
            }
        }
        private void DisplayAssignments()
        {
            Console.Clear();
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();
            // Form SQL Statement
            string stm = "select * from Assignments where userId = @userId order by AssignmentStartDate asc;";
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@userId", _userId);
            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
            int counter = 1;
            while (rdr.Read())
            {
                string assignmentId = (rdr["AssignmentId"].ToString());
                string assignmentName = (rdr["AssignmentName"].ToString());
                Console.WriteLine($"{counter}) Assignment Id:{assignmentId}, Assignment Name: {assignmentName}.");
                counter++;
            }

        }
        private void UpdateCompletedAssignments(string aN,string n,string sd,string ed,string c)
        {
            // Open a connection to MySQL
            conn = new MySqlConnection(cs);
            conn.Open();

            // Form SQL Statement
            string stm = $"insert into CompletedAssignments(UserId,AssignmentsName,AssignmentNote,AssignmentsStartDate,AssignmentDueDate,course)" +
                $" Values (@userId,@AssignmentsName,@AssignmentNote,@AssignmentsStartDate,@AssignmentDueDate,@course); ";

            // @userFN, @userLN, @userEA, @userPassword
            MySqlDataReader rdr = null;
            // Prepare SQL Statement
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            cmd.Parameters.AddWithValue("@userId", _userId);
            cmd.Parameters.AddWithValue("@AssignmentsName", aN);
            cmd.Parameters.AddWithValue("@AssignmentNote", n);
            cmd.Parameters.AddWithValue("@AssignmentsStartDate", sd);
            cmd.Parameters.AddWithValue("@AssignmentDueDate", ed);
            cmd.Parameters.AddWithValue("@course", c);

            // Execute SQL Statement and Convert Results to a String
            rdr = cmd.ExecuteReader();
        }

    }
}
