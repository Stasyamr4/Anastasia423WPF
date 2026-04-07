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
            if (PhoneText.Text.Contains("+"))
            {
                if (PhoneText.Text.Length == 12)
                {
                    us.Phone = PhoneText.Text;
                }
                else
                {
                    MessageBox.Show("Ошибка! Введите корректный номер телефона!");return;
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
    }
}
