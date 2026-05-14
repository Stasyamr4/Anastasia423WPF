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
using Anastasia423WPF;
using Anastasia423WPF.Windows;

namespace Anastasia423WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorPage.xaml
    /// </summary>
    public partial class AuthorPage : Page
    {
        private user_ _currentUser;
        public AuthorPage(user_ user)
        {
            InitializeComponent();
            _currentUser = user;
            UpdateData();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }
        private void UpdateData()
        {
            try
            {
                Core.Context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                LBoxAuthorBooks.ItemsSource = Core.Context.book.Where(b => b.AuthorID == _currentUser.ID).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
            }
        }

        private void LBoxAuthorBooks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedBook = LBoxAuthorBooks.SelectedItem as book;
            if (selectedBook != null)
            {
                BookEditWindow editWin = new BookEditWindow(selectedBook, _currentUser);
                if (editWin.ShowDialog() == true)
                {
                    UpdateData();
                }
            }
        }

        private void BtnAddBook_Click(object sender, RoutedEventArgs e)
        {
            BookEditWindow addWin = new BookEditWindow(null, _currentUser);
            if (addWin.ShowDialog() == true)
            {
                UpdateData();
            }
        }
        private void BtnAppeal_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = (sender as Button).Tag as book;
            if (selectedBook == null) return;

            string reason = $"Ваше произведение '{selectedBook.Name}' было заморожено. Для подробной информации обратитесь к модератору";
            //FreezeAppealWindow appealWin = new FreezeAppealWindow(Core.CurrentUser, reason, selectedBook.ID);
            //appealWin.ShowDialog();
        }
    }
}