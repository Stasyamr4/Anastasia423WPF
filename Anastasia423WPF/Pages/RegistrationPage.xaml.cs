using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        User us = new User();
        public RegistrationPage()
        {
            InitializeComponent();
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        public bool PasswordVer(string password, string password2)
        {
            if (password != null && password2 != null)
            {
                if (password == password2)
                {
                    us.Password = password;
                    return true;
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают!", "Ошибка соответствия паролей", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают!");
                return false;
            }
        }

        private void RegistrUser_Click(object sender, RoutedEventArgs e)
        {
            if (RegistrationUser(LoginText.Text, PassText.Password))
            {
                MessageBox.Show("Успешная регистрация!");
                NavigationService.Navigate(new Catalog(us));
            }
            else
            {
                MessageBox.Show("Регистрация не удалась!", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool RegistrationUser(string login, string password)
        {
            var usInDB = Core.Context.User.Where(u => u.Login == login).FirstOrDefault();
            if (usInDB == null)
            {
                if (CheckFields(login, password, PassVerificText.Password, FIOText.Text, PhoneText.Text) && (PasswordVer(password, PassVerificText.Password)))
                {
                    Core.Context.User.Add(us);
                    Core.Context.SaveChanges();
                    MessageBox.Show("Пользователь успешно зарегистрирован!", "Успешная регистрация");
                    return true;
                }

                else return false;
                }
            else
            {
                var answ = MessageBox.Show("Ошибка! Пользователь уже зарегистрирован! Желаете войти?", "Повторная регистрация", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (answ == MessageBoxResult.Yes)
                {
                    NavigationService.Navigate(new AuthPage(us));
                    return true;
                }
                else return false;
            }
        }

        private bool CheckFields(string login, string password, string passwordConfirm, string FIO, string phoneNum)
        {
            while (true)
            {
                // Проверка login
                if (string.IsNullOrEmpty(login))
                {
                    MessageBox.Show("Логин должен быть указан!", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // Проверка пароля
                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Пароль должен быть указан!", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // Проверка пароля подтвенржденного
                if (string.IsNullOrEmpty(passwordConfirm))
                {
                    MessageBox.Show("Пароль должен быть указан!", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // Проверка ФИО
                if (string.IsNullOrEmpty(FIO))
                {
                    MessageBox.Show("Поле ФИО должно быть заполнено!", "Некорректный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                // Проверка телефона
                if (string.IsNullOrEmpty(phoneNum))
                {
                    MessageBox.Show("Номер телефона не может быть пустым!", "Некорректный ввод",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // Нормализация номера (если есть +, то должно быть 12 символов, иначе 11)
                string cleanPhone = phoneNum.Trim();
                bool isPhoneValid = (cleanPhone.StartsWith("+") && cleanPhone.Length == 12) ||
                                    (!cleanPhone.StartsWith("+") && cleanPhone.Length == 11);

                if (!isPhoneValid)
                {
                    MessageBox.Show("Номер телефона должен содержать 11 цифр или 12 с '+' в начале!", "Некорректный ввод",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                // Все проверки пройдены – сохраняем данные в объект пользователя

                us.Login = login;
                us.FIO = FIO;
                us.Phone = phoneNum;

                return true;
            }
        }
    }
}
