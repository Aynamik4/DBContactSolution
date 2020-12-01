using static System.Console;
using DBContactLibrary;
using DBContactLibrary.Models;
using System.Data.SqlClient;
using System;

namespace DBContactTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test all methods of the class SqlService here...

            Contact c = new Contact()
            {
                FirstName = "Håkan",
                LastName = "Johansson",
                SSN = "19620701-1234"
            };

            try
            {
                SqlService.CreateContact(c);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            
            System.Console.WriteLine($"New contact ID: {c.ID}");
        }
    }
}