using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Reflection.Metadata;
using Turtur.Models;
using Turtur.Utills;

namespace Turtur.Services.Db
{
    public class DbCatService
    {
        public List<Cat> GetAllCats()
        {
            using SQLiteConnection conn = new SQLiteConnection(Constants.Paths.PathToDB);
            conn.Open();
                
            var reader = new SQLiteCommand(Constants.SqlCommands.SelectAll + Constants.TableNames.Cats, conn).ExecuteReader();
            var cats = new List<Cat>();

            while (reader.Read())
            {
                int id = 0, weight = 0, cost = 0;
                var name = string.Empty;

                try
                {
                    if (reader[Constants.TableFields.Id] != null)
                    {
                        id = GetIntByColumn(Constants.TableFields.Id, reader);
                    }

                    if (reader[Constants.TableFields.Cost] != null)
                    {
                        cost = GetIntByColumn(Constants.TableFields.Cost, reader);
                    }

                    if (reader[Constants.TableFields.Weight] != null)
                    {
                        weight = GetIntByColumn(Constants.TableFields.Weight, reader);
                    }

                    if (reader[Constants.TableFields.Name] != null)
                    {
                        name = reader[Constants.TableFields.Name].ToString();
                    }

                    var cat = new Cat
                    {
                        Id = id,
                        Cost = cost,
                        Weight = weight,
                        Name = name,
                    };

                    cats.Add(cat);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
                
            reader.Close();
                
            return cats;
        }

        private int GetIntByColumn(string columnName, SQLiteDataReader sqLiteDataReader)
        {
            var dataFromColumn = sqLiteDataReader[columnName].ToString();
            if (!string.IsNullOrEmpty(dataFromColumn))
            {
                return int.Parse(dataFromColumn);
            }

            return 0;
        }

        public void AddNewCat(string name, int cost, int weight)
        {
            using SQLiteConnection conn = new SQLiteConnection(Constants.Paths.PathToDB);
            conn.Open();

            var columnName = '"' + name + '"';
            var stringQuery = $"{Constants.SqlCommands.InsertInto + Constants.TableNames.Cats}({Constants.TableFields.Cost},{Constants.TableFields.Name},{Constants.TableFields.Weight})Values({cost},{columnName},{weight})";
            
            var executedCommand = new SQLiteCommand(stringQuery, conn).ExecuteReader();
            var createCommandResult = conn.CreateCommand();
            
            createCommandResult.CommandText = stringQuery;
            createCommandResult.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdateCat(int id, string name, int cost, int weight)
        {
            using SQLiteConnection conn = new SQLiteConnection(Constants.Paths.PathToDB);
            conn.Open();

            var columnName = '"' + name + '"';
            
            var stringQuery = $"{Constants.SqlCommands.Update}{Constants.TableNames.Cats} {Constants.SqlCommands.Set} {Constants.TableFields.Name} = {columnName}, {Constants.TableFields.Cost} = {cost}, {Constants.TableFields.Weight} = {weight} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {id};";
            
            var executedCommand = new SQLiteCommand(stringQuery, conn).ExecuteReader();
            var createCommandResult = conn.CreateCommand();
            
            createCommandResult.CommandText = stringQuery;
            createCommandResult.ExecuteNonQuery();
            conn.Close();
        }
    }
}