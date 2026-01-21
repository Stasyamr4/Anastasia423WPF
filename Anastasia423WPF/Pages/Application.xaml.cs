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
    /// Логика взаимодействия для Application.xaml
    /// </summary>
    public partial class Application : Page
    {
        public Application()
        {
            InitializeComponent();
        }
        bool active = false;
        public void CheckValid(object send)
        {
            bool Name = false;
            bool usPhone = false;
            bool usMail = false;
            if (UserName.Text.Length < 1)
            {
                MessageBox.Show("Введите корректное имя");
            }
            else
            {
                Name = true;
            }
            if ((UserPhone.Text.Length > 12) || (UserPhone.Text.Length < 11))
            {
                MessageBox.Show("Введите корректный номер телефона");
            }
            else
            {
                usPhone = true;
            }
            if (!UserEmail.Text.Contains("@"))
            {
                MessageBox.Show("Введите корректную электронную почту");
            }
            else
            {
                usMail = true;
            }
            if (Name && usPhone && usMail)
            {
                active = true;
            }
        }

        private void UserName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.All(char.IsDigit))
            {
                e.Handled = true;
            }
        }

        private void UserPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!e.Text.All(char.IsDigit))
            {
                e.Handled = true;
            }
        }

        private void User_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckValid(sender);
            if (active == true)
            {
                ApplicationBut.IsEnabled = true;
            }
        }

        private void ApplicationBut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Заявка успешно отправлена! С вами скоро свяжутся!");
        }
        //private void UserName_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    UserName.Text = "";
        //}

        //private void UserPhone_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    UserPhone.Text = "";
        //}

        //private void UserEmail_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    UserEmail.Text = "";
        //}
    }
}
