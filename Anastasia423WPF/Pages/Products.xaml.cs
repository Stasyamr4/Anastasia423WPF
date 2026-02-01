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
            RestoreCurrentOrder();
        }

        // Дополнительный конструктор с параметром (для передачи из других страниц)
        public Products(int? orderId) : this() // Вызываем основной конструктор
        {
            _currentOrderId = orderId;
        }

        private void RestoreCurrentOrder()
        {
            // Ищем неоформленный заказ (с временными данными)
            var tempOrder = Core.Context.Order
                .Where(o => o.Fio == "Временный заказ" || o.Email == "temp@example.com")
                .OrderByDescending(o => o.DataOrd)
                .FirstOrDefault();

            if (tempOrder != null)
            {
                // Проверяем, есть ли в нем товары
                var hasItems = Core.Context.Product_Order
                    .Any(po => po.OrderID == tempOrder.ID);

                if (hasItems)
                {
                    _currentOrderId = tempOrder.ID;
                }
            }
        }

        private void LoadProducts()
        {
            // Загружаем товары из БД
            var products = Core.Context.Product.ToList();
            listBox.ItemsSource = products;
        }

        private void ToBasket_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var product = button?.DataContext as Product;

            if (product != null)
            {
                AddToOrder(product.ID);
            }
        }

        private void AddToOrder(int productId)
        {
            try
            {
                // Создаем новый заказ, если его еще нет
                if (_currentOrderId == null)
                {
                    var newOrder = new Order
                    {
                        DataOrd = DateTime.Now,
                        Fio = "Временный заказ",
                        Email = "temp@example.com",
                        Address = "Не указан"
                    };

                    Core.Context.Order.Add(newOrder);
                    Core.Context.SaveChanges();

                    _currentOrderId = newOrder.ID;
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

