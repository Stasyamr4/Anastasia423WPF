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
    public partial class CreditAuto : Page
    {
        Car ItsCar;
        public CreditAuto(Car car1)
        {
            ItsCar = car1;
            InitializeComponent();

        }
        decimal SrokCredit = 0;
        decimal ProcCred = 0;
        decimal A;
        public void CreditCalculate()
        {
            decimal S = ItsCar.Price - (ItsCar.Price * (ProcCred / 100m));

            // Месячная процентная ставка (процент делим на 12 месяцев)
            decimal i = ProcCred / 100m / 12m;

            // Расчет аннуитетного платежа по формуле:
            // A = S * (i * (1 + i)^n) / ((1 + i)^n - 1)

            decimal numerator = i * Power(1 + i, (int)SrokCredit);
            decimal denominator = Power(1 + i, (int)SrokCredit) - 1;

            A = S * (numerator / denominator);
        }

        private decimal Power(decimal x, int exp)
        {
            if (exp == 0) return 1;
            if (exp == 1) return x;

            decimal result = 1;
            for (int i = 0; i < exp; i++)
            {
                result *= x;
            }
            return result;
        }

        private void SrokCred_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            YearText.Visibility = Visibility.Visible;
            YearText.Text = "Выбранный срок кредита (в месяцах): ";
            SrokCredit = (decimal)Math.Round(SrokCred.Value);
            YearText.Text += SrokCredit.ToString();
            CredFinalCalc();
        }

        private void PercentCred_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {

            PercentText.Visibility = Visibility.Visible;
            PercentText.Text = "Выбранный процент первоначального взноса: ";
            ProcCred = (decimal)Math.Round(PercentCred.Value);
            PercentText.Text += ProcCred.ToString();
            CredFinalCalc();
        }

        public void CredFinalCalc()
        {
            if (SrokCredit != 0 && ProcCred != 0)
            {
                CreditCalculate();
                MonthPay.Text = $"Ваш ежемесячный платеж составляет {Math.Round(A).ToString()}";
            }
        }

        private void NextBut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Application());
        }
    }
}
