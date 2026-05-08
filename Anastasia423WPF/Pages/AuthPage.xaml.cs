using System;
using System.Collections.Generic;
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

namespace Anastasia423WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public user_ user { get; set; }
        public AuthPage()
        {
            InitializeComponent();
        }

        public AuthPage(user_ us) : this()
        {
            if (us != null)
            {
                user = us;
                LoginText.Text += user.Login;
                PassText.Password += user.Password;
            }
        }

        private void Registr_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (AuthUser(LoginText.Text, PassText.Password))
            {
                if (user.IsFreeze == true)
                {
                    MessageBox.Show("Ваш аккаунт заблокирован администратором.", "Ошибка доступа");
                    return; // Прерываем вход
                }
                // Если статус нормальный, то пускаем дальше...
                MessageBox.Show("Успешный вход!", "Успешный вход", MessageBoxButton.OK, MessageBoxImage.Information);
                if (user.RoleID == 1) // Пользователь
                {
                    NavigationService.Navigate(new CatalogPage(user));
                }
                else if (user.RoleID == 2) // Автор
                {
                    //NavigationService.Navigate(new AuthorPage(user));
                }
                else if (user.RoleID == 3)// Админ
                {
                    //NavigationService.Navigate(new AdminPage(user));
                }

            }
            else
            {
                MessageBox.Show("Авторизация не удалась! Проверьте праваильность логина и пароя!", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool AuthUser(string login, string password)
        {
            var UsInDB = Core.Context.user_.Where(u => u.Login == login).FirstOrDefault();
            if (UsInDB != null)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    if (password == UsInDB.Password)
                    {
                        user = UsInDB;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Введен неверный пароль!", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Введен неверный пароль!", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Ошибка! Пользователь не найден в базе данных!", "Не найден в базе данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}