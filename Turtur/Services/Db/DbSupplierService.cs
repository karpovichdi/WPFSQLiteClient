using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Turtur.Models;
using Turtur.Utills;
using Turtur.Utills.Helpers.Db;

namespace Turtur.Services.Db
{
    public class DbSupplierService
    {
        public void AddNew(Supplier supplier)
        {
            var name = '"' + supplier.Name + '"';
            var category = '"' + supplier.Category + '"';
            
            var stringQuery = $"{Constants.SqlCommands.InsertInto + Constants.TableNames.Suppliers}({Constants.TableFields.Name},{Constants.TableFields.Category})Values({name},{category})";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void UpdateById(Supplier supplier)
        {
            var name = '"' + supplier.Name + '"';
            var category = '"' + supplier.Category + '"';

            var stringQuery = $"{Constants.SqlCommands.Update}{Constants.TableNames.Suppliers} {Constants.SqlCommands.Set} {Constants.TableFields.Name} = {name}, {Constants.TableFields.Category} = {category} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {supplier.Id};";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void DeleteById(int id)
        {
            var stringQuery = $"{Constants.SqlCommands.DeleteFrom} {Constants.TableNames.Suppliers} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {id};";
            DbHelper.ExecuteSql(stringQuery);
        }
        
        public List<Supplier> GetAll()
        {
            var resources = new List<Supplier>();

            try
            {
                using var conn = new SQLiteConnection(Constants.Paths.PathToDb);
                conn.Open();
                
                var reader = new SQLiteCommand(Constants.SqlCommands.SelectAll + Constants.TableNames.Suppliers, conn).ExecuteReader();

                while (reader.Read())
                {
                    int id = 0;
                    string name = string.Empty, category = string.Empty;

                    if (reader[Constants.TableFields.Id] != null) id = DbHelper.GetIntByColumn(Constants.TableFields.Id, reader);
                    if (reader[Constants.TableFields.Name] != null) name = reader[Constants.TableFields.Name].ToString();
                    if (reader[Constants.TableFields.Category] != null) category = reader[Constants.TableFields.Category].ToString();

                    var sale = new Supplier(id, name, category);
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