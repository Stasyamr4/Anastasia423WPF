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
using System.Windows.Shapes;

namespace Anastasia423WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для AppealFreezeWindow.xaml
    /// </summary>
    public partial class AppealFreezeWindow : Window
    {
        private user_ _user;
        private int? _bookId;
        public Visibility BookTitleVisibility => _bookId != null ? Visibility.Visible : Visibility.Collapsed;
        public Visibility AccountTitleVisibility => _bookId == null ? Visibility.Visible : Visibility.Collapsed;
        public AppealFreezeWindow(user_ user, string reason, int? bookId = null)
        {
            InitializeComponent();
            _user = user;
            _bookId = bookId;
            TBlockReason.Text = reason;
            if (_bookId != null)
                this.Title = "Оспорить заморозку книги";
            else
                this.Title = "Оспорить заморозку аккаунта";
            this.DataContext = this;
        }
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBoxAppeal.Text))
            {
                MessageBox.Show("Напишите текст апелляции");
                return;
            }
            var request = new requestUnFreeze
            {
                userID = _user.ID,
                bookID = _bookId, //null - значит аккаунт, если число - значит книга
                requestText = TBoxAppeal.Text,
                reportDate = DateTime.Now
            };
            Core.Context.requestUnFreeze.Add(request);
            Core.Context.SaveChanges();
            MessageBox.Show("Ваше обращение отправлено модераторам.");
            this.Close();
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
