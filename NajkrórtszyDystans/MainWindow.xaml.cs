using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NajkrórtszyDystans
{
    public partial class MainWindow : Window
    {
        private List<string> cities;
        private int[,] distances;

        public MainWindow()
        {
            InitializeComponent();
            LoadData("distances-pl.txt");
            PopulateComboBoxes();
        }

        private void LoadData(string path)
        {
            var lines = File.ReadAllLines(path, System.Text.Encoding.UTF8);

            cities = lines
                .Select(line => line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)[0])
                .ToList();

            int n = cities.Count;
            distances = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                var tokens = lines[i]
                    .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < n; j++)
                {
                    distances[i, j] = int.Parse(tokens[j + 1]);
                }
            }
        }

        private void PopulateComboBoxes()
        {
            ComboBox1.ItemsSource = cities;
            ComboBox2.ItemsSource = cities;

            if (cities.Count > 1)
            {
                ComboBox1.SelectedIndex = 0;
                ComboBox2.SelectedIndex = 1;
            }
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            int i = ComboBox1.SelectedIndex;
            int j = ComboBox2.SelectedIndex;

            if (i < 0 || j < 0)
            {
                MessageBox.Show("Wybierz oba miasta.", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int d = distances[i, j];
            distance.Content = d.ToString();
        }

        private void button_minimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void button_close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void move(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    } // koniec klasy MainWindow
} // koniec namespace
