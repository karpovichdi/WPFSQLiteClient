using System.Windows;
using Turtur.Models;
using Turtur.Services.Db;

namespace Turtur.Pages.Add
{
    public partial class AddInvestor : Window
    {
        private DbInvestorService _investorService;

        public AddInvestor()
        {
            InitializeComponent();

            _investorService = new DbInvestorService();
        }

        private void SaveClickHandler(object sender, RoutedEventArgs e)
        {
            var name = nameBox.Text;

            if (string.IsNullOrEmpty(name))
            {
                nameBox.Text = "Вы должны ввести название организации";
                return;
            }

            var investor = new Investor(0, name);
            _investorService.AddNew(investor);

            Hide();
        }
    }
}