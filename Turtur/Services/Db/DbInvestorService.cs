using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Turtur.Models;
using Turtur.Utills;
using Turtur.Utills.Helpers.Db;

namespace Turtur.Services.Db
{
    public class DbInvestorService
    {
        public void AddNew(Investor investor)
        {
            var columnName = '"' + investor.Name + '"';

            var stringQuery = $"{Constants.SqlCommands.InsertInto + Constants.TableNames.Investors}({Constants.TableFields.Name})Values({columnName})";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void UpdateById(Investor investor)
        {
            var columnName = '"' + investor.Name + '"';

            var stringQuery = $"{Constants.SqlCommands.Update}{Constants.TableNames.Investors} {Constants.SqlCommands.Set} {Constants.TableFields.Name} = {columnName} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {investor.Id};";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void DeleteById(int id)
        {
            var stringQuery = $"{Constants.SqlCommands.DeleteFrom} {Constants.TableNames.Investors} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {id};";
            DbHelper.ExecuteSql(stringQuery);
        }
        
        public List<Investor> GetAll()
        {
            var employee = new List<Investor>();

            try
            {
                using var conn = new SQLiteConnection(Constants.Paths.PathToDb);
                conn.Open();
                
                var reader = new SQLiteCommand(Constants.SqlCommands.SelectAll + Constants.TableNames.Investors, conn).ExecuteReader();

                while (reader.Read())
                {
                    var id = 0;
                    var name = string.Empty;

                    if (reader[Constants.TableFields.Id] != null) id = DbHelper.GetIntByColumn(Constants.TableFields.Id, reader);
                    if (reader[Constants.TableFields.Name] != null) name = reader[Constants.TableFields.Name].ToString();

                    var customer = new Investor(id, name);
                    employee.Add(customer);
                }
                
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            return employee;
        }
    }
}