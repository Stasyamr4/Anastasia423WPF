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

namespace Anastasia423WPF.NewFolder1
{
    /// <summary>
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        public List<Product> products = Core.Context.Product.ToList();
        private int? _currentOrderId = null;

        public Products()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            // Загружаем товары из БД
            var products = Core.Context.Product.ToList();
            listBox.ItemsSource = products;
        }

        private void ToBasket_Click(object sender, RoutedEventArgs e)
        {
            // Способ 1: Получаем товар из DataContext кнопки
            var button = sender as Button;
            //var product = button?.DataContext as Product;

            // Способ 2: Получаем из CommandParameter (если использовали CommandParameter="{Binding}")
            // var product = button?.CommandParameter as Product;


            var productId = (int?)button?.Tag;
            var product = Core.Context.Product.FirstOrDefault(p => p.ID == productId);

            if (product != null)
            {
                AddToOrder(product.ID);
            }
        }

        private void AddToOrder(int productId)
        {
            try
            {
                // Создаем новый заказ (корзину), если его еще нет
                if (_currentOrderId == null)
                {
                    var newOrder = new Order
                    {
                        DataOrd = DateTime.Now,
                        Fio = "Временный заказ", // Можно оставить пустым или заполнить позже
                        Email = "temp@example.com",
                        Address = "Не указан"
                    };

                    Core.Context.Order.Add(newOrder);
                    Core.Context.SaveChanges();

                    _currentOrderId = newOrder.ID;

                    // Сохраняем ID заказа в сессии
                    Application.Current.Properties["CurrentOrderId"] = _currentOrderId;
                }

                // Проверяем, есть ли уже этот товар в заказе
                var existingProductOrder = Core.Context.Product_Order
                    .FirstOrDefault(po => po.OrderID == _currentOrderId && po.ProductID == productId);

                if (existingProductOrder != null)
                {
                    MessageBox.Show("Этот товар уже добавлен в корзину!");
                    return;
                }

                // Добавляем связь товара с заказом
                var productOrder = new Product_Order
                {
                    ProductID = productId,
                    OrderID = _currentOrderId.Value
                };

                Core.Context.Product_Order.Add(productOrder);
                Core.Context.SaveChanges();

                // Получаем название товара для сообщения
                var product = Core.Context.Product.FirstOrDefault(p => p.ID == productId);
                if (product != null)
                {
                    MessageBox.Show($"Товар '{product.Name}' добавлен в корзину!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении в корзину: {ex.Message}");
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentOrderId == null)
            {
                MessageBox.Show("Добавьте товары в корзину!");
                return;
            }

            var productCount = Core.Context.Product_Order
                .Count(po => po.OrderID == _currentOrderId);

            if (productCount == 0)
            {
                MessageBox.Show("Добавьте товары в корзину!");
                return;
            }

            // Переход на страницу корзины
            NavigationService.Navigate(new Basket(_currentOrderId));
        }
    }
}

