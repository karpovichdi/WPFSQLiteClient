using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Turtur.Models;
using Turtur.Utills;
using Turtur.Utills.Helpers.Db;

namespace Turtur.Services.Db
{
    public class DbCustomerService
    {
        public void AddNew(Customer customer)
        {
            var columnName = '"' + customer.Name + '"';
            var columnPhone = '"' + customer.Phone + '"';

            var stringQuery = $"{Constants.SqlCommands.InsertInto + Constants.TableNames.Customer}({Constants.TableFields.Phone},{Constants.TableFields.Name})Values({columnPhone},{columnName})";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void UpdateById(Customer customer)
        {
            var columnName = '"' + customer.Name + '"';
            var columnPhone = '"' + customer.Phone + '"';

            var stringQuery = $"{Constants.SqlCommands.Update}{Constants.TableNames.Customer} {Constants.SqlCommands.Set} {Constants.TableFields.Name} = {columnName}, {Constants.TableFields.Phone} = {columnPhone} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {customer.Id};";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void DeleteById(int id)
        {
            var stringQuery = $"{Constants.SqlCommands.DeleteFrom} {Constants.TableNames.Customer} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {id};";
            DbHelper.ExecuteSql(stringQuery);
        }
        
        public List<Customer> GetAll()
        {
            var customers = new List<Customer>();

            try
            {
                using var conn = new SQLiteConnection(Constants.Paths.PathToDb);
                conn.Open();
                
                var reader = new SQLiteCommand(Constants.SqlCommands.SelectAll + Constants.TableNames.Customer, conn).ExecuteReader();

                while (reader.Read())
                {
                    int id = 0;
                    string name = string.Empty, 
                           phone = string.Empty;

                    if (reader[Constants.TableFields.Id] != null) id = DbHelper.GetIntByColumn(Constants.TableFields.Id, reader);
                    if (reader[Constants.TableFields.Phone] != null)  phone = reader[Constants.TableFields.Phone].ToString();
                    if (reader[Constants.TableFields.Name] != null) name = reader[Constants.TableFields.Name].ToString();

                    var customer = new Customer(id, name, phone);

                    customers.Add(customer);
                }
                
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            return customers;
        }
    }
}