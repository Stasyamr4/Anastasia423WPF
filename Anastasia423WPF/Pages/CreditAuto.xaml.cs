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
    /// Логика взаимодействия для CreditAuto.xaml
    /// </summary>
    public partial class CreditAuto : Page
    {
        Car ItsCar;
        public CreditAuto(Car car1)
        {
            ItsCar = car1;
            InitializeComponent();
            
        }
        double SrokCredit = 0;
        double ProcCred = 0;
        decimal A;
        public void CreditCalculate()
        {
            decimal S = ItsCar.Price - (ItsCar.Price * (decimal)(ProcCred / 100));
            int i = (int)22.5/12;
            A = S * ((i * (1 + i) ^ ((int)SrokCredit)) / (1 + i) ^ (int)(SrokCredit - 1));
        }

        private void SrokCred_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //YearText.Visibility = Visibility.Visible;
            YearText.Text += SrokCred.Value.ToString();
            SrokCredit = SrokCred.Value;
        }

       

        private void SrokCred_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            YearText.Visibility = Visibility.Visible;
            YearText.Text = "Выбранный срок кредита (в месяцах): ";
            SrokCredit = Math.Round(SrokCred.Value);
            YearText.Text += SrokCredit.ToString();
        }

        private void PercentCred_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {

            PercentText.Visibility = Visibility.Visible;
            PercentText.Text = "Выбранный процент кредита: ";
            ProcCred = Math.Round(PercentCred.Value);
            PercentText.Text += ProcCred.ToString();
            if (SrokCredit != 0 && ProcCred != 0)
            {
                CreditCalculate();
                MonthPay.Text = $"Ваш ежемесячный платеж составляет {A.ToString()}";
            }
        }
    }
}
