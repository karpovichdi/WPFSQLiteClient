using System;
using System.Data.SQLite;

namespace Turtur.Utills.Helpers.Db
{
    public static class DbHelper
    {
        public static int GetIntByColumn(string columnName, SQLiteDataReader sqLiteDataReader)
        {
            var dataFromColumn = sqLiteDataReader[columnName].ToString();
            if (!string.IsNullOrEmpty(dataFromColumn))
            {
                return int.Parse(dataFromColumn);
            }

            return 0;
        }

        public static void ExecuteSql(string stringQuery)
        {
            try
            {
                using SQLiteConnection conn = new SQLiteConnection(Constants.Paths.PathToDb);
                conn.Open();
            
                new SQLiteCommand(stringQuery, conn).ExecuteReader();
                conn.CreateCommand();
            
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void DeleteAll(string tableName) 
        {
            var stringQuery = $"DELETE FROM `{tableName}`;";
            ExecuteSql(stringQuery);
        }
    }
}