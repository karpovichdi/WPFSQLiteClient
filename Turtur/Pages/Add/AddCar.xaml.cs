using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Turtur.Models;
using Turtur.Services.Db;

namespace Turtur.Pages.Add
{
    public partial class AddCar : Window
    {
        private DbCarService _service;

        public AddCar()
        {
            InitializeComponent();

            _service = new DbCarService();
        }

        private void ShowButtonClickHandler(object sender, RoutedEventArgs e)
        {
            try
            {
                var text = photoLinkBox.Text;
                if (string.IsNullOrEmpty(text))
                {
                    photoLinkBox.Text = "Вы должны добавить сюда ссылку на фото кота";
                }
                else
                {
                    photo.Source = new BitmapImage(new Uri(text));
                }
            }
            catch (Exception exception)
            {
                photo.Source = null;
                Console.WriteLine(exception);
            }
        }

        private void SaveClickHandler(object sender, RoutedEventArgs e)
        {
            var photo = photoLinkBox.Text;
            var name = nameBox.Text;
            var stateNumber = stateNumberBox.Text;

            if (string.IsNullOrEmpty(photo))
            {
                photo = "";
            }

            if (string.IsNullOrEmpty(name))
            {
                nameBox.Text = "Вы должны ввести имя и породу";
                return;
            }

            if (string.IsNullOrEmpty(stateNumber))
            {
                nameBox.Text = "Вы должны ввести гос номер";
                return;
            }

            var car = new Car(0, name, photo, stateNumber);
            _service.AddNew(car);

            Hide();
        }
    }
}