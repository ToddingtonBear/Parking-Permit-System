﻿using System;
using System.Data.OleDb;

namespace ConsoleApplication_Database
{
    class Program
    {
        static OleDbConnection con; //static connection object
        static OleDbCommand cmd;    //static command object 
        static OleDbDataReader reader;  //static reader object 
        static String ConStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Christopher\Documents\College\NET\Project\ParkingPermits.accdb; Persist Security Info=False;";
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
                Console.WriteLine(reader[0] + "/" + reader[1] + "/" + reader[2] + "/" + reader[3] + "/" + reader[4]);
            }
            con.Close();    //close connection 
        }

        public static void InsertPermit()   //method to insert a permit 
        {
            Console.Write("Student ID : "); //write this text to console 
            int ID = Convert.ToInt32(Console.ReadLine());   //take console input and convert to int variable ID 
            Console.Write("Vehicle Model : ");  //write text to console
            string model = Console.ReadLine();  //take console input and assign to model variable 
            Console.Write("Registration : ");   //etc
            string reg = Console.ReadLine();    //etc
            Console.Write("Owner : ");
            string owner = Console.ReadLine();
            Console.Write("Apartment : ");
            int apnum = Convert.ToInt32(Console.ReadLine());
            con = new OleDbConnection(ConStr);  //new connection object 
            cmd = new OleDbCommand();   //new command object 
            cmd.Connection = con;   //assign connection to command, define command (assigning taken values to new row in table
            cmd.CommandText = "INSERT INTO Permits (Student_ID,Vehicle_Model,Registration,Owner,Apartment) VALUES ('" + ID + "','" + model + "','" + reg + "','" + owner + "','" + apnum + "' )";
            con.Open(); // open connection 
            int num = cmd.ExecuteNonQuery();    // perform insertions and return number of rows affected
            con.Close();    //close connection 
            if (num > 0)    //if any rows were affected ( something was inserted) say so 
            {
                Console.WriteLine("Inserted");
            }
            else    //nothing happened, say so 
            {
                Console.WriteLine("Three are errors. The record was not inserted.");
            }
        }

        public static void UpdatePermit()   //method to upate permit 
        {
            Console.Write("Student ID : "); //write to console etc 
            int ID = Convert.ToInt32(Console.ReadLine());
            Console.Write("Vehicle Model : ");
            string model = Console.ReadLine();
            Console.Write("Registration : ");
            string reg = Console.ReadLine();
            Console.Write("Apartment : ");
            int apnum = Convert.ToInt32(Console.ReadLine());
            con = new OleDbConnection(ConStr);  //create stuff same as before
            cmd = new OleDbCommand();
            cmd.Connection = con;   //define command, where ID in table = input, change values to input values
            cmd.CommandText = "UPDATE Permits SET Vehicle_Model='" + model + "',Registration='" + reg + "',Apartment='" + apnum + "' WHERE Student_ID=" + ID;
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



        static void Main(string[] args)
        {

            while (true)    //run until there is a break 
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("1.Display permits"); //write options to console 
                Console.WriteLine("2.Insert");
                Console.WriteLine("3.Update");
                Console.WriteLine("4.Delete");
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