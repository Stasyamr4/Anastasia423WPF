using Anastasia423WPF.NewFolder1;
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
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Page
    {
        private int? _orderId;
        private List<Product> _basketProducts = new List<Product>();
        public Basket(int? orderId = null)
        {
            InitializeComponent();
            _orderId = orderId;
            LoadBasket();
        }

        private void LoadBasket()
        {
            try
            {
                if (_orderId == null)
                {
                    // Пытаемся найти неоформленный заказ
                    var tempOrder = Core.Context.Order
                        .Where(o => o.Fio == "Временный заказ" || o.Email == "temp@example.com")
                        .OrderByDescending(o => o.DataOrd)
                        .FirstOrDefault();

                    if (tempOrder != null)
                    {
                        _orderId = tempOrder.ID;
                    }
                }

                if (_orderId != null)
                {
                    // Получаем товары из корзины через Product_Order
                    var productIds = Core.Context.Product_Order
                        .Where(po => po.OrderID == _orderId)
                        .Select(po => po.ProductID)
                        .ToList();

                    _basketProducts = Core.Context.Product
                        .Where(p => productIds.Contains(p.ID))
                        .ToList();

                    // Отображаем товары
                    ItemsControlBasket.ItemsSource = _basketProducts;

                    // Считаем общую сумму
                    decimal total = _basketProducts.Sum(p => p.Price);
                    txtTotalPrice.Text = $"{total:C}";

                    // Активируем кнопку оформить заказ, если есть товары
                    BtnCheckout.IsEnabled = _basketProducts.Count > 0;

                    // Меняем текст кнопки в зависимости от наличия товаров
                    if (_basketProducts.Count == 0)
                    {
                        txtTotalPrice.Text = "0 руб.";
                        BtnCheckout.Content = "Корзина пуста";
                    }
                }
                else
                {
                    // Корзина пуста
                    ItemsControlBasket.ItemsSource = null;
                    txtTotalPrice.Text = "0 руб.";
                    BtnCheckout.IsEnabled = false;
                    BtnCheckout.Content = "Корзина пуста";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке корзины: {ex.Message}");
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            // Возвращаемся на страницу товаров
            NavigationService.Navigate(new Products(_orderId));
        }

        private void BtnCheckout_Click(object sender, RoutedEventArgs e)
        {
            if (_orderId == null || _basketProducts.Count == 0)
            {
                MessageBox.Show("Корзина пуста!");
                return;
            }

            // Переходим на страницу оформления заказа
            NavigationService.Navigate(new OrderPage(_orderId.Value));
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is int productId && _orderId != null)
            {
                try
                {
                    // Находим и удаляем связь Product_Order
                    var productOrder = Core.Context.Product_Order
                        .FirstOrDefault(po => po.OrderID == _orderId && po.ProductID == productId);

                    if (productOrder != null)
                    {
                        // Получаем название товара для сообщения
                        var product = _basketProducts.FirstOrDefault(p => p.ID == productId);
                        string productName = product?.Name ?? "товар";

                        Core.Context.Product_Order.Remove(productOrder);
                        Core.Context.SaveChanges();

                        // Перезагружаем корзину
                        LoadBasket();

                        MessageBox.Show($"Товар '{productName}' удален из корзины");

                        // Если корзина пуста, можно удалить сам заказ
                        if (_basketProducts.Count == 0)
                        {
                            var order = Core.Context.Order.FirstOrDefault(o => o.ID == _orderId);
                            if (order != null)
                            {
                                Core.Context.Order.Remove(order);
                                Core.Context.SaveChanges();
                                _orderId = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении товара: {ex.Message}");
                }
            }
        }

        // Метод для обновления данных при навигации обратно
        public void RefreshBasket(int orderId)
        {
            _orderId = orderId;
            LoadBasket();
        }
    }
}
