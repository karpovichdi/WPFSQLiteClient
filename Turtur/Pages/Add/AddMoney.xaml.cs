using System;
using System.Windows;
using Turtur.Models;
using Turtur.Services.Db;
using Turtur.Utills.Helpers;

namespace Turtur.Pages.Add
{
    public partial class AddMoney : Window
    {
        private DbMoneyService _service;

        public AddMoney()
        {
            InitializeComponent();

            _service = new DbMoneyService();
        }

        private void SaveClickHandler(object sender, RoutedEventArgs e)
        {
            var transactionName = transactionNameBox.Text;
            var cost = costBox.Text;

            var costInt = 0;

            if (string.IsNullOrEmpty(transactionName))
            {
                transactionName = "";
            }

            try
            {
                costInt = StringHelper.GetIntFromString(cost);
                if (costInt == -1) throw new Exception();
            }
            catch (Exception exception)
            {
                costBox.Text = "Можно вводить только числа";
                return;
            }

            var money = new Money(0, transactionName, costInt, 0);
            _service.AddNew(money);

            Hide();
        }
    }
}
