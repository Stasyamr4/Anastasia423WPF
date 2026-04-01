using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace OhmsLawCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем значения из полей
                double voltage = string.IsNullOrEmpty(txtVoltage.Text) ? 0 :
                    double.Parse(txtVoltage.Text.Replace(".", ","), CultureInfo.InvariantCulture);
                double current = string.IsNullOrEmpty(txtCurrent.Text) ? 0 :
                    double.Parse(txtCurrent.Text.Replace(".", ","), CultureInfo.InvariantCulture);
                double resistance = string.IsNullOrEmpty(txtResistance.Text) ? 0 :
                    double.Parse(txtResistance.Text.Replace(".", ","), CultureInfo.InvariantCulture);

                // Определяем что вычислять
                if (rbCurrent.IsChecked == true)
                {
                    // Вычисляем силу тока: I = U/R
                    if (resistance == 0)
                    {
                        MessageBox.Show("Сопротивление не может быть равно 0!", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    double result = voltage / resistance;
                    txtCurrent.Text = result.ToString("0.###", CultureInfo.InvariantCulture);
                }
                else if (rbVoltage.IsChecked == true)
                {
                    // Вычисляем напряжение: U = I*R
                    double result = current * resistance;
                    txtVoltage.Text = result.ToString("0.###", CultureInfo.InvariantCulture);
                }
                else if (rbResistance.IsChecked == true)
                {
                    // Вычисляем сопротивление: R = U/I
                    if (current == 0)
                    {
                        MessageBox.Show("Сила тока не может быть равна 0!", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    double result = voltage / current;
                    txtResistance.Text = result.ToString("0.###", CultureInfo.InvariantCulture);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка ввода данных! Введите корректные числа.\n\nПример: 10.5 или 10,5",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtVoltage.Clear();
            txtCurrent.Clear();
            txtResistance.Clear();
            rbCurrent.IsChecked = true;
        }
    }
}