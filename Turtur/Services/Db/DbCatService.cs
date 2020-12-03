using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Turtur.Models;
using Turtur.Utills;
using Turtur.Utills.Helpers.Db;

namespace Turtur.Services.Db
{
    public class DbCatService
    {
        public void AddNew(Cat cat)
        {
            var columnName = '"' + cat.Name + '"';
            var stringQuery = $"{Constants.SqlCommands.InsertInto + Constants.TableNames.Cats}({Constants.TableFields.Cost},{Constants.TableFields.Name},{Constants.TableFields.Weight})Values({cat.Cost},{columnName},{cat.Weight})";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void UpdateById(Cat cat)
        {
            var columnName = '"' + cat.Name + '"';
            var stringQuery = $"{Constants.SqlCommands.Update}{Constants.TableNames.Cats} {Constants.SqlCommands.Set} {Constants.TableFields.Name} = {columnName}, {Constants.TableFields.Cost} = {cat.Cost}, {Constants.TableFields.Weight} = {cat.Weight} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {cat.Id};";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void DeleteById(int id)
        {
            var stringQuery = $"{Constants.SqlCommands.DeleteFrom} {Constants.TableNames.Cats} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {id};";
            DbHelper.ExecuteSql(stringQuery);
        }
        
        public List<Cat> GetAll()
        {
            var cats = new List<Cat>();

            try
            {
                using var conn = new SQLiteConnection(Constants.Paths.PathToDb);
                conn.Open();
                
                var reader = new SQLiteCommand(Constants.SqlCommands.SelectAll + Constants.TableNames.Cats, conn).ExecuteReader();

                while (reader.Read())
                {
                    int id = 0, weight = 0, cost = 0;
                    var name = string.Empty;

                    if (reader[Constants.TableFields.Id] != null) id = DbHelper.GetIntByColumn(Constants.TableFields.Id, reader);
                    if (reader[Constants.TableFields.Cost] != null) cost = DbHelper.GetIntByColumn(Constants.TableFields.Cost, reader);
                    if (reader[Constants.TableFields.Weight] != null) weight = DbHelper.GetIntByColumn(Constants.TableFields.Weight, reader);
                    if (reader[Constants.TableFields.Name] != null) name = reader[Constants.TableFields.Name].ToString();

                    var cat = new Cat(id, weight, cost, name);
                    cats.Add(cat);
                }
                
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            return cats;
        }
    }
}