using System.Windows;
using Turtur.Models;
using Turtur.Services.Db;

namespace Turtur.Pages.Add
{
    public partial class AddEmploye : Window
    {
        private DbEmployeService _employeService;

        public AddEmploye()
        {
            InitializeComponent();

            _employeService = new DbEmployeService();
        }

        private void SaveClickHandler(object sender, RoutedEventArgs e)
        {
            var firstName = firstNameBox.Text;
            var lastName = lastNameBox.Text;

            if (string.IsNullOrEmpty(lastName))
            {
                lastNameBox.Text = "Вы должны ввести фамилию";
                return;
            }

            if (string.IsNullOrEmpty(firstName))
            {
                firstNameBox.Text = "Вы должны ввести имя";
                return;
            }

            var employe = new Employe(0, firstName, lastName);
            _employeService.AddNew(employe);

            Hide();
        }
    }
}