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
using Anastasia423WPF.Pages;
namespace Anastasia423WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Navigated += MainFrame_Navigated;
        }

        //private void PreviousPage_Click(object sender, RoutedEventArgs e)
        //{
        //    Page currentPage = MainFrame.Content as Page;
        //    if (MainFrame.NavigationService.CanGoBack)
        //    {

        //        MainFrame.NavigationService.GoBack();
        //    }
        //}


        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Меняем заголовок в зависимости от страницы
            if (e.Content is Page page)
            {
                string pageTitle = page.Title;
                if (!string.IsNullOrEmpty(pageTitle))
                {
                    this.Title = $"{pageTitle} - Magical Products Shop";
                }
                else
                {
                    // Если у страницы нет Title, используем имя класса
                    string pageName = page.GetType().Name;
                    this.Title = $"{pageName} - Magical Products Shop";
                }
            }
        }
    }
}
