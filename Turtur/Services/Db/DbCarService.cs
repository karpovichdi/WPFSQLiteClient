﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Turtur.Models;
using Turtur.Utills;
using Turtur.Utills.Helpers.Db;

namespace Turtur.Services.Db
{
    public class DbCarService
    {
        public void AddNew(Car car)
        {
            var columnName = '"' + car.Name + '"';
            var stringQuery = $"{Constants.SqlCommands.InsertInto + Constants.TableNames.Cars}({Constants.TableFields.Name})Values({columnName})";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void UpdateById(Car car)
        {
            var columnName = '"' + car.Name + '"';
            var stringQuery = $"{Constants.SqlCommands.Update}{Constants.TableNames.Cars} {Constants.SqlCommands.Set} {Constants.TableFields.Name} = {columnName} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {car.Id};";
            DbHelper.ExecuteSql(stringQuery);
        }

        public void DeleteById(int id)
        {
            var stringQuery = $"{Constants.SqlCommands.DeleteFrom} {Constants.TableNames.Cars} {Constants.SqlCommands.Where} {Constants.TableFields.Id} = {id};";
            DbHelper.ExecuteSql(stringQuery);
        }
        
        public List<Car> GetAll()
        {
            var cars = new List<Car>();

            try
            {
                using var conn = new SQLiteConnection(Constants.Paths.PathToDb);
                conn.Open();
                
                var reader = new SQLiteCommand(Constants.SqlCommands.SelectAll + Constants.TableNames.Cars, conn).ExecuteReader();

                while (reader.Read())
                {
                    var id = 0;
                    var name = string.Empty;

                    if (reader[Constants.TableFields.Id] != null) id = DbHelper.GetIntByColumn(Constants.TableFields.Id, reader);
                    if (reader[Constants.TableFields.Name] != null) name = reader[Constants.TableFields.Name].ToString();

                    var cat = new Car(id, name);

                    cars.Add(cat);
                }
                
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return cars;
        }
    }
}