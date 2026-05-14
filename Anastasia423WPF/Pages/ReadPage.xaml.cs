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
using Anastasia423WPF.Windows;

namespace Anastasia423WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReadPage.xaml
    /// </summary>
    public partial class ReadPage : Page
    {
        private book _currentBook;
        private user_ _currentUser;

        public ReadPage(int bookId, user_ user)
        {
            InitializeComponent();
            _currentUser = user;
            _currentBook = Core.Context.book.FirstOrDefault(b => b.ID == bookId);

            if (_currentBook != null)
            {
                TBlockBookName.Text = _currentBook.Name;
                TBlockContent.Text = $"Вы начали чтение книги: {_currentBook.Name}. \n\nОписание: {_currentBook.Description}\n\nТЕКСТ: {_currentBook.Text}";
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();

        private void OpenReviewWindow_Click(object sender, RoutedEventArgs e)
        {
            var reviewWin = new ReviewWindow(_currentBook.ID, _currentUser.ID);
            reviewWin.Owner = Window.GetWindow(this);

            if (reviewWin.ShowDialog() == true)
            {
            }
        }
    }
}
