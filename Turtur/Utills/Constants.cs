using System.Data.Entity.Infrastructure.Design;

namespace Turtur.Utills
{
    public static class Constants
    {
        public static class Paths
        {
            public const string PathToDb = @"Data Source=C:\Users\Karpo\Desktop\Turtur\Turtur\cats.db;";
        }
        
        public static class SqlCommands
        {
            public const string SelectAll = "Select * from ";
            public const string InsertInto = "INSERT INTO ";
            public const string Update = "UPDATE ";
            public const string Set = "SET";
            public const string Where = "WHERE";
            public const string DeleteFrom = "DELETE FROM";
        }
        
        public static class TableNames
        {
            public const string Cats = "Cats";
            public const string Cars = "Cars";
            public const string Customer = "Customers";
            public const string Employee = "Employee";
            public const string Investors = "Investors";
            public const string Money = "Money";
            public const string Sales = "Sales";
            public const string Resources = "Resources";
            public const string Suppliers = "Supplier";
        }
        
        public static class TableFields
        {
            public const string Name = "Name";
            public const string Id = "Id";
            public const string Cost = "Cost";
            public const string Weight = "Weight";
            public const string Phone = "Phone";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string TransactionName = "TransactionName";
            public const string Customer = "Customer";
            public const string Employees = "Employees";
            public const string Supplier = "Supplier";
            public const string Date = "Date";
            public const string Cat = "Cat";
            public const string Category = "Category";
            public const string Photo = "Photo";
        }
    }
}