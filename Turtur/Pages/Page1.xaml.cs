﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Turtur.Models;

namespace Turtur.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();

            List<Car> items = new List<Car>();
            items.Add(new Car(0, "wertwerte"));
            items.Add(new Car(0, "jkjphibuouio"));
            items.Add(new Car(0, "fghjgfhjfjghj"));
            lvUsers.ItemsSource = items;
        }
    }
}
