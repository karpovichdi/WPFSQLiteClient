using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Turtur.Models;
using Turtur.Utills;
using Turtur.Utills.Helpers.Db;

namespace Turtur.Services.Db
{
    public class DbSaleService
    {
        private DbMoneyService _dbMoneyService;
        private DbCatService _dbCatService;

        public DbSaleService() 
        {
            _dbMoneyService = new DbMoneyService();
            _dbCatService = new DbCatService();
        }

        public void AddNew(Sale sale)
        {
            var date = '"' + sale.Date + '"';

            var stringQuery = $"{Constants.SqlCommands.InsertInto + Constants.TableNames.Sales}({Constants.TableFields.Date},{Constants.TableFields.Customer},{Constants.TableFields.Cat})Values({date},{sale.Customer},{sale.Cat})";
            DbHelper.ExecuteSql(stringQuery);

            try
            {
                var cats = _dbCatService.GetAll();
                var cat = cats.Find((x) => x.Id == sale.Cat);

                var cost = 0;
                if (cat != null) cost = cat.COST;

                _dbMoneyService.AddNew(new Money(0, "SALE: " + sale.Date + " " + cat?.Name, cost, sale.Id));
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
            }            
        }

        public void UpdateById(Sale sale)
        {
            var date = '"' + sale.Date + '"';

            var stringQuery = $"{Constants.SqlCommands.Update}{Constants.TableNames.Sales} {Constants.SqlCommands.Set} {Constants.TableFields.Date} = {date}, {Constants.TableFields.Customer} = {sale.Customer}, {Constants.TableFields.Cat} = {sale.Cat} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {sale.Id};";
            DbHelper.ExecuteSql(stringQuery);

            try
            {
                var moneys = _dbMoneyService.GetAll();
                var money = moneys.Find((x) => x.Sale == sale.Id);

                var cats = _dbCatService.GetAll();
                var cat = cats.Find((x) => x.Id == sale.Cat);

                var cost = 0;
                if (cat != null) cost = cat.COST;

                _dbMoneyService.UpdateById(new Money(money.Id, "SALE: " + sale.Date + " " + cat?.Name, cost, sale.Id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DeleteById(int id)
        {
            try
            {
                var moneys = _dbMoneyService.GetAll();
                var money = moneys.Find((x) => x.Sale == id);

                _dbMoneyService.DeleteById(money.Id);

                var stringQuery = $"{Constants.SqlCommands.DeleteFrom} {Constants.TableNames.Sales} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {id};";
                DbHelper.ExecuteSql(stringQuery);
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
            }
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