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
using Anastasia423WPF.NewFolder1;

namespace Anastasia423WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private int _orderId;
        private List<Product> _orderProducts;

        public OrderPage()
        {
            InitializeComponent();
        }

        public OrderPage(int orderId) : this()
        {
            _orderId = orderId;
            LoadOrderData();
        }

        private void LoadOrderData()
        {
            try
            {
                // Устанавливаем текущую дату
                txtOrderDate.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

                if (_orderId > 0)
                {
                    // Получаем ID товаров из этого заказа
                    var productIds = Core.Context.Product_Order
                        .Where(po => po.OrderID == _orderId)
                        .Select(po => po.ProductID)
                        .ToList();

                    // Получаем товары
                    _orderProducts = Core.Context.Product
                        .Where(p => productIds.Contains(p.ID))
                        .ToList();

                    // Показываем товары
                    listBoxOrder.ItemsSource = _orderProducts;

                    // Считаем общую сумму
                    decimal total = _orderProducts.Sum(p => p.Price);
                    txtTotalPrice.Text = $"{total} ₽";

                    // Если есть сохраненные данные заказа, заполняем поля
                    var order = Core.Context.Order.FirstOrDefault(o => o.ID == _orderId);
                    if (order != null && order.Fio != "Временный заказ")
                    {
                        txtFio.Text = order.Fio;
                        txtEmail.Text = order.Email;
                        txtAddress.Text = order.Address;
                        txtOrderDate.Text = order.DataOrd.ToString("dd.MM.yyyy HH:mm");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            // Возвращаемся на страницу товаров
            NavigationService.Navigate(new Products(_orderId));
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем заполнение обязательных полей
            if (string.IsNullOrWhiteSpace(txtFio.Text))
            {
                MessageBox.Show("Введите ФИО!");
                txtFio.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Введите Email!");
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Введите адрес доставки!");
                txtAddress.Focus();
                return;
            }

            // Проверяем формат email (простая проверка)
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Введите корректный Email!");
                txtEmail.Focus();
                return;
            }

            try
            {
                // Находим заказ в БД
                var order = Core.Context.Order.FirstOrDefault(o => o.ID == _orderId);

                if (order != null)
                {
                    // Обновляем данные заказа
                    order.Fio = txtFio.Text.Trim();
                    order.Email = txtEmail.Text.Trim();
                    order.Address = txtAddress.Text.Trim();
                    order.DataOrd = DateTime.Now; // Обновляем дату заказа

                    Core.Context.SaveChanges();

                    MessageBox.Show($"Заказ №{order.ID} успешно оформлен!\nС вами свяжутся для подтверждения.",
                                    "Заказ оформлен",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);

                    // Возвращаемся на страницу товаров (без orderId, так как заказ оформлен)
                    NavigationService.Navigate(new Products());
                }
                else
                {
                    MessageBox.Show("Ошибка: заказ не найден!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении заказа: {ex.Message}");
            }
        }

    }
}
