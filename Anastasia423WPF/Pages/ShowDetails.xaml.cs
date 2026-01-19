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
    /// Логика взаимодействия для ShowDetails.xaml
    /// </summary>
    public partial class ShowDetails : Page
    {
        Car car1;
        List<Options> CheckDop;
        public ShowDetails(Car mycar, List<Options> chek)
        {
            car1 = mycar;
            CheckDop = chek;
            InitializeComponent();
            ChangeInform();
        }

        public void ChangeInform()
        {
            MyCar.Text += car1.Name;
            Engine.Text += car1.engine.Name;
            foreach(var dops in CheckDop)
            {
                Dop.Text += $"{dops.Name}, ";
            }
            Dop.Text = Dop.Text.Remove(Dop.Text.Length - 1);
            ColorCar.Text += car1.color;
            PriceCar.Text += car1.Price;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationData.CurrentData = car1 ;
            NavigationService.Navigate(new CreditAuto(car1));
        }
    }
}
