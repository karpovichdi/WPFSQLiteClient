using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Turtur.Models;
using Turtur.Utills;
using Turtur.Utills.Helpers.Db;

namespace Turtur.Services.Db
{
    public class DbMoneyService
    {
        public void AddNew(Money money)
        {
            var columnTransactionName = '"' + money.TransactionName + '"';

            var stringQuery = $"{Constants.SqlCommands.InsertInto + Constants.TableNames.Money}({Constants.TableFields.TransactionName},{Constants.TableFields.Cost})Values({columnTransactionName},{money.Cost})";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void UpdateById(Money money)
        {
            var columnTransactionName = '"' + money.TransactionName + '"';

            var stringQuery = $"{Constants.SqlCommands.Update}{Constants.TableNames.Money} {Constants.SqlCommands.Set} {Constants.TableFields.TransactionName} = {columnTransactionName}, {Constants.TableFields.Cost} = {money.Cost} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {money.Id};";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void DeleteById(int id)
        {
            var stringQuery = $"{Constants.SqlCommands.DeleteFrom} {Constants.TableNames.Money} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {id};";
            DbHelper.ExecuteSql(stringQuery);
        }
        
        public List<Money> GetAll()
        {
            var money = new List<Money>();

            try
            {
                using var conn = new SQLiteConnection(Constants.Paths.PathToDb);
                conn.Open();
                
                var reader = new SQLiteCommand(Constants.SqlCommands.SelectAll + Constants.TableNames.Money, conn).ExecuteReader();

                while (reader.Read())
                {
                    int id = 0, cost = 0;
                    string transactionName = string.Empty;

                    if (reader[Constants.TableFields.Id] != null) id = DbHelper.GetIntByColumn(Constants.TableFields.Id, reader);
                    if (reader[Constants.TableFields.Cost] != null) cost = DbHelper.GetIntByColumn(Constants.TableFields.Cost, reader);
                    if (reader[Constants.TableFields.TransactionName] != null) transactionName = reader[Constants.TableFields.TransactionName].ToString();

                    var customer = new Money(id, transactionName, cost);
                    money.Add(customer);
                }
                
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            return money;
        }
    }
}