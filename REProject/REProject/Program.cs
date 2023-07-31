using System;

using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Org.BouncyCastle.Crypto.Generators;
using BCrypt.Net;

namespace REProject
{
    class Program
    {

       public static string connectionString = "server = localhost;port = 3306; uid =root; pwd = cdac; database = RealEstateProject"; // Replace with your actual connection string.
       // "Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = RealEs2; Integrated Security = True; "
        static void Main(string[] args)
        {

            Console.WriteLine();
            Console.WriteLine("        $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$        ");
            Console.WriteLine("        $$$                                                 $$$        ");
            Console.WriteLine("        $$$             Real Estate Website                 $$$        ");
            Console.WriteLine("        $$$                                                 $$$        ");
            Console.WriteLine("        $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$        ");
            Console.WriteLine();



            bool exit = false;
            while (!exit)
            {
                try {
                    Console.WriteLine("1. Admin Login");
                    Console.WriteLine("2. User Registration");
                    // Console.WriteLine("3. Admin Registration");
                    Console.WriteLine("3. User Login");
                    Console.WriteLine("4. Exit");
                    Console.Write("Enter your choice: ");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            AdminLogin();
                            break;
                        case 2:
                            UserRegistration();
                            break;
                        case 3:
                            UserLogin();
                    //AdminRegistration();
                            break;
                        case 4:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    //The user entered a non - integer value
                    Console.WriteLine("Invalid input. Please enter a valid integer choice.");
                }
            }
        }

       
       /* 
        
         Admin Registration
        static void AdminRegistration()
        {
            Console.Write("Enter Admin Username: ");
            string adminUsername = Console.ReadLine();
            Console.Write("Enter Admin Password: ");
            string adminPassword = Console.ReadLine();

            // Validate password complexity
            if (!IsPasswordValid(adminPassword))
            {
                Console.WriteLine("");
                Console.WriteLine("Password does not meet complexity requirements. Please make sure the password has at least 6 characters, contains at least one special character, and has at least one uppercase letter.");
                Console.WriteLine("");
                return;
            }

            // Hash the admin's password before storing it in the database
           // string hashedPassword = BCrypt.Net.BCrypt.HashPassword(adminPassword);
            string hashedPassword = adminPassword;

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO Admins (Username, Password) VALUES (@Username, @Password)";
                    using (var command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Username", adminUsername);
                        command.Parameters.AddWithValue("@Password", hashedPassword); // Store the hashed password in the database

                        try
                        {
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Admin registration successful.");
                                Console.WriteLine("");
                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Error registering admin.");
                                Console.WriteLine("");
                                Console.WriteLine("");
                            }
                        }
                        catch (MySqlException ex)
                        {
                            if (ex.Number == 1062) // MySQL error code for duplicate entry
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Username already exists. Please choose a different username.");
                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Error registering admin.");
                                Console.WriteLine("");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
*/

        //==============User Regitration==================================
        static void UserRegistration()
        {
            Console.Write("Enter User Username: ");
            string userUsername = Console.ReadLine();
            Console.Write("Enter User Password: ");
            string userPassword = Console.ReadLine();

            // Validate password complexity
            if (!IsPasswordValid(userPassword))
            {
                Console.WriteLine("");
                Console.WriteLine("Password does not meet complexity requirements. Please make sure the password has at least 6 characters, contains at least one special character, and has at least one uppercase letter.");
                Console.WriteLine("");
                return;
            }

            // Hash the user's password before storing it in the database
            // string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userPassword);
            string hashedPassword = userPassword;

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                    using (var command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Username", userUsername);
                        command.Parameters.AddWithValue("@Password", hashedPassword); // Store the hashed password in the database

                        try
                        {
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("-------------------------------------------");
                                Console.WriteLine("User registration successful.");
                                Console.WriteLine("--------------------------------------------");
                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Error registering user.");
                                Console.WriteLine("");
                                Console.WriteLine("");
                            }
                        }
                        catch (MySqlException ex)
                        {
                            if (ex.Number == 1062) // MySQL error code for duplicate entry
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Username already exists. Please choose a different username.");
                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Error registering user.");
                                Console.WriteLine("");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
        
        // Password complexity validation method
       
        static bool IsPasswordValid(string password)
        {
            // Check if the password has at least 6 characters
            if (password.Length < 6)
            {
                return false;
            }

            // Check if the password contains at least one special character, one uppercase letter, and one lowercase letter
            if (!Regex.IsMatch(password, @"^(?=.*[!@#$%^&*(),.?\\"":{}|<>])(?=.*[A-Z])(?=.*[a-z]).*$"))
            {
                return false;
            }

            return true;
        }

       
// ...
//===============AdminLogin============================


    
         static void AdminLogin()
            {
                Console.Write("Enter Admin Username: ");
                string adminUsername = Console.ReadLine();
                Console.Write("Enter Admin Password: ");
                string adminPassword = Console.ReadLine();

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                //string selectQuery = "SELECT Password FROM Admins WHERE Username = @Username and @Password= Password";
                string selectQuery = "SELECT Password FROM Admins WHERE Username = @Username and @Password= Password";

                using (var command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Username", adminUsername);
                        command.Parameters.AddWithValue("@Password", adminPassword);

                    string hashedPassword = command.ExecuteScalar() as string;
                        //int hashedPassword = command.EndExecuteNonQuery()
                         //if (hashedPassword != null && BCrypt.Net.BCrypt.Verify(adminPassword, hashedPassword))
                         if (hashedPassword != null  )

                        {
                            Console.WriteLine("");
                            Console.WriteLine("Admin login successful.............");
                            Console.WriteLine("");
                            AdminMenu(); // Move to the Admin Menu after successful login.
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Invalid admin credentials.");
                            Console.WriteLine("");
                            Console.WriteLine("");
                        }
                    }
                }
            }

        static void UserLogin()
        {
            Console.Write("Enter User Username: ");
            string userUsername = Console.ReadLine();
            Console.Write("Enter User Password: ");
            string userPassword = Console.ReadLine();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                //string selectQuery = "SELECT Password FROM Users WHERE Username = @Username";
                string selectQuery = "SELECT Password FROM Users WHERE Username = @Username and @Password= Password";

                using (var command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", userUsername);
                    command.Parameters.AddWithValue("@Password", userPassword);

                    string hashedPassword = command.ExecuteScalar() as string;

                    // if (hashedPassword != null && BCrypt.Net.BCrypt.Verify(userPassword, hashedPassword))
                    if (hashedPassword != null)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("User login successful........");
                        Console.WriteLine("");
                        UserMenu(); // Move to the User Menu after successful login.
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Invalid user credentials.");
                        Console.WriteLine("");
                        Console.WriteLine("");
                    }
                }
            }
        }
  
    //=============== Admin Menu==========================
        static void AdminMenu()
        {
            bool logout = false;
            while (!logout)
            {
            try { 
                Console.WriteLine("\n==============Admin Menu===================");
                Console.WriteLine("1. Add New Property record");
                Console.WriteLine("2. Update Property record");
                Console.WriteLine("3. Delete Property record");
                Console.WriteLine("4. View All available Property records ");
               // Console.WriteLine("5. View Property requests");
                Console.WriteLine("5. Logout");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());
                
                
                    switch (choice)
                    {
                        case 1:
                            AddNewPropRecord();
                            break;
                        case 2:
                            UpdatePropRecord();
                            break;
                        case 3:
                            DeletePropRecord();
                            break;
                        case 4:
                            ViewAllPropRecords();
                            break;
                        case 5:
                            logout = true;

                            // ViewAllPropRequest();
                            break;
                       // case 6:
                         //   logout = true;
                           // break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer choice.");
                }
            }
        }

        // ===============User Menu==========================
        static void UserMenu()
        {
            bool logout = false;
            while (!logout)
            {
             
            try { 
                Console.WriteLine("\n=================User Menu==================");
                Console.WriteLine("1. View All Prop Records");
                Console.WriteLine("2. View My Purchased Prop");
                Console.WriteLine("3. Buy Prop");
                Console.WriteLine("4. Logout");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            ViewAllPropRecords();
                            break;
                        case 2:
                            ViewMyPurchasedProp();
                            break;
                        case 3:
                            BuyProp();
                            break;
                        case 4:
                            logout = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer choice.");
                }
            }
        }

        // ====================Add Prop Record==========================================
        static void AddNewPropRecord()
        {
            Console.WriteLine("");
            Console.WriteLine("--- Add New Prop Record ---");
            Console.Write("Enter Prop Record: ");
            Console.WriteLine("");

            Console.Write("Enter the Prop ID : ");
            int PropID = int.Parse(Console.ReadLine());
            Console.Write("Enter Prop name : ");
            string PropName = Console.ReadLine();
            Console.Write("Enter Prop Type : ");
            string PropGroup = Console.ReadLine();
            Console.Write("Enter  Price : ");
            decimal price = decimal.Parse(Console.ReadLine());

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO PropRecords (PropID, PropName, PropGroup, Price) VALUES (@PropID, @PropName, @PropGroup, @Price)";
                using (var command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@PropID", PropID);
                    command.Parameters.AddWithValue("@PropName", PropName);
                    command.Parameters.AddWithValue("@PropGroup", PropGroup);
                    command.Parameters.AddWithValue("@Price", price);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Property Record added successfully...");
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Error adding");
                        Console.WriteLine("");
                    }
                }
            }
        }

        //===================Update Rec=============================
        static void UpdatePropRecord()
        {
            Console.WriteLine("============Update Prop Record==================");
            ViewAllPropRecords();

            Console.Write("Enter the Prop ID to update: ");
            int PropId = int.Parse(Console.ReadLine());

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Check if the vehicle with the specified ID exists
                string checkVehicleQuery = "SELECT COUNT(*) FROM PropRecords WHERE PropId = @PropId";
                using (var checkCommand = new MySqlCommand(checkVehicleQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@PropId", PropId);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        Console.Write("Enter New Prop Name: ");
                        string PropName = Console.ReadLine();
                        Console.Write("Enter New Prop Type: ");
                        string PropGroup = Console.ReadLine();
                        Console.Write("Enter New Storage Price: ");
                        decimal price = decimal.Parse(Console.ReadLine());

                        string updateQuery = "UPDATE PropRecords SET PropName = @PropName, PropGroup = @PropGroup, Price = @Price WHERE PropId = @PropId";
                        using (var command = new MySqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@PropName", PropName);
                            command.Parameters.AddWithValue("@PropGroup", PropGroup);
                            command.Parameters.AddWithValue("@Price", price);
                            command.Parameters.AddWithValue("@PropId", PropId);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Prop Record updated successfully...");
                            }
                            else
                            {
                                Console.WriteLine("Error updating....");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Property found with the specified ID.");
                    }
                }
            }
        }


        // ========================Delete Prop Record========================
        static void DeletePropRecord()
        {
            Console.WriteLine("=============Delete Prop Record ==============");
            ViewAllPropRecords();

            Console.Write("Enter the Prop ID to delete: ");
            int PropId = int.Parse(Console.ReadLine());

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM PropRecords WHERE PropId = @PropId";
                using (var command = new MySqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@PropId", PropId);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Prop Record deleted successfully******.");
                    }
                    else
                    {
                        Console.WriteLine("Error deleting ");
                    }
                }
            }
        }
        // ===================ALl Records===============================
        static void ViewAllPropRecords()
        {
            Console.WriteLine("==================All Property Records======================");
            
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // RetriPrope all Prop records except the purchased ones
                string selectQuery = "SELECT * FROM PropRecords WHERE PropID NOT IN (SELECT PropID FROM UserPurchasedProp)";
                using (var command = new MySqlCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"PropID: {reader["PropId"]}, Name: {reader["PropName"]}, Group: {reader["PropGroup"]}, Price: {reader["Price"]}");
                            Console.WriteLine("------------------------------------------------------------------------------------------------");

                        }
                    }
                }
            }
        }

        /*
        static void ViewAllPropRequest()
        {
            Console.WriteLine("--- Prop Request ---");

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Request";
                using (var command = new MySqlCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Request ID: {reader["RequestId"]}, User ID: {reader["UserId"]},  Group: {reader["PropGroup"]}, Quantity: {reader["Quantity"]}, Total Price: {reader["TotalPrice"]}");
                        }
                    }
                }
            }
        }
        */

        static void BuyProp()
        {
            Console.WriteLine("======== Buy Prop ==================");
            ViewAllPropRecords();

            Console.Write("Enter the Prop ID to buy Prop: ");
            int PropID = int.Parse(Console.ReadLine());

            //Console.Write("please Enter.. ");
            // int quantity = int.Parse(Console.ReadLine());

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // RetriPrope vehicle price to calculate total price
                string getQuery = "SELECT * FROM PropRecords WHERE PropID = @PropID";
                // string getQuery = "SELECT * FROM  UserPurchasedProp WHERE PropID = @PropID";
                decimal price = 0;
                using (var command = new MySqlCommand(getQuery, connection))
                {
                    command.Parameters.AddWithValue("@PropID", PropID);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        price = Convert.ToDecimal(result);
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID. Please try again.");
                        return;
                    }
                 }

                    decimal totalPrice = price;
                    //= price *quantity;

                    // Insert Prop purchase into the database
                    string insertPurchaseQuery = "INSERT INTO UserPurchasedProp (UserId, PropID, TotalPrice, PurchaseDate) VALUES (@UserId, @PropID, @TotalPrice, @PurchaseDate)";
                    using (var insertCommand = new MySqlCommand(insertPurchaseQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@UserId", 1); // Replace with the actual user ID when you have proper user authentication.
                        insertCommand.Parameters.AddWithValue("@PropID", PropID);
                        //      insertCommand.Parameters.AddWithValue("@Quantity", quantity);
                        insertCommand.Parameters.AddWithValue("@TotalPrice", totalPrice);
                        insertCommand.Parameters.AddWithValue("@PurchaseDate", DateTime.Now); // Or use a specific purchase date if needed.
                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Prop purchased successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Error purchasing.");
                        }
                    }
                }
            }
        
        //================== View My Purchased Prop======================================
    
        static void ViewMyPurchasedProp()
        {
            Console.WriteLine("===================My Purchased Prop ==============================");

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM UserPurchasedProp";
                using (var command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserId", 1); // Replace with the actual user ID when you have proper user authentication.

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"PropID: {reader["PropID"]}, Total Price: {reader["TotalPrice"]}, Purchase Date: {reader["PurchaseDate"]}");
                            Console.WriteLine("------------------------------------------------------------------------------------------------------------");

                        }
                    }
                }
            }
        }

    }
}