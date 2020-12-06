using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using Turtur.Models;
using Turtur.Services.Db;
using Turtur.Utills.Helpers;

namespace Turtur.Pages.Add
{
    public partial class AddCat : Window
    {
        private DbCatService _service;

        public AddCat()
        {
            InitializeComponent();

            _service = new DbCatService();
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
            var weight = weightBox.Text;
            var cost = costBox.Text;

            var weightInt = 0;
            var costInt = 0;

            if (string.IsNullOrEmpty(photo)) 
            {
                photo = "";
            }

            if (string.IsNullOrEmpty(name))
            {
                nameBox.Text = "Вы должны ввести имя и породу";
                return;
            }

            try
            {
                weightInt = StringHelper.GetIntFromString(weight);
                if (weightInt == -1) throw new Exception();
            }
            catch (Exception exception) 
            {
                weightBox.Text = "Можно вводить только числа";
                return;
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

            var cat = new Cat(0, photo, weightInt, costInt, name);
            _service.AddNew(cat);

            Hide();
        }
    }
}