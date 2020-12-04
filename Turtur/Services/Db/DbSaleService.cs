﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Turtur.Models;
using Turtur.Utills;
using Turtur.Utills.Helpers.Db;

namespace Turtur.Services.Db
{
    public class DbSaleService
    {
        public void AddNew(Sale sale)
        {
            var date = '"' + sale.Date + '"';

            var stringQuery = $"{Constants.SqlCommands.InsertInto + Constants.TableNames.Sales}({Constants.TableFields.Date},{Constants.TableFields.Customer},{Constants.TableFields.Cat})Values({date},{sale.Customer},{sale.Cat})";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void UpdateById(Sale sale)
        {
            var date = '"' + sale.Date + '"';

            var stringQuery = $"{Constants.SqlCommands.Update}{Constants.TableNames.Sales} {Constants.SqlCommands.Set} {Constants.TableFields.Date} = {date}, {Constants.TableFields.Customer} = {sale.Customer}, {Constants.TableFields.Cat} = {sale.Cat} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {sale.Id};";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void DeleteById(int id)
        {
            var stringQuery = $"{Constants.SqlCommands.DeleteFrom} {Constants.TableNames.Sales} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {id};";
            DbHelper.ExecuteSql(stringQuery);
        }
        
        public List<Sale> GetAll()
        {
            var resources = new List<Sale>();

            try
            {
                using var conn = new SQLiteConnection(Constants.Paths.PathToDb);
                conn.Open();
                
                var reader = new SQLiteCommand(Constants.SqlCommands.SelectAll + Constants.TableNames.Sales, conn).ExecuteReader();

                while (reader.Read())
                {
                    int id = 0, customer = 0, cat = 0;
                    string date = string.Empty;

                    if (reader[Constants.TableFields.Id] != null) id = DbHelper.GetIntByColumn(Constants.TableFields.Id, reader);
                    if (reader[Constants.TableFields.Customer] != null) customer = DbHelper.GetIntByColumn(Constants.TableFields.Customer, reader);
                    if (reader[Constants.TableFields.Cat] != null) cat = DbHelper.GetIntByColumn(Constants.TableFields.Cat, reader);
                    if (reader[Constants.TableFields.Date] != null) date = reader[Constants.TableFields.Date].ToString();

                    var sale = new Sale(id, date, customer, cat);
                    resources.Add(sale);
                }
                
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            return resources;
        }
    }
}