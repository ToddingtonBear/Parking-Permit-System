using System;
using ConsoleApplication_Database;
using NUnit.Framework;

namespace Assignment6
{
    [TestFixture]
    class Test
    {
        [TestCase]
        public void TestUnique()    //testing the number of unique vehicles counted
        {
            Console.WriteLine("Testing UniqueVehicles()");  //print message
            int result = Permits.UniqueVehicles();  //call method and save as result
            Assert.AreEqual(10, result);    //check if result correct
        }

        [TestCase]
        public void TestPermitsIssued()
        {
            Console.WriteLine("Testing PermitsIssued()");  //print message
            int result = Permits.PermitsIssued();  //call method and save as result
            Assert.AreEqual(8, result);    //check if result correct
        }

        [TestCase]
        public void TestFees()
        {
            Console.WriteLine("Testing FeesCalculation()");  //print message
            int result = Permits.FeesCalculation();  //call method and save as result
            Assert.AreEqual(400, result);    //check if result correct
        }



    }
}
