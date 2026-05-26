using System;
using System.Linq;
using System.Windows;
using WpfLocalDbDemo.Data;
using WpfLocalDbDemo.Models;

namespace WpfLocalDbDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (var context = new AppDbContext())
            {
                dgUsers.ItemsSource = context.Users.ToList();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите имя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtAge.Text, out int age) || age < 0 || age > 150)
            {
                MessageBox.Show("Введите корректный возраст (число от 0 до 150).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newUser = new User { Name = txtName.Text.Trim(), Age = age };

            using (var context = new AppDbContext())
            {
                context.Users.Add(newUser);
                context.SaveChanges();
            }

            txtName.Clear();
            txtAge.Clear();
            LoadUsers();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgUsers.SelectedItem as User;
            if (selected == null)
            {
                MessageBox.Show("Выберите пользователя для удаления.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (MessageBox.Show($"Удалить пользователя '{selected.Name}'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var context = new AppDbContext())
                {
                    var userToDelete = context.Users.Find(selected.Id);
                    if (userToDelete != null)
                    {
                        context.Users.Remove(userToDelete);
                        context.SaveChanges();
                    }
                }
                LoadUsers();
            }
        }
    }
}
