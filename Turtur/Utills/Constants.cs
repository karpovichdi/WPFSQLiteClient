using System.Data.Entity.Infrastructure.Design;

namespace Turtur.Utills
{
    public static class Constants
    {
        public static class Paths
        {
            public const string PathToDB = @"Data Source=C:\Users\Karpo\Desktop\Turtur\Turtur\cats.db;";
        }
        
        public static class SqlCommands
        {
            public const string SelectAll = "Select * from ";
            public const string InsertInto = "INSERT INTO ";
        }
        
        public static class TableNames
        {
            public const string Cats = "Cats";
        }
        
        public static class TableFields
        {
            public const string Name = "Name";
            public static string Id = "Id";
            public static string Cost = "Cost";
            public static string Weight = "Weight";
        }
    }
}