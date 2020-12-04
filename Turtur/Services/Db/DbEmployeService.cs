using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Turtur.Models;
using Turtur.Utills;
using Turtur.Utills.Helpers.Db;

namespace Turtur.Services.Db
{
    public class DbEmployeService
    {
        public void AddNew(Employe employe)
        {
            var columnFirstName = '"' + employe.FirstName + '"';
            var columnLastName = '"' + employe.LastName + '"';

            var stringQuery = $"{Constants.SqlCommands.InsertInto + Constants.TableNames.Employee}({Constants.TableFields.FirstName},{Constants.TableFields.LastName})Values({columnFirstName},{columnLastName})";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void UpdateById(Employe employe)
        {
            var columnFirstName = '"' + employe.FirstName + '"';
            var columnLastName = '"' + employe.LastName + '"';

            var stringQuery = $"{Constants.SqlCommands.Update}{Constants.TableNames.Employee} {Constants.SqlCommands.Set} {Constants.TableFields.FirstName} = {columnFirstName}, {Constants.TableFields.LastName} = {columnLastName} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {employe.Id};";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void DeleteById(int id)
        {
            var stringQuery = $"{Constants.SqlCommands.DeleteFrom} {Constants.TableNames.Employee} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {id};";
            DbHelper.ExecuteSql(stringQuery);
        }
        
        public List<Employe> GetAll()
        {
            var employee = new List<Employe>();

            try
            {
                using var conn = new SQLiteConnection(Constants.Paths.PathToDb);
                conn.Open();
                
                var reader = new SQLiteCommand(Constants.SqlCommands.SelectAll + Constants.TableNames.Employee, conn).ExecuteReader();

                while (reader.Read())
                {
                    var id = 0;
                    string firstName = string.Empty, 
                           lastName = string.Empty;

                    if (reader[Constants.TableFields.Id] != null) id = DbHelper.GetIntByColumn(Constants.TableFields.Id, reader);
                    if (reader[Constants.TableFields.FirstName] != null)  lastName = reader[Constants.TableFields.FirstName].ToString();
                    if (reader[Constants.TableFields.LastName] != null) firstName = reader[Constants.TableFields.LastName].ToString();

                    var customer = new Employe(id, firstName, lastName);
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