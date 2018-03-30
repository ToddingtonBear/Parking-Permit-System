using System;
using System.Data.OleDb;

namespace ConsoleApplication_Database
{
    public class Permits
    {
        static OleDbConnection con; //static connection object
        static OleDbCommand cmd;    //static command object 
        static OleDbDataReader reader;  //static reader object 
        static String ConStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Chris\Documents\College\NET\Project\ParkingPermits.accdb; Persist Security Info=False;";
        //connection string for database

        public static void GetPermit()  //method to display all permits
        {
            con = new OleDbConnection(ConStr);  //new connection object with connection string 
            cmd = new OleDbCommand();   //new command object 
            cmd.Connection = con;   //assigns connection to command 
            cmd.CommandText = "SELECT * FROM Permits";  //defines what command does
            con.Open(); //open connection 
            reader = cmd.ExecuteReader();   //retrieve value from database 
            while (reader.Read())   //while reader is reading 
            {   //display contents of the reader (values in database 
                Console.WriteLine(reader[0] + "/" + reader[1] + "/" + reader[2] + "/" + reader[3] + "/" + reader[4] + "/" + reader[5] + "/" + reader[6] + "/" + reader[7] + "/" + reader[8]);
            }
            con.Close();    //close connection 
        }

        public static void InsertPermit()   //method to insert a permit 
        {
            Console.Write("Student ID : "); //write this text to console 
            int ID = Convert.ToInt32(Console.ReadLine());   //take console input and convert to int variable ID 
            Console.Write("Owner : ");
            string owner = Console.ReadLine();
            Console.Write("Apartment : ");
            int apnum = Convert.ToInt32(Console.ReadLine());
            Console.Write("Expiration : (dd/mm/yyyy)"); //expiration date
            DateTime expire = DateTime.Parse(Console.ReadLine());
            Console.Write("Vehicle Model 1: ");  //write text to console
            string model1 = Console.ReadLine();  //take console input and assign to model variable 
            Console.Write("Registration1 : ");   //etc
            string reg1 = Console.ReadLine();    //etc
            Console.Write("Vehicle Model2 : ");  //write text to console
            string model2 = Console.ReadLine();  //take console input and assign to model variable 
            Console.Write("Registration 2: ");   //etc
            string reg2 = Console.ReadLine();    //etc
            Console.Write("Vehicle Model 3: ");  //write text to console
            string model3 = Console.ReadLine();  //take console input and assign to model variable 
            Console.Write("Registration 3: ");   //etc
            string reg3 = Console.ReadLine();    //etc
            con = new OleDbConnection(ConStr);  //new connection object 
            cmd = new OleDbCommand();   //new command object 
            cmd.Connection = con;   //assign connection to command, define command (assigning taken values to new row in table
            cmd.CommandText = "INSERT INTO Permits (Student_ID,Vehicle_Model_1,Registration_1,Vehicle_Model_2,Registration_2,Vehicle_Model_3,Registration_3,Owner,Apartment,Expires) VALUES ('" + ID + "','" + model1 + "','" + reg1 + "','" + model2 + "','" + reg2 + "','" + model3 + "','" + reg3 + "','" + owner + "','" + apnum + "','" + expire + "'  )";
            con.Open(); // open connection 
            int num = cmd.ExecuteNonQuery();    // perform insertions and return number of rows affected
            con.Close();    //close connection 
            if (num > 0)    //if any rows were affected ( something was inserted) say so 
            {
                Console.WriteLine("Inserted");
            }
            else    //nothing happened, say so 
            {
                Console.WriteLine("There are errors. The record was not inserted.");
            }
        }

        public static void UpdatePermit()   //method to upate permit 
        {
            Console.Write("Student ID : "); //write to console etc 
            int ID = Convert.ToInt32(Console.ReadLine());
            Console.Write("Apartment : ");
            int apnum = Convert.ToInt32(Console.ReadLine());
            Console.Write("Expiration : (dd/mm/yyyy)"); //expiration date
            DateTime expire = DateTime.Parse(Console.ReadLine());
            Console.Write("Vehicle Model 1: ");
            string model1 = Console.ReadLine();
            Console.Write("Registration 1: ");
            string reg1 = Console.ReadLine();
            Console.Write("Vehicle Model 2: ");
            string model2 = Console.ReadLine();
            Console.Write("Registration 2: ");
            string reg2 = Console.ReadLine();
            Console.Write("Vehicle Model 3: ");
            string model3 = Console.ReadLine();
            Console.Write("Registration 3: ");
            string reg3 = Console.ReadLine();
            con = new OleDbConnection(ConStr);  //create stuff same as before
            cmd = new OleDbCommand();
            cmd.Connection = con;   //define command, where ID in table = input, change values to input values
            cmd.CommandText = "UPDATE Permits SET Vehicle_Model_1='" + model1 + "',Registration_1='" + reg1 + "',Vehicle_Model_2='" + model2 + "',Registration_2='" + reg2 + "',Vehicle_Model_3='" + model3 + "',Registration_3='" + reg3 + "',Apartment='" + apnum + "',Expires='" + expire + "' WHERE Student_ID=" + ID;
            con.Open(); //open connection 
            int num = cmd.ExecuteNonQuery();  //execute query and return number of rows affected
            con.Close();    //close connection 
            if (num > 0)
            {   //if rows affected, say so
                Console.WriteLine("Record updated");
            }
            else    //if no rows affected, say so 
            {
                Console.WriteLine("Errors. Record not updated");
            }
        }

        public static void DeletePermit()   //method to delete an entry 
        {
            Console.Write("Student ID : "); //write to console, take in input 
            int ID = Convert.ToInt32(Console.ReadLine());
            con = new OleDbConnection(ConStr);
            cmd = new OleDbCommand();
            cmd.Connection = con;   //command is where ID in table = input ID, delete this row
            cmd.CommandText = "DELETE FROM Permits WHERE Student_ID=" + ID + "";
            con.Open(); //open 
            int num = cmd.ExecuteNonQuery();    //execute and return number of rows affected 
            con.Close();    //close
            if (num > 0)
            {
                Console.WriteLine("Deleted.");
            }
            else
            {
                Console.WriteLine("Errors present. Record not deleted.");
            }
        }

        public static int UniqueVehicles()
        {
            con = new OleDbConnection(ConStr);  //new connection object with connection string 
            cmd = new OleDbCommand();   //new command object 
            cmd.Connection = con;   //assigns connection to command 
            cmd.CommandText = "SELECT Student_ID, Vehicle_Model_1, Vehicle_Model_2, Vehicle_Model_3 FROM Permits";  //defines what command does
            con.Open(); //open connection 
            reader = cmd.ExecuteReader();   //retrieve value from database 
            int totalcount = 0;
            while (reader.Read())   //while reader is reading 
            {
                int count = 0;      // counter for each permit to track number of vehicles 
                for (int i = 1; i <= 3; i++) //loop through vehicles 
                {
                    if (reader[i] != DBNull.Value)  //if cell exists 
                    {
                        count++;        //add to count
                        totalcount++;
                        if (String.IsNullOrEmpty(Convert.ToString(reader[i])))
                        {               //if the cell just contains an empty string or nothing
                            count--;        //subtract from count
                            totalcount--;
                        }
                    }
                }
                //display contents of the reader ( the id and the count value 
                Console.WriteLine(reader[0] + " has " + count + " vehicles assigned");
            }
            con.Close();    //close connection 
            return totalcount;
        }

        public static int PermitsIssued()
        {
            int count = 0;  // counter to track how many rows in table (how many permits)
            con = new OleDbConnection(ConStr);  //new connection object with connection string 
            cmd = new OleDbCommand();   //new command object 
            cmd.Connection = con;   //assigns connection to command 
            cmd.CommandText = "SELECT Student_ID FROM Permits";  //defines what command does
            con.Open(); //open connection 
            reader = cmd.ExecuteReader();   //retrieve value from database 
            while (reader.Read())   //while reader is reading 
            {   //display contents of the reader (values in database 
                count++;
            }
            con.Close();    //close connection 
            Console.WriteLine(count + " permits currently issued");
            return count;
        }

        public static int FeesCalculation()
        {
            con = new OleDbConnection(ConStr);  //new connection object with connection string 
            cmd = new OleDbCommand();   //new command object 
            cmd.Connection = con;   //assigns connection to command 
            //initialise fees 
            int fees = 100;
            int totalFees = 0;
            cmd.CommandText = "SELECT * FROM Permits";
            con.Open(); //open connection 
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //store expiration date 
                DateTime expire = Convert.ToDateTime(reader["Expires"]);
                int due = 0;    //fees due on current permit
                if (expire < DateTime.Now)  //if current date later than expiry 
                {
                    totalFees = totalFees + fees;   //add to total fees
                    due += fees;                    //add to current permit fees 
                }
                Console.WriteLine(due + "due on permit " + reader[0]);
            }
            Console.WriteLine("Total fees: " + totalFees);
            //close the reader when no longer needed
            reader.Close();
            return totalFees;
        }




            static void Main(string[] args)
        {
            while (true)    //run until there is a break 
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("1.Display permits"); //write options to console 
                Console.WriteLine("2.Insert");
                Console.WriteLine("3.Update");
                Console.WriteLine("4.Delete");
                Console.WriteLine("5.Unique vehicles");
                Console.WriteLine("6.Permits issued");
                Console.WriteLine("7.Fees");
                Console.WriteLine("------------------------------");
                Console.Write("Select : ");
                string choice = Console.ReadLine(); //read input and check against each case
                Console.WriteLine("--------------------------------");
                if (choice == "1")
                {
                    GetPermit();
                    Console.WriteLine("------------------------------");
                }
                else if (choice == "2")
                {
                    InsertPermit();
                    Console.WriteLine("---------------------------------");
                    GetPermit();
                    Console.WriteLine("------------------------------");
                }
                else if (choice == "3")
                {
                    UpdatePermit();
                    Console.WriteLine("----------------------------------");
                    GetPermit();
                    Console.WriteLine("------------------------------");
                }
                else if (choice == "4")
                {
                    DeletePermit();
                    Console.WriteLine("----------------------------------");
                    GetPermit();
                    Console.WriteLine("------------------------------");
                }
                else if (choice == "5")
                {
                    UniqueVehicles();
                }
                else if (choice == "6")
                {
                    PermitsIssued();
                }
                else if (choice == "7")
                {
                    FeesCalculation();
                }
                Console.Write("Continue? (y/n) : ");
                string check = Console.ReadLine();
                if (check != "y") //if input is not y, break loop and end program 
                {
                    break;
                }
            }
        }
    }
}