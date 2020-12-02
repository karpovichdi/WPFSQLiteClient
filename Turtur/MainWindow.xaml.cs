using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Turtur
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            using(SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\Karpo\Desktop\Turtur\Turtur\cats.db;"))
            {
                conn.Open();
            
                var tableName = "cats";

                var commandString = "Select * from cats";
                
                SQLiteCommand command = new SQLiteCommand(commandString, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                    Console.WriteLine(reader["Name"]);
            
            
                reader.Close();
            }
        }
    }
}
