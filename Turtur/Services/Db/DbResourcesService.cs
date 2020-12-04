using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Turtur.Models;
using Turtur.Utills;
using Turtur.Utills.Helpers.Db;

namespace Turtur.Services.Db
{
    public class DbResourcesService
    {
        public void AddNew(Resource resource)
        {
            var name = '"' + resource.Name + '"';

            var stringQuery = $"{Constants.SqlCommands.InsertInto + Constants.TableNames.Resources}({Constants.TableFields.Name},{Constants.TableFields.Supplier})Values({name},{resource.Supplier})";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void UpdateById(Resource resource)
        {
            var name = '"' + resource.Name + '"';

            var stringQuery = $"{Constants.SqlCommands.Update}{Constants.TableNames.Resources} {Constants.SqlCommands.Set} {Constants.TableFields.Name} = {name}, {Constants.TableFields.Supplier} = {resource.Supplier} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {resource.Id};";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void DeleteById(int id)
        {
            var stringQuery = $"{Constants.SqlCommands.DeleteFrom} {Constants.TableNames.Resources} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {id};";
            DbHelper.ExecuteSql(stringQuery);
        }
        
        public List<Resource> GetAll()
        {
            var resources = new List<Resource>();

            try
            {
                using var conn = new SQLiteConnection(Constants.Paths.PathToDb);
                conn.Open();
                
                var reader = new SQLiteCommand(Constants.SqlCommands.SelectAll + Constants.TableNames.Resources, conn).ExecuteReader();

                while (reader.Read())
                {
                    int id = 0, supplier = 0;
                    string name = string.Empty;

                    if (reader[Constants.TableFields.Id] != null) id = DbHelper.GetIntByColumn(Constants.TableFields.Id, reader);
                    if (reader[Constants.TableFields.Supplier] != null) supplier = DbHelper.GetIntByColumn(Constants.TableFields.Supplier, reader);
                    if (reader[Constants.TableFields.Name] != null) name = reader[Constants.TableFields.Name].ToString();

                    var customer = new Resource(id, name, supplier);
                    resources.Add(customer);
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