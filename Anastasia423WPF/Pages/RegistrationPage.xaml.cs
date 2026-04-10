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

        

        private void PhoneText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(PhoneText.Text))
            {
                if (PhoneText.Text.Contains("+"))
                {
                    if (PhoneText.Text.Length == 12)
                    {
                        us.Phone = PhoneText.Text;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка! Введите корректный номер телефона!"); return;
                    }
                }
                else if (!PhoneText.Text.Contains("+"))
                {
                    if (PhoneText.Text.Length == 11)
                    {
                        us.Phone = PhoneText.Text;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка! Введите корректный номер телефона!"); return;
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка! Введите корректный номер телефона!"); return;
                }
            }
            else
            {
                MessageBox.Show("Ошибка! Заполните поле!"); return;
            }
        }

        private void LoginText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LoginText.Text))
            {
                us.Login = LoginText.Text;
            }
            else
            {
                MessageBox.Show("Ошибка! Заполните поле!", "ошибка логина", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void PassText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PassText.Password))
            {
                PasswordVer(PassText.Password, PassVerificText.Password);
            }
            else
            {
                MessageBox.Show("Ошибка! Заполните поле!", "ошибка пароля", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void PassVerificText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PassVerificText.Password))
            {
                PasswordVer(PassText.Password, PassVerificText.Password);
            }
            else
            {
                MessageBox.Show("Ошибка! Заполните поле!", "ошибка пароля", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void FIOText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(FIOText.Text))
            {
                us.FIO = FIOText.Text;
            }
            else
            {
                MessageBox.Show("Ошибка! Заполните поле!", "ошибка ФИО", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool PasswordVer (string password, string password2)
        {
            if(password != null && password2 != null) 
            {
                if(password == password2)
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
            var usInDB = Core.Context.User.Where(u => u.Login == us.Login).FirstOrDefault();
            if (usInDB == null)
            {
                Core.Context.User.Add(us);
                Core.Context.SaveChanges();
                NavigationService.Navigate(new Catalog(us));
            }
            else
            {
                var answ = MessageBox.Show("Ошибка! Пользователь уже зарегистрирован! Желаете войти?", "Повторная регистрация", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (answ == MessageBoxResult.Yes)
                {
                    NavigationService.Navigate(new AuthPage(us));
                }
                else return;
            }
        }
    }
}
